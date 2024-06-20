using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeterClient;

Console.WriteLine("start grpc call...");
using var channel = GrpcChannel.ForAddress("http://localhost:7777");
var client = new Greeter.GreeterClient(channel);

CancellationTokenSource cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(3));
try
{
    var reply = await client.SayHelloAsync(
                      new HelloRequest { Name = "GreeterClient" },
                      deadline: DateTime.UtcNow.AddSeconds(3) 
                    //cancellationToken: cts.Token             
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
finally
{
    cts.Dispose();
}
Console.ReadKey();