using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTubeTake.Code {

    public class LinkTest {


        public LinkTest() {
        
        }

        public bool testUrl(ref string url) {

            if (url == null) return false;

            //Space cleaner
            url = SpaceCleaner(url);

            if (url == null) return false;
            if (url.Length < 40) return false;

            if(!TestHavingText(ref url, "youtube.com")) return false;
            TestHavingText(ref url, @"https://www.", true);

            return true;
        }

        private string SpaceCleaner(string link) {
            int i = 0;
            
            while(i < link.Length) {
                if (link[i] == ' ') {
                    link = link.Remove(i, 1);
                    i -= 1;
                }

                i++;
            }

            return link;
        }

        private bool TestHavingText(ref string line, string text, bool addText = false) {

            if (line.Contains(text)) return true;
            else {

                if(addText) {
                    line = text + line;
                    return true;
                }
            }

            return false;
        }
    }
}
