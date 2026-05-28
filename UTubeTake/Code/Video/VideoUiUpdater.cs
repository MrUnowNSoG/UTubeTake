using UTubeTake.Code.StartPage;



namespace UTubeTake.Code {

    internal sealed class VideoUiUpdater {

        private Picker _qualityPicker;
        private Picker _bitRatePicker;

        private Label _name;
        private Label _author;
        private Label _duration;
        private Label _sizeDownload;

        public VideoUiUpdater(StartPage_XAMLContainer container) {

            _qualityPicker = container.QualityPicker;
            _bitRatePicker = container.BitRatePicker;

            _name = container.NameVideoLabel;
            _author = container.AuthorVideoLabel;
            _duration = container.DurationVideoLabel;
            _sizeDownload = container.SizeVideoLabel;

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
