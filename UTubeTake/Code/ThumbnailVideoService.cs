namespace UTubeTake.Code {
    internal sealed class ThumbnailVideoService {

        public ThumbnailVideoService() { }

        public string? GetThumbnailUrl(string url) {

            string? code = GetCodeThumbnail(url);

            if (code != null) return @"http://img.youtube.com/vi/" + code + @"/mqdefault.jpg";
            return null;
        }

        private string? GetCodeThumbnail(string url) {

            //Shorts
            if (url.Contains("https://www.youtube.com/shorts/") == true) {
                url = url.Remove(0, 31);

                if (url.Length > 3) return url;
            }

            //Video
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

    }

}
