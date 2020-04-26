﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
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
                new IdentityResource("roles","Your role(s)",new List<string>()
                {
                    "role"
                }),
                new IdentityResource("country","Name of coutry you're living in",new List<string>(){ "country" }),
                new IdentityResource("subscriptionlevel","Subscription Level",new List<string>(){ "subscriptionlevel" })
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
             new ApiResource("imagegalleryapi","Image Gallery API")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "Image Gallery",
                    ClientId = "imagegalleryclient",
                    AllowOfflineAccess = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>(){ "https://localhost:44339/signin-oidc" },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44339/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "imagegalleryapi",
                        "country",
                        "subscriptionlevel"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                }
            };

    }
}