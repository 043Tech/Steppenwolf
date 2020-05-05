using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Steppenwolf.Config;

namespace Steppenwolf.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddIdentityProviders(this AuthenticationBuilder authenticationBuilder, IdentityProviders config)
        {
            if (config.Facebook != null)
            {
                authenticationBuilder.AddFacebook(options =>
                {
                    options.AppId = config.Facebook.AppId;
                    options.AppSecret = config.Facebook.AppSecret;
                });   
            }
            
            if (config.Google != null)
            {
                authenticationBuilder.AddGoogle(options =>
                {
                    options.ClientId = config.Google.ClientId;
                    options.ClientSecret = config.Google.ClientSecret;
                });   
            }

            if (config.Twitter != null)
            {
                authenticationBuilder.AddTwitter(options =>
                {
                    options.ConsumerKey = config.Twitter.ApiKey;
                    options.ConsumerSecret = config.Twitter.ApiSecret;
                });   
            }

            return authenticationBuilder;
        }
    }
}