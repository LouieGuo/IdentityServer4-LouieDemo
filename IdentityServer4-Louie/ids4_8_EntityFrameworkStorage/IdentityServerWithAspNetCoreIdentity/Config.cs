using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetCoreIdentity
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","my api")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    ClientSecrets=
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes={"api1" },
                },
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId="mvc",
                    ClientName="mvc client",
                    AllowedGrantTypes=GrantTypes.HybridAndClientCredentials,

                    RequireConsent=true,//是否出现授权界面

                    ClientSecrets=
                    {
                        new Secret("secret".Sha256())
                    },

                    //重定向到指定地址
                    RedirectUris={"http://localhost:5002/signin-oidc" },
                    //重定向到指定地址
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    
                    //允许的范围，需要与 IdentityResource提供的范围一样
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess=true
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="alice",
                    Password="pwd",

                    //声明的用户信息，后期要想保护用户的信息有条件的获取，需要在apiresource中设置
                    Claims=new []
                    {
                        new Claim("name","Alice"),
                        new Claim("website","http://www.baidu.com")
                    }

                },
                new TestUser
                {
                    SubjectId="2",
                    Username="bob",
                    Password="pwd",
                    //声明的用户信息，后期要想保护用户的信息有条件的获取，需要在apiresource中设置
                    Claims=new []
                    {
                        new Claim("name","bob"),
                        new Claim("website","http://www.baidu.com")
                    }
                }

            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

    }
}
