using UTubeTake.Code.Setting;
using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.StartPage.Video.Presenters;
using UTubeTake.Code.Tools;
using UTubeTake.Code.Tools.ErrorHandler;
using UTubeTake.Code.VideoManger;



namespace UTubeTake.Code.StartPage.Video {

    internal sealed class VideoViewPresenter {

        private readonly VideoManager _videoManger;
        private readonly ThumbnailVideoService _thumbnailService;

        private readonly Layout _view;
        private readonly VideoTitlePresenter _title;
        private readonly VideoThumbnailPresenter _thumbnail;
        private readonly VideoPickerPresenter _pickers;
        private readonly VideoDownloaderBarPresenter _bar;

        private string _currentVideo;

        public event Action OnLoadDataComplite;

        public VideoViewPresenter(VideoViewElements elements) {
            _videoManger = new VideoManager();
            _thumbnailService = new ThumbnailVideoService();

            _view = elements.View;
            _title = new VideoTitlePresenter(elements.Title);
            _thumbnail = new VideoThumbnailPresenter(elements.Thumbnail);
            _pickers = new VideoPickerPresenter(elements.Picker);
            _bar = new VideoDownloaderBarPresenter(elements.Bar);
        }



        public async Task LoadView(string url) {

            if (StaticFlags.downloadInfo == true) return;

            StaticFlags.downloadInfo = true;
            _currentVideo = url;

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
             string? thumbnail = _thumbnailService.GetThumbnailUrl(url);
            _thumbnail.SetThumbnail(thumbnail);
        }

        public void Show() {
            var title = _videoManger.BuildTitleData();
            _title.SetTitleData(title);

            _pickers.SetPickers(_videoManger.BuildQualityList(), _videoManger.BuildBitRateList());
            
            UpdateVideoSize();

            _view.IsVisible = true;
        }

        public void UpdateVideoSize() {
            var index = _pickers.GetCurrentSelect();
            string size = _videoManger.BuildFileSize(index.Item1, index.Item2);

            _title.SetSizeFile(size);
        }

        public void Hide() {
            _view.IsVisible = false;
            ResetViewEffect();
        }




        public async void DownloadThumbnail() {
            if(StaticFlags.downloadImg == true) return;
            if(string.IsNullOrEmpty(_currentVideo) == true) return;

            StaticFlags.downloadImg = true;
            _thumbnail.SetLoadingState();

            string nameFile = StringHelper.RemoveInvalidPathChars(_videoManger.GetFileName());
            bool res = await _thumbnailService.DownloadThumbnail(_currentVideo, nameFile, SettingStatic.pathForImage);

            if(res == true) _thumbnail.SetCompleteState();
            StaticFlags.downloadImg = false;
        }

        public async void DownloadFile() {

            if (StaticFlags.downloadFile == true) return;
            StaticFlags.downloadFile = true;

            bool back = false;
            IProgress<double> progress = new Progress<double>(ProgressFileDownload);
            var index = _pickers.GetCurrentSelect();

            string nameFile = StringHelper.RemoveInvalidPathChars(_videoManger.GetFileName());
            string typeFile = _videoManger.DefinityTypeFile(index.Item1, index.Item2);

            ShowLoadingState(nameFile, typeFile);

            try {
                back = await _videoManger.DownloadVideo(nameFile, SettingStatic.pathForVideo, index.Item1, index.Item2, progress);

            } catch (Exception ex) {
                ErrorHandlerService.GetInstance().CathcError(ex);
            }

            if (back == true) SetCompleteState();
            StaticFlags.downloadFile = false;
        }

        private void ShowLoadingState(string fileName, string fileType) {
            _pickers.SetLoadingState();
            _bar.SetLoadingState(fileName, fileType);
        }

        private void ProgressFileDownload(double value) {
            string percent = Math.Truncate(value * 100).ToString() + "%";
            _pickers.UpdateFileDownload(percent);
            _bar.UpdateFileDownload(percent, value);
        }

        private void SetCompleteState() {
            _pickers.SetCompleteState();
            _bar.SetCompleteState();
        }

        private void ResetViewEffect() {
            _thumbnail.SetDefaultState();
            _pickers.SetDefaultState();
            _bar.SetDefaultState();
        }

    }
}
