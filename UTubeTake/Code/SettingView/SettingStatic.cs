namespace UTubeTake.Code.Setting {

    public static class SettingStatic {

        public static string pathForVideo = "";
        public static string pathForImage = "";
        private static Setting _setting = new Setting();

        public static void LoadSetting() {

            if(_setting.HadSettingFile == false) _setting.CreatAllFiles();

            pathForVideo = _setting.GetVideoFolder();
            pathForImage = _setting.GetImageFolder();
        }
        

        public static void SaveSetting(string videoPath, string imgPath) {
            pathForVideo = videoPath;
            pathForImage = imgPath;
            _setting.SaveFoleders(videoPath, imgPath);
        }
    
    }
}
