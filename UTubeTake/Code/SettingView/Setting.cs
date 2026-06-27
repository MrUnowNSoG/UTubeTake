using Microsoft.Maui.Storage;

namespace UTubeTake.Code.Setting {

    internal sealed class Setting {

        private const string KEY_VIDEO = "VideoFolderPath";
        private const string KEY_IMAGE = "ImageFolderPath";

        private readonly string _pathSettingFile;
        private readonly string _defaultVideoFolder;
        private readonly string _defaultImageFolder;

        public Setting() {
            _pathSettingFile = Path.Combine(FileSystem.AppDataDirectory, "Setting.txt");

            _defaultVideoFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            _defaultImageFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }

        public string DefaultVideoFolder => _defaultVideoFolder;
        public string DefaultImageFolder => _defaultImageFolder;

        public bool HadSettingFile => File.Exists(_pathSettingFile);


        public void CreateAllFiles() {
            FileStream file = File.Create(_pathSettingFile);
            file.Close();

            ClearFile(_pathSettingFile);
            AddParameter(_pathSettingFile, KEY_VIDEO, _defaultVideoFolder);
            AddParameter(_pathSettingFile, KEY_IMAGE, _defaultImageFolder);
        }

        public void SaveFolders(string video, string image) {
            ClearFile(_pathSettingFile);
            AddParameter(_pathSettingFile, KEY_VIDEO, video);
            AddParameter(_pathSettingFile, KEY_IMAGE, image);
        }


        public string GetVideoFolder() {
            string? str = ReadParameter(_pathSettingFile, KEY_VIDEO);

            if (str == null || str == "") return _defaultVideoFolder;
            if(Directory.Exists(str) == false) return _defaultVideoFolder;

            return str;
        }

        public string GetImageFolder() {
            string? str = ReadParameter(_pathSettingFile, KEY_IMAGE);

            if (str == null || str == "") return _defaultImageFolder;
            if (Directory.Exists(str) == false) return _defaultImageFolder;

            return str;
        }

        private void ClearFile(string path) {
            using (StreamWriter writer = new StreamWriter(path, false)) {
                writer.Write("");
            }
        }

        private void AddParameter(string path, string key, string value) {
            using (StreamWriter writer = new StreamWriter(path, true)) {
                writer.Write($@"`{key}`:`{value}`" + "\n");
            }
        }

        private string? ReadParameter(string path, string key) {

            string prefix = $"`{key}`:`";

            string? match = null;

            using (StreamReader sr = new StreamReader(path)) {
                string? line;
                while ((line = sr.ReadLine()) != null) {
                    if (line.StartsWith(prefix, StringComparison.Ordinal)
                        && line.EndsWith("`", StringComparison.Ordinal)) {
                        match = line;
                        break;
                    }
                }
            }

            if (match == null) return null;

            int valueLength = match.Length - prefix.Length - 1;
            if (valueLength <= 0) return null;

            return match.Substring(prefix.Length, valueLength);
        }
    }
}
