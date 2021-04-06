using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Entap.Basic.Firebase.Auth.EmailLink
{
    /// <summary>
    /// https://firebase.google.com/docs/auth/custom-email-handler
    /// </summary>
    public class EmailLinkHandler
    {
        public static EmailLinkHandler Current => LazyInitializer.Value;
        static readonly Lazy<EmailLinkHandler> LazyInitializer = new Lazy<EmailLinkHandler>(() => new EmailLinkHandler());

        PasswordAuthService _authService = new PasswordAuthService();

        public EmailLinkHandler()
        {
        }

        public virtual void OnResetPassword(EmailActionParameter parameter)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(EmailLinkHandler)}.{nameof(OnResetPassword)}");
        }

        public virtual void OnRecoverEmail(EmailActionParameter parameter)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(EmailLinkHandler)}.{nameof(OnRecoverEmail)}");
        }

        public virtual void OnVerifyEmail(EmailActionParameter parameter)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(EmailLinkHandler)}.{nameof(OnRecoverEmail)}");
        }

#nullable enable
        public void HandleEmailAction(string? url)
#nullable disable
        {
            if (string.IsNullOrEmpty(url)) return;

            var uri = new Uri(url);
            var query = uri.Query;
            if (string.IsNullOrEmpty(query)) return;

            var parameter = GetEmailActionParameter(query);
            if (parameter is null) return;

            switch (parameter.ActionMode)
            {
                case EmailActionMode.ResetPassword:
                    OnResetPassword(parameter);
                    break;
                case EmailActionMode.RecoverEmail:
                    OnRecoverEmail(parameter);
                    break;
                case EmailActionMode.VerifyEmail:
                    OnVerifyEmail(parameter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

#nullable enable
        static EmailActionParameter? GetEmailActionParameter(string? queryString)
#nullable disable
        {
            if (string.IsNullOrEmpty(queryString)) return null;

            var dictionary = ParseQueryString(queryString);
            if (dictionary is null) return null;

            var json = JsonSerializer.Serialize(dictionary);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<EmailActionParameter>(json, options);
        }

#nullable enable
        static Dictionary<string, string>? ParseQueryString(string? queryString)
#nullable disable
        {
            if (string.IsNullOrEmpty(queryString)) return null;

            var collection = HttpUtility.ParseQueryString(queryString);
            return collection.AllKeys.ToDictionary(k => k, k => collection[k]);
        }
    }
}
