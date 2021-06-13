﻿using MessagePipe.InProcess.Internal;
using MessagePipe.InProcess.Workers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessagePipe.InProcess
{
    [Preserve]
    public sealed class NamedPipeRemoteRequestHandler<TRequest, TResponse> : IRemoteRequestHandler<TRequest, TResponse>
    {
        readonly NamedPipeWorker worker;
        readonly Type handlerType;

        [Preserve]
        public NamedPipeRemoteRequestHandler(NamedPipeWorker worker, IAsyncRequestHandler<TRequest, TResponse> handler)
        {
            this.worker = worker;
            this.handlerType = handler.GetType();
        }

        public async ValueTask<TResponse> InvokeAsync(TRequest request, CancellationToken cancellationToken = default)
        {
            return await worker.RequestAsync<TRequest, TResponse>(handlerType, request, cancellationToken);
        }
    }
}
