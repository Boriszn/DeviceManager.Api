// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public const string SwaggerClient = "SWAGGER_CLIENT";

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com"),
                        new Claim(JwtClaimTypes.Role, "Admin")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com"),
                        new Claim(JwtClaimTypes.Role, "User")
                    }
                },
                new TestUser
                {
                    SubjectId = "3",
                    Username = "jan",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Jan"),
                        new Claim("website", "https://jan.com"),
                        new Claim(JwtClaimTypes.Role, "Manager")
                    }
                }
            };
        }

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
                new ApiResource("DeviceManagerApi", "Device Manager API", new [] { JwtClaimTypes.Name, JwtClaimTypes.WebSite, JwtClaimTypes.Role })
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
                        Extensions.GetUriFromEnvironmentAndCombine(SwaggerClient, "swagger/oauth2-redirect.html").ToString(),
                        Extensions.GetUriFromEnvironmentAndCombine(SwaggerClient, "/swagger/o2c.html").ToString(),

                    },

                    AllowedScopes = { "DeviceManagerApi"},
                    AlwaysSendClientClaims = true,
                    ClientClaimsPrefix = ""
                }
            };
        }
    }
}