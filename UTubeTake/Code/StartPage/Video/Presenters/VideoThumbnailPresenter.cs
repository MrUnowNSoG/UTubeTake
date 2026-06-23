using UTubeTake.Code.StartPage.Video.Elements;



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
                : ImageSource.FromFile("video_base_icon.png");
        }

    }

}
