using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Profile;

namespace dummy_gateway{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase{

        private readonly ProfileService.ProfileServiceClient _profileClient;
        private readonly ScoreService.ScoreServiceClient _scoreClient;

        public GatewayController(
            ProfileService.ProfileServiceClient profileClient,
            ScoreService.ScoreServiceClient scoreClient
        ){
            _profileClient = profileClient;
            _scoreClient = scoreClient;
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
        public async Task<SetProfileResponse> SetProfile(string id){
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
            var body = Request.Body;
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
                return new ScoreResponse{};
            }
            var resp =  _scoreClient.SubmitScore(requestPayload);
            return resp;
        }
    }
}