using AngleSharp.Dom;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Text;
using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.Tools;
using UTubeTake.Code.VideoManger;

namespace UTubeTake.Code.StartPage.Video {
    internal sealed class VideoView {

        private readonly VideoManager _videoManger;
        private readonly ThumbnailVideoService _thumbnailService;

        private readonly Layout _videoLayout;

        private readonly Label _titleVideo;
        private readonly Label _authorVideo;
        private readonly Label _durationVideo;
        private readonly Label _sizeVideo;

        private readonly Image _imageThumbnail;
        private readonly Border _borderThumbnailButton;
        private readonly Label _labelThumbnailButton;
        private readonly Image _imageThumbnailButton;

        public event Action OnLoadDataComplite;

        public VideoView(VideoViewElements videoView) {
            _videoManger = new VideoManager();
            _thumbnailService = new ThumbnailVideoService();

            _videoLayout = container.VideoLayout;

            _titleVideo = container.TitleVideoLabel;
            _authorVideo = container.AuthorVideoLabel;
            _durationVideo = container.DurationVideoLabel;
            _sizeVideo = container.SizeVideoLabel;

            _imageThumbnail = container.ThumbnailImage;
            _borderThumbnailButton = container.ThumbnailButtonBorder;
            _labelThumbnailButton = container.ThumbnailButtonLabel;
            _imageThumbnailButton = container.ThumbnailButtonImage;
        }

        public async Task LoadView(string url) {

            StaticFlags.downloadInfo = true;

            try {
                LoadThumbnail(url);
                await _videoManger.DownloadVideoData(url);

            } catch (Exception error) {
                ErrorHandlerService.GetInstance().CathcError(error);
                StaticFlags.downloadInfo = false;
                return;
            }

            OnLoadDataComplite?.Invoke();
            StaticFlags.downloadInfo = false;
            return;

        }

        private void LoadThumbnail(string url) {
            string? thumnailUrl = _thumbnailService.GetThumbnailUrl(url);

            _imageThumbnail.Source = thumnailUrl != null
                ? ImageSource.FromUri(new Uri(thumnailUrl))
                : ImageSource.FromFile("video_base_icon.png");
        }

        public void Show() {
            ResetViewEffect();

            var title = _videoManger.BuildTitleData();
            _titleVideo.Text = title.Title;
            _authorVideo.Text = title.Author;
            _durationVideo.Text = title.Duration;

            _container.VideoUiUpdater.UpdatePicker(informer.GetQualityList(), informer.GetBitRateList());

            UpdateVideoSize();
        }

        public void Hide() {

        }

        private void ResetViewEffect() {

        }

    }
}
