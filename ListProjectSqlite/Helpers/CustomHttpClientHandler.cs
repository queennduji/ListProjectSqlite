using System;
namespace ListProjectSqlite.Helpers
{
	public class CustomHttpClientHandler : HttpClientHandler
    {
        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            return base.Send(request, cancellationToken);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

