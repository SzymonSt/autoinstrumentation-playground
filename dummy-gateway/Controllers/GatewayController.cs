using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using System.Net.Http;
using Profile;
using static System.Net.Mime.MediaTypeNames;

namespace dummy_gateway{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase{

        private readonly ProfileService.ProfileServiceClient _profileClient;
        private readonly ScoreService.ScoreServiceClient _scoreClient;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly string _scoreServiceHttpAddr;

        public GatewayController(
            ProfileService.ProfileServiceClient profileClient,
            ScoreService.ScoreServiceClient scoreClient,
            IHttpClientFactory httpClientFactory
        ){
            _profileClient = profileClient;
            _scoreClient = scoreClient;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("/get_profile")]
        public GetProfileResponse GetProfileHandler(){
            var id = Request.Query["id"];
            GetProfileRequest request = new GetProfileRequest
            {
                Id = id
            };
            var resp = _profileClient.GetProfile(request);
            return resp;
        }

        [HttpPut]
        [Route("/set_profile")]
        public async Task<SetProfileResponse> SetProfile(){
            var body = Request.Body;
            SetProfileRequest requestPayload = new SetProfileRequest();
            char[] buffer = new char[2048];
            try{
                StreamReader reader = new StreamReader(body);
                var charNum = await reader.ReadAsync(buffer);
                char[] trimmedBuffer = new char[charNum];
                Array.Copy(buffer, trimmedBuffer, charNum);
                Console.WriteLine(trimmedBuffer);
                requestPayload = JsonSerializer.Deserialize<SetProfileRequest>(new string(trimmedBuffer));
            } catch (Exception e){
                return new SetProfileResponse{};
            }
            var resp = _profileClient.SetProfile(requestPayload);
            return resp;
        }

        [HttpPut]
        [Route("/submit_score")]
        public async Task<ScoreResponse> SubmitScore(){
            var downstream = Request.Query["downstream"];
            ScoreResponse r = new ScoreResponse{};
            var body = Request.Body;
            var requestPayload = await DecodeBody(body);
            switch (downstream){
                case "grpc":
                    r =  SubmitScoreDownstreamGrpc(requestPayload);
                    break;
                case "http1_1":
                    r = await SubmitScoreDownstreamHttp1_1(requestPayload);
                    break;
                default:
                    r = SubmitScoreDownstreamGrpc(requestPayload);
                    break;
            }
            return r;
        }

        private ScoreResponse SubmitScoreDownstreamGrpc(ScoreRequest requestPayload){
            var resp =  _scoreClient.SubmitScore(requestPayload);
            return resp;
        }
        private async Task<ScoreResponse> SubmitScoreDownstreamHttp1_1(ScoreRequest requestPayload){
            var content = new StringContent(
                JsonSerializer.Serialize(requestPayload),
                Encoding.UTF8,
                Application.Json);
            var httpClient = _httpClientFactory.CreateClient("ScoreServiceHttp");
            var httpResponseMessage = await httpClient.PutAsync("/api/submit_score", content);
            var resp = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ScoreResponse>(resp);
        }
        private async Task<ScoreRequest> DecodeBody(Stream body){
            ScoreRequest requestPayload = new ScoreRequest();
            char[] buffer = new char[2048];
            try{
                StreamReader reader = new StreamReader(body);
                var charNum = await reader.ReadAsync(buffer);
                char[] trimmedBuffer = new char[charNum];
                Array.Copy(buffer, trimmedBuffer, charNum);
                Console.WriteLine(trimmedBuffer);
                requestPayload = JsonSerializer.Deserialize<ScoreRequest>(new string(trimmedBuffer));
            } catch (Exception e){
                return new ScoreRequest{};
            }
            return requestPayload;
        }
    }
}