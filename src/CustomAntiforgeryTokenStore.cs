using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Antiforgery;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.Primitives;

namespace PCSC.GreenShelter
{
    /// <summary>
    /// HACK: Header base Antiforgery token won't be support until RC2
    /// https://github.com/aspnet/Antiforgery/issues/29#issuecomment-165780540
    /// </summary>
    public class CustomAntiforgeryTokenStore : IAntiforgeryTokenStore, IGreenShelterApplication
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager("Microsoft.AspNet.Antiforgery.Resources", typeof(DefaultAntiforgeryTokenStore).GetTypeInfo().Assembly);
        
        private ILogger _logger;
        private readonly AntiforgeryOptions _options;
        private readonly IAntiforgeryTokenSerializer _tokenSerializer;

        public string TagName {
            get {return "CustomAntiforgeryTokenStore"; }
        }
        
        public CustomAntiforgeryTokenStore (
            ILogger<CustomAntiforgeryTokenStore> logger,
            IOptions<AntiforgeryOptions> optionsAccessor,
            IAntiforgeryTokenSerializer tokenSerializer)
        {
            _logger = logger;
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            if (tokenSerializer == null)
            {
                throw new ArgumentNullException(nameof(tokenSerializer));
            }

            _options = optionsAccessor.Value;
            _tokenSerializer = tokenSerializer;
        }

        public AntiforgeryToken GetCookieToken(HttpContext httpContext)
        {
            _logger.LogInformation("GetCookieToken");
            
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var services = httpContext.RequestServices;
            var contextAccessor = services.GetRequiredService<IAntiforgeryContextAccessor>();
            if (contextAccessor.Value != null)
            {
                return contextAccessor.Value.CookieToken;
            }

            var requestCookie = httpContext.Request.Cookies[_options.CookieName];
            if (string.IsNullOrEmpty(requestCookie))
            {
                // unable to find the cookie.
                return null;
            }

            return _tokenSerializer.Deserialize(requestCookie);
        }

        public async Task<AntiforgeryTokenSet> GetRequestTokensAsync(HttpContext httpContext)
        {
            _logger.LogInformation("GetRequestTokensAsync");
            
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var requestCookie = httpContext.Request.Cookies[_options.CookieName];
            if (string.IsNullOrEmpty(requestCookie))
            {
                var message = string.Format(CultureInfo.CurrentCulture, 
                    _resourceManager.GetString("Antiforgery_CookieToken_MustBeProvided"), _options.CookieName);
                throw new InvalidOperationException(message);
            }

            StringValues requestToken;
            if (httpContext.Request.HasFormContentType)
            {
                // Check the content-type before accessing the form collection to make sure
                // we throw gracefully.
                var form = await httpContext.Request.ReadFormAsync();
                requestToken = form[_options.FormFieldName];
            }

            // Fall back to header if the form value was not provided.
            if (requestToken.Count == 0)
            {
                requestToken = httpContext.Request.Headers[_options.FormFieldName];
            }

            if (requestToken.Count == 0)
            {
                var message = _resourceManager.GetString("Antiforgery_FormToken_MustBeProvided");
                throw new InvalidOperationException(message);
            }

            return new AntiforgeryTokenSet(requestToken, requestCookie);
        }

        public void SaveCookieToken(HttpContext httpContext, AntiforgeryToken token)
        {
            _logger.LogInformation("SaveCookieToken");
            
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            // Add the cookie to the request based context.
            // This is useful if the cookie needs to be reloaded in the context of the same request.

            var services = httpContext.RequestServices;
            var contextAccessor = services.GetRequiredService<IAntiforgeryContextAccessor>();
            Debug.Assert(contextAccessor.Value == null, "AntiforgeryContext should be set only once per request.");
            contextAccessor.Value = new AntiforgeryContext() { CookieToken = token };

            var serializedToken = _tokenSerializer.Serialize(token);
            var options = new CookieOptions() { HttpOnly = true };

            // Note: don't use "newCookie.Secure = _options.RequireSSL;" since the default
            // value of newCookie.Secure is poulated out of band.
            if (_options.RequireSsl)
            {
                options.Secure = true;
            }

            httpContext.Response.Cookies.Append(_options.CookieName, serializedToken, options);
        }
    }
}