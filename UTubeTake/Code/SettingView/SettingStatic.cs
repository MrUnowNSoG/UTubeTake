namespace UTubeTake.Code.Setting {

    public static class SettingStatic {

        public static string PathForVideo = "";
        public static string PathForImage = "";
        private static Setting _setting = new Setting();

        public static void LoadSetting() {

            try {
                if (_setting.HadSettingFile == false) _setting.CreateAllFiles();

                PathForVideo = _setting.GetVideoFolder();
                PathForImage = _setting.GetImageFolder();

            } catch (Exception ex) {
                // Settings I/O must never crash startup: fall back to safe defaults.
                System.Diagnostics.Debug.WriteLine($"LoadSetting failed: {ex}");
                PathForVideo = _setting.DefaultVideoFolder;
                PathForImage = _setting.DefaultImageFolder;
            }
        }
        

        public static void SaveSetting(string videoPath, string imgPath) {
            PathForVideo = videoPath;
            PathForImage = imgPath;
            _setting.SaveFolders(videoPath, imgPath);
        }
    
    }
}
