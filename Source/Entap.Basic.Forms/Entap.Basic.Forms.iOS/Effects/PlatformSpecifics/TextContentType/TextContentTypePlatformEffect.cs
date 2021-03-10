using System;
using Entap.Basic.Forms.iOS.PlatformSpecifics;
using Entap.Basic.Forms.PlatformConfiguration.iOSSpecific;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FormsElement = Xamarin.Forms.Entry;
using UITextContentType = Entap.Basic.Forms.PlatformConfiguration.iOSSpecific.UITextContentType;

[assembly: ExportEffect(typeof(TextContentTypePlatformEffect), "TextContentTypeEffect")]
namespace Entap.Basic.Forms.iOS.PlatformSpecifics
{
    public class TextContentTypePlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var textContentType = (Element as FormsElement)?.OnThisPlatform()?.GetUITextContentType();
                var uiTextContentType = GetTextContentTypeString(textContentType);
                UpdateTextContentType(uiTextContentType);
            }
            catch { }
        }

        protected override void OnDetached()
        {
        }

        void UpdateTextContentType(NSString textContentType)
        {
            if (Control is not UITextField textField) return;
            textField.TextContentType = textContentType;
        }

        NSString GetTextContentTypeString(UITextContentType? textContentType)
        {
            if (textContentType is null) return null;
            return textContentType switch
            {
                UITextContentType.AddressCity => UIKit.UITextContentType.AddressCity,
                UITextContentType.AddressCityAndState => UIKit.UITextContentType.AddressCityAndState,
                UITextContentType.AddressState => UIKit.UITextContentType.AddressState,
                UITextContentType.CountryName => UIKit.UITextContentType.CountryName,
                UITextContentType.CreditCardNumber => UIKit.UITextContentType.CreditCardNumber,
                UITextContentType.EmailAddress => UIKit.UITextContentType.EmailAddress,
                UITextContentType.FamilyName => UIKit.UITextContentType.FamilyName,
                UITextContentType.FullStreetAddress => UIKit.UITextContentType.FullStreetAddress,
                UITextContentType.GivenName => UIKit.UITextContentType.GivenName,
                UITextContentType.JobTitle => UIKit.UITextContentType.JobTitle,
                UITextContentType.Location => UIKit.UITextContentType.Location,
                UITextContentType.MiddleName => UIKit.UITextContentType.MiddleName,
                UITextContentType.Name => UIKit.UITextContentType.Name,
                UITextContentType.NamePrefix => UIKit.UITextContentType.NamePrefix,
                UITextContentType.NameSuffix => UIKit.UITextContentType.NameSuffix,
                UITextContentType.NewPassword => UIKit.UITextContentType.NewPassword,
                UITextContentType.Nickname => UIKit.UITextContentType.Nickname,
                UITextContentType.OneTimeCode => UIKit.UITextContentType.OneTimeCode,
                UITextContentType.OrganizationName => UIKit.UITextContentType.OrganizationName,
                UITextContentType.Password => UIKit.UITextContentType.Password,
                UITextContentType.PostalCode => UIKit.UITextContentType.PostalCode,
                UITextContentType.StreetAddressLine1 => UIKit.UITextContentType.StreetAddressLine1,
                UITextContentType.StreetAddressLine2 => UIKit.UITextContentType.StreetAddressLine2,
                UITextContentType.Sublocality => UIKit.UITextContentType.Sublocality,
                UITextContentType.TelephoneNumber => UIKit.UITextContentType.TelephoneNumber,
                UITextContentType.Url => UIKit.UITextContentType.Url,
                UITextContentType.Username => UIKit.UITextContentType.Username,
                _ => null,
            };
        }
    }
}
