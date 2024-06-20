using Dapper;
using Grpc.Core;
using MySql.Data.MySqlClient;
using System.Data;
using Test;

namespace Test.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            await Task.Delay(1000);

            //查詢DB
            /*var connectStr = "Data source=192.168.249.67;initial catalog=pxplus_member;persist security info=True;user id=pxplus;password=PxpaY@2805;Min Pool Size=15;Max Pool Size=700;ConnectionLifetime=500;Treat Tiny As Boolean=false;CharSet=utf8mb4;SslMode=None;";
            using (IDbConnection dbConnection = new MySqlConnection(connectStr))
            {
                dbConnection.Open();

                // 查詢數據
                string query = "SELECT * FROM pxplus_member.member;";
                var members = await dbConnection.QueryAsync<object>(query, context.CancellationToken);
                Console.WriteLine($"member count :{members.Count()}");
            }*/

            //迴圈耗時間
            int i = 0;
            while (i < 1000000)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("loop break");
                    break;
                }
                Console.WriteLine($"index:{i++}");
            }

            return new HelloReply
            {
                Message = "Hello " + request.Name
            };
        }
    }
}