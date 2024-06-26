using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UTubeTake.Code {
    internal class ImgVideoDownload {

        private WebClient _web;

        public ImgVideoDownload() {
            _web = new WebClient();
        }

        public async void DownloadImg(string link, string name, string place) {

            //Need add static resoure bool for download we img yes or not
            await Task.Run(() => {
                string pathFile = place + "\\" + name + ".png";

                link = GetImgVideoUrl(link);


                byte[] img = _web.DownloadData(link);
                File.WriteAllBytes(pathFile, img);
            });
        }

        public bool DownloadImg(string link, out ImageSource img) {

            img = "";

            if (!GetCodeLink(ref link)) return false;

            //Download
            byte[]? imgBytes = null;
           
            try {
                imgBytes = _web.DownloadData("http://img.youtube.com/vi/" + link + "/mqdefault.jpg");
            
            } catch(Exception e) {

                if(e.Message == "The remote server returned an error: (404) Not Found.") {
                    img = ImageSource.FromFile("dotnet_bot.png");
                    return true;
                }
            }

            if (imgBytes != null && imgBytes.Length > 0) {
                img = ImageSource.FromStream(() => new MemoryStream(imgBytes));
                return true; 
            }

            return false;
        }

        public string GetImgVideoUrl(string link) {

            if (GetCodeLink(ref link)) {
                return @"http://img.youtube.com/vi/" + link + @"/mqdefault.jpg";
            }

            return "urlNotFoundImg";
        }



        private bool GetCodeLink(ref string link) {

            //Short
            if (link.Contains("https://www.youtube.com/shorts/")) {
                link = link.Remove(0, 31);

                if(link.Length > 3) return true;
            }

            //Video
            if (link.Contains("https://www.youtube.com/watch?v=")) {

                //Video
                int index = 0;
                int count = 0;

                for (int i = 0; i < link.Length; i++) {
                    if (link[i] == '=') {
                        if (index == 0) {
                            index = i + 1;
                        } else {
                            count = (i - 5) - index;
                            break;
                        }
                    }
                }

                if (count != 0) link = link.Substring(index, count);
                else link = link.Substring(index, link.Length - index);

                if (link.Length > 3) return true;
            } 

            return false;
        }

    }
}
