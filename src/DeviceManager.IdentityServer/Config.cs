// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4.Quickstart.UI;
using DeviceManager.IdentityServer.Quickstart;

namespace DeviceManager.IdentityServer
{
    public static class Config
    {
        public const string SwaggerClient = "SWAGGER_CLIENT";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            var x = new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
            return x;
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("DeviceManagerApi", "Device Manager API", new [] { JwtClaimTypes.Name, JwtClaimTypes.WebSite, JwtClaimTypes.Role, ApplicationConstants.TenantClaim })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // OpenID Connect implicit flow Swagger
                new Client
                {
                    ClientId = "DeviceManagerApi_Swagger",
                    ClientName = "Device Manager Api Swagger Ui",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    // where to redirect to after login
                    RedirectUris = {
                        //"http://localhost:52217/swagger/oauth2-redirect.html",
                        //"http://localhost:52217/swagger/o2c.html"
                        Extensions.GetUriFromEnvironmentAndCombine(SwaggerClient, "/swagger/oauth2-redirect.html").ToString(),
                        Extensions.GetUriFromEnvironmentAndCombine(SwaggerClient, "/swagger/o2c.html").ToString(),

                    },

                    AllowedScopes = { "DeviceManagerApi"},
                    AlwaysSendClientClaims = true,
                    ClientClaimsPrefix = ""
                },

                new Client
                {
                    ClientId = "DeviceManagerApi_UnitTest",
                    ClientName = "Device Manager Unit Test",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "DeviceManagerApi"},
                    ClientSecrets  =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
        }
    }
}
