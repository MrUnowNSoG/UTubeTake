using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.Tools;
using UTubeTake.Code.VideoManger.VideoData;
using YoutubeExplode.Videos.Streams;



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

        public void SetPickers(List<QualityOptionData> quality, List<BitRateOptionData> bitrate) {
            _qualityPicker.ItemsSource = quality;
            _bitratePicker.ItemsSource = bitrate;
        }

        public (VideoOnlyStreamInfo?, AudioOnlyStreamInfo?) GetCurrentSelect() {

            VideoOnlyStreamInfo? video = null;
            AudioOnlyStreamInfo? bitRate = null;
            if (_qualityPicker.SelectedItem is QualityOptionData qData) video = qData.GetStream();
            if (_bitratePicker.SelectedItem is BitRateOptionData bData) bitRate = bData.GetStream();
            
            return (video, bitRate);
        }


        public void UpdateFileDownload(string percent) {
            _labelButton.Text = percent;
        }

        public void SetDefaultState() {
            Color textColor = AppColor.Background;
            SetState(Icons.Download, textColor, "Downloading", AppColor.MainAccent, AppColor.MainAccent);
        }

        public void SetLoadingState() {
            Color textColor = AppColor.TextSecondary;
            SetState(Icons.Loading, textColor, "0%", AppColor.Border, AppColor.CardBackground);
        }

        public void SetCompleteState() {
            Color textColor = AppColor.TextPrimary;
            SetState(Icons.Complete, textColor, "Done", AppColor.SecondAccent, AppColor.SubSecondAccent);
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
