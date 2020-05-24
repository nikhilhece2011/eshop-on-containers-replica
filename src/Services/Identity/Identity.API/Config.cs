// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                //new IdentityResource("roles","Your role(s)",new List<string>()
                //{
                //    "role"
                //}),
                //new IdentityResource("country","Name of coutry you're living in",new List<string>(){ "country" }),
                //new IdentityResource("subscriptionlevel","Subscription Level",new List<string>(){ "subscriptionlevel" })
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
             new ApiResource("basketapi","Basket API"),
             new ApiResource("catalogapi","Catalog API"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "MVC Client",
                    ClientId = "mvcclient",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>(){ "http://localhost:5100/signin-oidc" },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "http://localhost:5100/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        //"roles",
                        "catalogapi",
                        "basketapi"
                        //"country",
                        //"subscriptionlevel"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                },
                new Client()
                {
                    ClientName = "Basket Swagger Client",
                    ClientId = "basketswaggerclient",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>(){ "http://localhost:5103/swagger/o2c.html" },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "http://localhost:5103/swagger/"
                    },
                    AllowedScopes =
                    {
                       "basketapi"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                }
            };

    }
}