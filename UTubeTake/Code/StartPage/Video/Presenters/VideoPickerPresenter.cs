using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.Tools;



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

        public void SetDefaultState() {
            Color textColor = AppColor.Background;
            SetState("download_bottom_icon.png", textColor, "Download", AppColor.MainAccent, AppColor.MainAccent);
        }

        public void SetLoadingState() {
            Color textColor = AppColor.TextSecondery;
            SetState("loading_thumbnail_icon.png", textColor, "0%", AppColor.Border, AppColor.CardBackground);
        }

        public void SetCompleteState() {
            Color textColor = AppColor.TextPrimary;
            SetState("check_icon_second_accent.png", textColor, "Done", AppColor.SecondAccent, AppColor.SubSecondAccent);
        }

        private void SetState(string img, Color colorLabel, string label, Color stroke, Color back) {
            _borderButton.Stroke = stroke;
            _borderButton.BackgroundColor = back;

            _imageButton.Source = ImageSource.FromFile(img);
            _labelButton.TextColor = colorLabel;
            _labelButton.Text = label;
        }

    }

}
