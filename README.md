# autoinstrumentation-playground

> [!WARNING]
> This is not a setup meant for production use. It is not secure and should not be used to any other purpose than testing and learning.

## Running instrumented application locally
> ### dummy-gateway
> _./dummy-gateway/appsettings.json_ <br>
>```
>"ProfileServiceAddress": "http://autoinstrumentation-playground-dummy-profile-service-1:8089",
>"ScoreServiceAddress": "http://autoinstrumentation-playground-dummy-score-service-1:8081"
>```
> _./dummy-gateway/.env_ <br>
>```
># enable OpenTelemetry .NET Automatic Instrumentation
>CORECLR_ENABLE_PROFILING="1"
>CORECLR_PROFILER='{918728DD-259F-4A6A-AC2B-B85E1B658318}'
>CORECLR_PROFILER_PATH="/otel-dotnet-auto/linux-x64/OpenTelemetry.AutoInstrumentation.Native.so"
>DOTNET_ADDITIONAL_DEPS="/otel-dotnet-auto/AdditionalDeps"
>DOTNET_SHARED_STORE="/otel-dotnet-auto/store"
>#Openeteletry default STARTUP HOOK
>DOTNET_STARTUP_HOOKS="/otel-dotnet-auto/net/OpenTelemetry.AutoInstrumentation.StartupHook.dll"
>OTEL_DOTNET_AUTO_HOME="/otel-dotnet-auto"
>```
>Build image using:  `docker build -t 322456/dummy-gateway:latest -f ./dummy-gateway/Dockerfile .`

> ### dummy-profile-service
> _./dummy-profile-service/.env_ <br>
>```
>POSTGRES_DB=profservice
>POSTGRES_USER=dummy-user
>POSTGRES_PASSWORD=dummy-password
>POSTGRES_HOST=autoinstrumentation-playground-dummy-profile-database-1
>OTEL_SERVICE_NAME=dummy-profile-service
>OTEL_EXPORTER_OTLP_ENDPOINT=http://autoinstrumentation-playground-otel-collector-1:4318
>OTEL_EXPORTER_OTLP_TRACES_PROTOCOL=http/protobuf
>```
>Build image using:  `docker build -t 322456/dummy-profile-service:latest -f ./dummy-profile-service/Dockerfile ./dummy-profile-service/`

> ### dummy-score-service
> _./dummy-score-service/.local.env_ <br>
>```
>DB_URI=mongodb://autoinstrumentation-playground-dummy-score-database-1:27017/?connectTimeoutMS=3000
>DB_CONN_RETRY=5
>SERVER_PORT=8081
>```
>Build image using:  `docker build -t 322456/dummy-score-service:latest -f ./dummy-score-service/Dockerfile ./dummy-score-service/`

> ### Run project
> `docker compose up -d`

## Running k8s cluster wide instrumentation with Otel Collector, Jaeger and Odigos