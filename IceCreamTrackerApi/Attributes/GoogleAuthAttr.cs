using Google.Apis.Auth;
using Helpers.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace IceCreamTrackerApi.Attributes
{
    public class GoogleAuthAttr : TypeFilterAttribute
    {
        public GoogleAuthAttr() : base(typeof(GoogleAuthorizeFilter)) { }

    }

    public class GoogleAuthorizeFilter : IAuthorizationFilter
    {
        private readonly JwtHandler _jwtHandler;

        public GoogleAuthorizeFilter(JwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var headers = context.HttpContext.Request.Headers;

                var authHeader = headers["Authorization"].ToString();

                var token = authHeader.Remove(0, 7);

                var payload = _jwtHandler.VerifyGoogleToken(token);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
