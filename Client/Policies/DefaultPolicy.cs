﻿using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using SampleClient.Policies.Contracts;

namespace SampleClient.Policies
{
    public class DefaultPolicy : IPolicy
    {
        private const int _retryCount = 5;
        private const int _handledEventsAllowedBeforeBreaking = 3;
        private readonly TimeSpan _durationOfBreak = TimeSpan.FromMinutes(1);
        private readonly IAsyncPolicy<HttpResponseMessage> _circuitBreakerPolicy;

        protected DefaultPolicy()
        {
            _circuitBreakerPolicy = GetCircuitBreakerPolicy();
        }

        public virtual IAsyncPolicy<HttpResponseMessage> GetPolicyWrap(HttpRequestMessage request)
        {
            return Policy.WrapAsync(_circuitBreakerPolicy, GetRetryPolicy());
        }

        protected virtual IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy
                .Handle<Exception>()
                .OrTransientHttpError()
                .CircuitBreakerAsync<HttpResponseMessage>(
                    handledEventsAllowedBeforeBreaking: _handledEventsAllowedBeforeBreaking,
                    durationOfBreak: _durationOfBreak,
                    onBreak: (c, t) => Console.WriteLine("The circuit is in an open state"),
                    onReset: () => Console.WriteLine("The circuit is being reset")
                );
        }


        protected virtual AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy
                .Handle<Exception>()
                .OrTransientHttpError()
                .RetryAsync(
                    retryCount: _retryCount,
                    onRetry: (exception, retryCount, context) => Console.WriteLine($"retried after sleep: {retryCount}")
                );
        }
    }
}
