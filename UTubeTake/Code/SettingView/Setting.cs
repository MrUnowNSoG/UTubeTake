namespace UTubeTake.Code.Setting {

    internal sealed class Setting {

        private const string KEY_VIDEO = "VideoFolderPath";
        private const string KEY_IMAGE = "ImageFolderPath";

        private string _pathProject;
        private string _pathSettingFile;
        private string _pathVideoFolder;
        private string _pathImageFolder;

        public Setting() {
            _pathProject = AppContext.BaseDirectory;

            _pathSettingFile = Path.Combine(_pathProject, "Setting.txt");
            _pathVideoFolder = Path.Combine(_pathProject, "Video");
            _pathImageFolder = Path.Combine(_pathProject, "Image");
        }

        public bool HadSettingFile => File.Exists(_pathSettingFile);
        

        public void CreateAllFiles() {
            if (Directory.Exists(_pathVideoFolder) == false) Directory.CreateDirectory(_pathVideoFolder);
            if (Directory.Exists(_pathImageFolder) == false) Directory.CreateDirectory(_pathImageFolder);

            FileStream file = File.Create(_pathSettingFile);
            file.Close();

            ClearFile(_pathSettingFile);
            AddParameter(_pathSettingFile, KEY_VIDEO, _pathVideoFolder);
            AddParameter(_pathSettingFile, KEY_IMAGE, _pathImageFolder);
        }

        public void SaveFolders(string video, string image) {
            ClearFile(_pathSettingFile);
            AddParameter(_pathSettingFile, KEY_VIDEO, video);
            AddParameter(_pathSettingFile, KEY_IMAGE, image);
        }


        public string GetVideoFolder() {
            string? str = ReadParameter(_pathSettingFile, KEY_VIDEO);

            if (str == null || str == "") return _pathVideoFolder;
            if(Directory.Exists(str) == false) return _pathVideoFolder;

            return str;
        }

        public string GetImageFolder() {
            string? str = ReadParameter(_pathSettingFile, KEY_IMAGE);

            if (str == null || str == "") return _pathImageFolder;
            if (Directory.Exists(str) == false) return _pathImageFolder;

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

            string line = "";

            using (StreamReader sr = new StreamReader(path)) {
                while ((line = sr.ReadLine()) != null) {
                    if (line.Contains(@$"`{key}`")) break;
                }

            }

            if (line == "" || line == null) return null;

            //4 - these ``:` additional symbols, line remove delete last additional symbol
            int startRead = key.Length + 4;
            line = line.Substring(startRead);
            line = line.Remove(line.Length - 1);

            if (line.Length < 3) return null;

            return line;
        }
    }
}
