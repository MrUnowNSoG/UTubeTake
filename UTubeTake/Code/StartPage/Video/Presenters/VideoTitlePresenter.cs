using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.VideoManger.VideoData;



namespace UTubeTake.Code.StartPage.Video.Presenters {

    internal sealed class VideoTitlePresenter {

        private readonly Label _titleVideo;
        private readonly Label _authorVideo;
        private readonly Label _durationVideo;
        private readonly Label _sizeVideo;

        public VideoTitlePresenter(VideoTitleElements elements) {
            _titleVideo = elements.Title;
            _authorVideo = elements.Author;
            _durationVideo = elements.Duration;
            _sizeVideo = elements.Size;
        }

        public void SetTitleData(VideoTitleData data) {
            _titleVideo.Text = data.Title;
            _authorVideo.Text = data.Author;
            _durationVideo.Text = data.Duration;
        }

        public void SetSizeFile(string size) => _sizeVideo.Text = size;


    }
}
