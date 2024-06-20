using Grpc.Core;
using Grpc.Core.Interceptors;

namespace TestGrpcCancelInterceptor.Interceptors
{
    public class CancelInterceptor : Interceptor
    {
        private readonly TimeSpan _ts;

        public CancelInterceptor(TimeSpan ts)
        {
            _ts = ts;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            //CancellationTokenSource cts = new CancellationTokenSource();
            //cts.CancelAfter(_ts);
            //var options = context.Options.WithCancellationToken(cts.Token);
            //context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options); 

            var options= context.Options.WithDeadline(DateTime.UtcNow.AddSeconds(_ts.TotalSeconds));
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);

            return base.AsyncUnaryCall(request, context, continuation);
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            return base.UnaryServerHandler(request, context, continuation);
        }
        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var options = context.Options.WithDeadline(DateTime.UtcNow.AddSeconds(_ts.TotalSeconds));
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            return base.BlockingUnaryCall(request, context, continuation);
        }
        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            var options = context.Options.WithDeadline(DateTime.UtcNow.AddSeconds(_ts.TotalSeconds));
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            return base.AsyncClientStreamingCall(context, continuation);
        }
        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            var options = context.Options.WithDeadline(DateTime.UtcNow.AddSeconds(_ts.TotalSeconds));
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            return base.AsyncServerStreamingCall(request, context, continuation);
        }
        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            var options = context.Options.WithDeadline(DateTime.UtcNow.AddSeconds(_ts.TotalSeconds));
            context = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, options);
            return base.AsyncDuplexStreamingCall(context, continuation);
        }
    }
}