using System;
using System.Threading.Tasks;
using Android.Content;
using Xamarin.Essentials;
using Entap.Basic.Core.Android;

namespace Sample.Droid
{
    public class StarterActivityService
    {
        public StarterActivityService()
        {
        }

        public static async Task<string> PickPhotoAsync()
        {
            var intent = new Intent(Intent.ActionGetContent);
            intent.SetType("image/*");
            var pickerIntent = Intent.CreateChooser(intent, "");
            var activity = Xamarin.Essentials.Platform.CurrentActivity;

            try
            {
                var result = await StarterActivity.StartAsync(activity, pickerIntent, 1000);
                return result.Data.ToString();
            }
            catch (TaskCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
