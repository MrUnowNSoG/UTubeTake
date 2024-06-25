using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTubeTake.Code {
    internal class VideoUiUpdater {

        //Visual
        Picker _qualityPicker;
        Picker _bitRatePicker;

        private Label _name;
        private Label _author;
        private Label _duration;
        private Label _sizeDownload;

        public VideoUiUpdater(Picker pickerQuality, Picker pickerBitRate,params Label[] label) {

            _qualityPicker = pickerQuality;
            _bitRatePicker = pickerBitRate;

            _name = label[0];
            _author = label[1];
            _duration = label[2];
            _sizeDownload = label[3];
        }
        
        public void UpdateTextVideo(string[] arrayText) {
            _name.Text = arrayText[0];
            _author.Text = arrayText[1];
            _duration.Text = arrayText[2];
        }

        public void UpdatePicker(List<string> firstPicker, List<string> secondPicker) {
            _qualityPicker.ItemsSource = firstPicker;
            _bitRatePicker.ItemsSource= secondPicker;
        }

        public void UpdateSizeVideo(string size) {
            _sizeDownload.Text = size;
        }
    }
}
