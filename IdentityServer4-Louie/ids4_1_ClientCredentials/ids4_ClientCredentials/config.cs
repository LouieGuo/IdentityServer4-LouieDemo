using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ids4_ClientCredentials
{
    public class Config
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1","my API")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId="client",
                    //AccessTokenType=AccessTokenType.Jwt,配置客户端默认为Jwt的Token
                    //认证方式
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    //用于认证的密码
                    ClientSecrets=
                    {
                        new Secret("secret".Sha256())
                    },
                    //客户端有权访问 api的资源
                    AllowedScopes={ "api1"}
                }
            };
        }
    }
}
