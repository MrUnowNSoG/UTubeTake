using UTubeTake.Code.Tools.ErrorHandler;



namespace UTubeTake.Code {

    internal sealed class ThumbnailVideoService {

        private readonly HttpClient _httpClient;

        public ThumbnailVideoService() { 
            _httpClient = new HttpClient();
        }

        public string? GetThumbnailUrl(string url) {

            string? code = GetCodeThumbnail(url);

            if (code != null) return @"http://img.youtube.com/vi/" + code + @"/mqdefault.jpg";
            return null;
        }

        private string? GetCodeThumbnail(string url) {

            if (url.Contains("https://www.youtube.com/shorts/") == true) {
                url = url.Remove(0, 31);

                if (url.Length > 3) return url;
            }

            if (url.Contains("https://www.youtube.com/watch?v=") == true) {

                int index = 0;
                int count = 0;

                for (int i = 0; i < url.Length; i++) {

                    if (url[i] == '=') {

                        if (index == 0) {
                            index = i + 1;

                        } else {
                            count = (i - 5) - index;
                            break;
                        }
                    }
                }

                url = count != 0
                    ? url.Substring(index, count)
                    : url.Substring(index, url.Length - index);

                if (url.Length > 3) return url;
            }

            return null;
        }

        public async Task<bool> DownloadThumbnail(string urlVideo, string nameFile, string path) {

            string? url = GetThumbnailUrl(urlVideo);
            if (url == null) return false;

            try {

                byte[] img = await _httpClient.GetByteArrayAsync(url);
                string pathFile = System.IO.Path.Combine(path, nameFile + ".jpg");
                await File.WriteAllBytesAsync(pathFile, img);
                return true;

            } catch (Exception ex) {
                ErrorHandlerService.GetInstance().CathcError(ex);
                return false;
            }

        }

    }

}
