namespace UTubeTake.Code.Tools {

    internal static class StringHelper {

        public static string? ValidationUrl(string url) {

            if (string.IsNullOrWhiteSpace(url)) return null;

            url = url.Trim();
            url = url.Replace(" ", "");

            if (url.Contains(YouTubeUrls.DomainFull) != true && url.Contains(YouTubeUrls.DomainShort) != true)
                return null;

            if (url.StartsWith("http://") != true && url.StartsWith("https://") != true)
                url = "https://www." + url;

            if (url.Length < 20) return null;

            return url;
        }

        public static string RemoveInvalidPathChars(string name) {

            char[] invalidChars = Path.GetInvalidFileNameChars();

            foreach (char c in invalidChars) {
                name = name.Replace(c.ToString(), " ");
            }

            return name;
        }

    }

}
