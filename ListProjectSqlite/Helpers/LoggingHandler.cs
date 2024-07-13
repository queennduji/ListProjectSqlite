using System;
using Microsoft.Extensions.Logging;

namespace ListProjectSqlite.Helpers
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingHandler> _logger;

        public LoggingHandler(ILogger<LoggingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request: {request.Method} {request.RequestUri}");
            if (request.Content != null)
            {
                var requestBody = await request.Content.ReadAsStringAsync();
                _logger.LogInformation($"Request Body: {requestBody}");
            }

            var response = await base.SendAsync(request, cancellationToken);

            _logger.LogInformation($"Response Status Code: {response.StatusCode}");
            if (response.Content != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response Body: {responseBody}");
            }

            return response;
        }
    }
}

