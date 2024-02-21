using IdentityModel.Client;

namespace InfoSafeConsole.Main.HttpHandlers
{
    public class BearerTokenHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = "";
            if (!string.IsNullOrEmpty(token))
            {
                request.SetBearerToken(token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}