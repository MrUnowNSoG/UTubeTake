using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.Tools;



namespace UTubeTake.Code.StartPage.Video.Presenters {

    internal sealed class VideoThumbnailPresenter {

        private readonly Image _imageThumbnail;
        private readonly Border _borderThumbnailButton;
        private readonly Label _labelThumbnailButton;
        private readonly Image _imageThumbnailButton;

        public VideoThumbnailPresenter(VideoThumbnailElements elements) {
            _imageThumbnail = elements.Thumbnail;
            _borderThumbnailButton = elements.ButtonBorder;
            _imageThumbnailButton = elements.ButtonImage;
            _labelThumbnailButton = elements.ButtonLabel;
        }

        public void SetThumbnail(string? url) {
            _imageThumbnail.Source = url != null
                ? ImageSource.FromUri(new Uri(url))
                : ImageSource.FromFile(Icons.VideoPlaceholder);
        }

        public void SetDefaultState() => SetState(Icons.SaveThumbnail, AppColor.TextPrimary,"Save thumbnail", AppColor.MainAccent, AppColor.SubMainAccent);

        public void SetLoadingState() => SetState(Icons.Loading, AppColor.TextSecondary, "Saving...", AppColor.Border, AppColor.AccentBackground);

        public void SetCompleteState() => SetState(Icons.Complete, AppColor.TextPrimary, "Saved", AppColor.SecondAccent, AppColor.SubSecondAccent);

        private void SetState(string img, Color textColor, string label, Color stroke, Color back) {
            _borderThumbnailButton.BackgroundColor = back;
            _borderThumbnailButton.Stroke = stroke;

            _imageThumbnailButton.Source = ImageSource.FromFile(img);
            _labelThumbnailButton.TextColor = textColor;
            _labelThumbnailButton.Text = label;
        }

    }

}
