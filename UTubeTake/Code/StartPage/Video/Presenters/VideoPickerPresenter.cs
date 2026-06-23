using UTubeTake.Code.StartPage.Video.Elements;



namespace UTubeTake.Code.StartPage.Video.Presenters {

    internal sealed class VideoPickerPresenter {

        private readonly Picker _qualityPicker;
        private readonly Picker _bitratePicker;
        private readonly Border _borderButton;
        private readonly Image _imageButton;
        private readonly Label _labelButton;

        public VideoPickerPresenter(VideoPickerElements elements) {
            _qualityPicker = elements.Quality;
            _bitratePicker = elements.Bitrate;
            _borderButton = elements.Border;
            _imageButton = elements.Image;
            _labelButton = elements.Label;
        }

        public void SetPickers(List<string> quality, List<string> bitrate) {
            _qualityPicker.ItemsSource = quality;
            _bitratePicker.ItemsSource = bitrate;
        }

        public (int, int) GetCurrentSelect() {
            return (_qualityPicker.SelectedIndex, _bitratePicker.SelectedIndex);
        }

        public void UpdateFileDownload(string percent) {
            _labelButton.Text = percent;
        }

    }

}
