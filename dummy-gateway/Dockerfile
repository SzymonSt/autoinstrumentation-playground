FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
WORKDIR /src
COPY ./dummy-gateway ./dummy-gateway
COPY ./dummy-proto ./dummy-proto
RUN dotnet restore "./dummy-gateway/dummy-gateway.csproj" && \
    dotnet publish "./dummy-gateway/dummy-gateway.csproj" -c Release -o /app/publish

FROM base as release 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./dummy-gateway"]