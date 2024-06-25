using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTubeTake.Code {
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
