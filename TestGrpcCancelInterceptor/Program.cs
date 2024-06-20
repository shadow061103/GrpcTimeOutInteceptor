// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcGreeterClient;
using TestGrpcCancelInterceptor.Interceptors;

Console.WriteLine("start grpc call...");
using var channel = GrpcChannel.ForAddress("http://localhost:7777");
var invoker = channel.Intercept(new CancelInterceptor(TimeSpan.FromSeconds(1.5)));

var client = new Greeter.GreeterClient(invoker);

try
{
    //設定期限
    var reply =  client.SayHello(
                      new HelloRequest { Name = "GreeterClient" }
                                          );
    Console.WriteLine("Greeting: " + reply.Message);
}
catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
{
    Console.WriteLine("Greeting timeout.");
}
catch (OperationCanceledException ex)
{
    Console.WriteLine("Greeting cancel.");
}
Console.ReadKey();