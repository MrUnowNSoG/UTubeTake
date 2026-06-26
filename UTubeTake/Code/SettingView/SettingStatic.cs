namespace UTubeTake.Code.Setting {

    public static class SettingStatic {

        public static string PathForVideo = "";
        public static string PathForImage = "";
        private static Setting _setting = new Setting();

        public static void LoadSetting() {

            if(_setting.HadSettingFile == false) _setting.CreateAllFiles();

            PathForVideo = _setting.GetVideoFolder();
            PathForImage = _setting.GetImageFolder();
        }
        

        public static void SaveSetting(string videoPath, string imgPath) {
            PathForVideo = videoPath;
            PathForImage = imgPath;
            _setting.SaveFolders(videoPath, imgPath);
        }
    
    }
}
