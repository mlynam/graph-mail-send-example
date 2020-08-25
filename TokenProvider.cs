using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

namespace email_send_example
{
  public class TokenProvider : IAuthenticationProvider
  {
    private readonly IConfidentialClientApplication _app;

    public TokenProvider(IConfidentialClientApplication app)
    {
      _app = app ?? throw new System.ArgumentNullException(nameof(app));
    }

    public async Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
      var response = await _app
        .AcquireTokenForClient(new string[] { "https://graph.microsoft.com/.default" })
        .ExecuteAsync();

      request.Headers.Add("Authorization", response.CreateAuthorizationHeader());
    }
  }
}