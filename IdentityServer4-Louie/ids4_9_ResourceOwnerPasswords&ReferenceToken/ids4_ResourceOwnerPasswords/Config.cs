using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ids4_ResourceOwnerPasswords
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","my api")
                {
                    //reference通过反向通道验证，验证资源端需要配置apisecret确保接口能验证通过
                    ApiSecrets={ new Secret("secret".Sha256())}
                }
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
                {   //引用令牌方式
                    ClientId = "reference.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenType=AccessTokenType.Reference,//引用令牌
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
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
                    Password="pwd"
                },
                new TestUser
                {
                    SubjectId="2",
                    Username="bob",
                    Password="pwd"
                }
            };
        }

    }
}
