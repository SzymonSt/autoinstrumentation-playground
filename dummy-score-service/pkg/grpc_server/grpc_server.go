package grpc_server

import (
	"context"
	"time"

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
	record := &scoreservicepb.ScoreRecord{
		UserProfileName: req.UserProfile,
		Score:           req.Score,
		Time:            time.Now(),
	}
	ss.dbClient.Database(ss.dbName).Collection("scores").InsertOne(ctx, record)

	return &scoreservicepb.ScoreResponse{
		ScoreRecord: record,
	}, err
}
