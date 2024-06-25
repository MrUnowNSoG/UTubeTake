using CommunityToolkit.Maui.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTubeTake.Code {
    internal class Setting {

        //Const
        private const string keyVideo = "VideoFolderPath";
        private const string keyImage = "ImageFolderPath";

        //All path for code
        private string pathProject;
        private string pathSettingFile;
        private string pathVideoFolder;
        private string pathImageFolder;

        public Setting() {
            pathProject = AppContext.BaseDirectory;
            ClearPathProject();

            pathSettingFile = pathProject + @"\Setting.txt";
            pathVideoFolder = pathProject + @"Video";
            pathImageFolder = pathProject + @"Image";
        }

        public bool HadSettingFile => File.Exists(pathSettingFile);
        

        //Main
        public void CreatAllFiles() {
            //Folder
            if (Directory.Exists(pathVideoFolder) == false) Directory.CreateDirectory(pathVideoFolder);
            if (Directory.Exists(pathImageFolder) == false) Directory.CreateDirectory(pathImageFolder);

            //Setting
            FileStream file = File.Create(pathSettingFile);
            file.Close();

            ClearFile(pathSettingFile);
            AddParemetr(pathSettingFile, keyVideo, pathVideoFolder);
            AddParemetr(pathSettingFile, keyImage, pathImageFolder);
        }

        public void SaveFoleders(string vieo, string image) {
            ClearFile(pathSettingFile);
            AddParemetr(pathSettingFile, keyVideo, vieo);
            AddParemetr(pathSettingFile, keyImage, image);
        }


        //Getter
        public string GetVideoFolder() {
            string str = ReadParametr(pathSettingFile, keyVideo);

            if (str == null || str == "") return pathVideoFolder;
            if(Directory.Exists(str) == false) return pathVideoFolder;

            return str;
        }

        public string GetImageFolder() {
            string str = ReadParametr(pathSettingFile, keyImage);

            if (str == null || str == "") return pathImageFolder;
            if (Directory.Exists(str) == false) return pathImageFolder;

            return str;
        }


        //
        private void ClearPathProject() {
            for (int i = pathProject.Length - 1; i > 0; i--) {
                if (pathProject[i] == '\\') {
                    break;
                } else {
                    pathProject.Remove(i);
                    i++;
                }
            }
        }
        private void ClearFile(string path) {
            using (StreamWriter writer = new StreamWriter(path, false)) {
                writer.Write("");
            }
        }

        private void AddParemetr(string path, string key, string value) {
            using (StreamWriter writer = new StreamWriter(path, true)) {
                writer.Write($@"`{key}`:`{value}`" + "\n");
            }
        }

        private string ReadParametr(string path, string key) {

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
