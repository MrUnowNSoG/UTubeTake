using UTubeTake.Code.Tools;
using UTubeTake.Code.Tools.ErrorHandler;



namespace UTubeTake.Code {

    internal sealed class ThumbnailVideoService {

        private readonly HttpClient _httpClient;

        public ThumbnailVideoService() { 
            _httpClient = new HttpClient();
        }

        public string? GetThumbnailUrl(string url) {

            string? code = GetCodeThumbnail(url);

            if (code != null) return YouTubeUrls.ThumbnailPrefix + code + YouTubeUrls.ThumbnailSuffix;
            return null;
        }

        private static readonly char[] _codeDelimiters = { '&', '?', '/' };

        private string? GetCodeThumbnail(string url) {

            if (url.Contains(YouTubeUrls.ShortsPrefix)) {
                string code = ExtractCode(url, YouTubeUrls.ShortsPrefix);
                if (code.Length > 3) return code;
            }

            if (url.Contains(YouTubeUrls.WatchPrefix)) {
                string code = ExtractCode(url, YouTubeUrls.WatchPrefix);
                if (code.Length > 3) return code;
            }

            if (url.Contains(YouTubeUrls.ShortLinkMarker)) {
                string code = ExtractCode(url, YouTubeUrls.ShortLinkMarker);
                if (code.Length > 3) return code;
            }

            return null;
        }

        private static string ExtractCode(string url, string marker) {
            int start = url.IndexOf(marker, StringComparison.Ordinal) + marker.Length;
            string code = url.Substring(start);

            int end = code.IndexOfAny(_codeDelimiters);
            return end >= 0 ? code.Substring(0, end) : code;
        }

        public async Task<bool> DownloadThumbnail(string urlVideo, string nameFile, string path) {

            string? url = GetThumbnailUrl(urlVideo);
            if (url == null) return false;

            try {

                byte[] img = await _httpClient.GetByteArrayAsync(url);
                string pathFile = System.IO.Path.Combine(path, nameFile + FileExtensions.Jpg);
                await File.WriteAllBytesAsync(pathFile, img);
                return true;

            } catch (Exception ex) {
                ErrorHandlerService.GetInstance().CatchError(ex);
                return false;
            }

        }

    }

}
