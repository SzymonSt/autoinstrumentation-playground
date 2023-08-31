package main

import (
	"fmt"
	"net"
	"scoreservice/pkg/config"
	"scoreservice/pkg/db"
	"scoreservice/pkg/grpc_server"

	scoreservicepb "github.com/SzymonSt/autoinstrumentation-playground/dummy-proto/scoreservice-go/v1"
	"google.golang.org/grpc"
)

func main() {
	var (
		configName = "local"
	)
	cfSrv, err := config.NewConfigServer(configName)
	if err != nil {
		panic(err)
	}
	dbClient, err := db.ConnectDB(cfSrv.DBUri, cfSrv.DDConnRetries)
	if err != nil {
		panic(err)
	}
	listener, err := net.Listen("tcp", fmt.Sprintf("0.0.0.0:%s", cfSrv.ServerPort))
	if err != nil {
		panic(err)
	}
	grpcServer := grpc.NewServer()
	scoreservicepb.RegisterScoreServiceServer(grpcServer,
		grpc_server.NewScoreServiceServer(dbClient),
	)
	fmt.Printf("Starting gRPC server on port %s\n", cfSrv.ServerPort)
	err = grpcServer.Serve(listener)
	if err != nil {
		panic(err)
	}
}
