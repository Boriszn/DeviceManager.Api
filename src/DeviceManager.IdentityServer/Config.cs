// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;
using DeviceManager.IdentityServer.Helpers;

namespace DeviceManager.IdentityServer
{
    public static class Config
    {

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
                new ApiResource(ApplicationConstants.DeviceManagerApi, "Device Manager API", new [] { JwtClaimTypes.Name, JwtClaimTypes.WebSite, JwtClaimTypes.Role, ApplicationConstants.TenantClaim })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // OpenID Connect implicit flow Swagger
                new Client
                {
                    ClientId = ApplicationConstants.DeviceManagerSwaggerClient,
                    ClientName = "Device Manager Api Swagger Ui",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    // where to redirect to after login
                    RedirectUris = {
                        //"http://localhost:52217/swagger/oauth2-redirect.html",
                        //"http://localhost:52217/swagger/o2c.html"
                        GenericHelper.GetUriFromEnvironmentAndCombine(ApplicationConstants.SwaggerClient, "/swagger/oauth2-redirect.html").ToString(),
                        GenericHelper.GetUriFromEnvironmentAndCombine(ApplicationConstants.SwaggerClient, "/swagger/o2c.html").ToString(),

                    },

                    AllowedScopes = { ApplicationConstants.DeviceManagerApi},
                    AlwaysSendClientClaims = true,
                    ClientClaimsPrefix = ""
                },

                new Client
                {
                    ClientId = ApplicationConstants.DeviceManagerTestClient,
                    ClientName = "Device Manager Unit Test",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { ApplicationConstants.DeviceManagerApi},
                    ClientSecrets  =
                    {
                        new Secret(ApplicationConstants.DeviceManagerTestClientSecret.Sha256())
                    }
                }
            };
        }
    }
}
