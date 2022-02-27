using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LRMS
{
    public static class LogService
    {
        const string FileName = "log.txt";
        static string _filePath;

        static LogService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
        }

        public static async Task<string> ReadAsync()
        {
            using (var fileStream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task WriteAsync(string message)
        {
            using (var fileStream = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.Write))
            using (var writer = new StreamWriter(fileStream))
            {
                var log = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff")},{message}";
                await writer.WriteLineAsync(log);
            }
        }

        public static void Clear()
        {
            var fileInfo = new FileInfo(_filePath);
            if (!fileInfo.Exists)
                return;

            fileInfo.Delete();

        }

        public static async Task ShareAsync()
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "ログ",
                File = new ShareFile(_filePath)
            });
        }
    }
}
