import grpc

from profileservice_py import profileservice_pb2
from profileservice_py import profileservice_pb2_grpc

class ProfileServiceServicer(profileservice_pb2_grpc.ProfileServiceServicer):

    def __init__(self, db_conn) -> None:
        self.db_conn = db_conn

    async def GetProfile(
            self, request: profileservice_pb2.GetProfileRequest, 
            context: grpc.ServicerContext
        ) -> profileservice_pb2.GetProfileResponse:
        id = request.id
        curs = self.db_conn.cursor()
        curs.execute(
            query=(
                    "SELECT * FROM profiles WHERE id = '{0}'"
                ).format(id)
        )
        records = curs.fetchone()
        curs.close()
        return profileservice_pb2.GetProfileResponse(
            profile=profileservice_pb2.Profile(
                id=str(records[0]),
                name=records[1],
                email=records[2]
        ))

    async def SetProfile(
            self, request, context
        ) -> profileservice_pb2.SetProfileResponse:
        name = request.name
        email = request.email
        try:
            curs = self.db_conn.cursor()
            curs.execute(
                query=(
                        "INSERT INTO profiles (name, email) VALUES ('{0}', '{1}') RETURNING id"
                    ).format(name, email)
            )
            id = curs.fetchone()[0]
            curs.close()
        except Exception as e:
            print(e)
            return
        return profileservice_pb2.SetProfileResponse(
            profile=profileservice_pb2.Profile(
                id=str(id),
                name=name,
                email=email
        ))