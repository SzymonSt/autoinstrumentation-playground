package grpc_server

import (
	"context"

	scoreservicepb "github.com/SzymonSt/autoinstrumentation-playground/dummy-proto/scoreservice-go/v1"
	"go.mongodb.org/mongo-driver/mongo"
)

type ScoreServiceServer struct {
	dbClient *mongo.Client
	dbName   string

	scoreservicepb.UnimplementedScoreServiceServer
}

func NewScoreServiceServer(dbClient *mongo.Client) *ScoreServiceServer {
	return &ScoreServiceServer{
		dbClient: dbClient,
		dbName:   "dummy-scores",
	}
}

func (ss *ScoreServiceServer) SubmitScore(ctx context.Context, req *scoreservicepb.ScoreRequest) (*scoreservicepb.ScoreResponse, error) {
	var err error
	var record *scoreservicepb.ScoreRecord
	return &scoreservicepb.ScoreResponse{
		ScoreRecord: record,
	}, err
}
