using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Helpers.Handlers
{
    public class JwtHandler
    {
        private readonly IConfigurationSection _googleSettings;
        private readonly IConfiguration _configuration;
        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _googleSettings = _configuration.GetSection("GoogleAuthSettings");
        }
        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(DomainModels.ExternalAuth externalAuth)
        {
            try
            {
                //var settings = new GoogleJsonWebSignature.ValidationSettings()
                //{
                //    Audience = new List<string>() { _googleSettings.GetSection("clientId").Value }
                //};
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.IdToken);
                return payload;
            }
            catch(Exception ex)
            {
                throw new Exception("something went wrong with verifications", ex);
            }
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string token)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleSettings.GetSection("clientId").Value }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
                return payload;
            }
            catch(Exception ex)
            {
                throw new Exception("something went wrong with verifications", ex);

            }

        }
    }
}
