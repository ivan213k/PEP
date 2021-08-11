﻿using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceEvaluationPlatform.Models.User.Auth0
{
    public class Auth0ClientFactory : IAuth0ClientFactory
    {
        private readonly Auth0Configur _config;
        private IMemoryCache _cache;
        private const string cacheKey = "Auth0MemberClientAPiTokenKey";
        public Auth0ClientFactory(IOptions<Auth0Configur> config,IMemoryCache cache)
        {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            _cache = cache;
        }
        public async Task<ManagementApiClient> CreateManagementApi()
        {
            AccessTokenResponse token;
            if (!_cache.TryGetValue(cacheKey,out token))
            {
                var authClient = new AuthenticationApiClient(_config.Domain);
                 token = await authClient.GetTokenAsync(new ClientCredentialsTokenRequest()
                {
                    ClientId = _config.ClientId,
                    ClientSecret = _config.ClientSecret,
                    SigningAlgorithm = JwtSignatureAlgorithm.RS256,
                    Audience =_config.ManagementApiUrl
                });
                _cache.Set(cacheKey, token, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes((token.ExpiresIn/60)-10)));
            }

            var client = new ManagementApiClient(token.AccessToken, new Uri($"https://{_config.Domain}/api/v2"));
            return client;
           
        }

        public async Task<AuthenticationApiClient> CreateAuthenticationApi()
        {
            return new AuthenticationApiClient(_config.Domain);
        }
    }
}
