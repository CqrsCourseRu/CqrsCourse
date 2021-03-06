﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.CqrsFramework
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }

    public interface IRequestHandler<TRequest> : IRequestHandler<TRequest, Unit>
    {
    }
}
