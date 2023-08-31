package grpc_server

import (
	"context"

	scoreservicepb "github.com/SzymonSt/autoinstrumentation-playground/dummy-proto/scoreservice/v1/"
	"go.mongodb.org/mongo-driver/mongo"
)

type ScoreServiceServer struct {
	dbClient *mongo.Client
	dbName   string
}

func NewScoreServiceServer(dbClient *mongo.Client) *ScoreServiceServer {
	return &ScoreServiceServer{
		dbClient: dbClient,
		dbName:   "dummy-scores",
	}
}

func (ss *ScoreServiceServer) Get(ctx context.Context, req *scoreservicepb.GetRequest) (*scoreservicepb.GetResponse, error) {
	var err error
	var survey *scoreservicepb.Survey
	return &scoreservicepb.GetResponse{
		ResponseCode: 0,
		Survey:       survey,
		Err:          err,
	}, nil
}
