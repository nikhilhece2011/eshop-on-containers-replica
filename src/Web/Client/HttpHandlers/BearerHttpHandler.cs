using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Client.HttpHandlers
{
    public class BearerHttpHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContext;

        private readonly IHttpClientFactory _httpClient;
        public BearerHttpHandler(IHttpContextAccessor httpContext,
                                  IHttpClientFactory httpClient)
        {
            _httpContext = httpContext;
            _httpClient = httpClient;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _httpContext.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var refreshToken = await _httpContext.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);


            if (!string.IsNullOrEmpty(accessToken))
            {
                request.SetBearerToken(accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
