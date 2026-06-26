using UTubeTake.Code.StartPage.Error;
using UTubeTake.Code.StartPage.Loading;
using UTubeTake.Code.StartPage.Video;
using UTubeTake.Code.Tools.ErrorHandler;



namespace UTubeTake.Code.StartPage {

    internal sealed class StartPageViewManager {

        private readonly Layout _welcomeView;
        private readonly LoadingView _loadView;
        private readonly VideoViewPresenter _videoView;
        private readonly ErrorView _errorView;

        public StartPageViewManager(StartPageXamlContainer container) {
            _welcomeView = container.WelcomeView;
            _loadView = new LoadingView(container.LoadingView);
            _videoView = new VideoViewPresenter(container.VideoView);
            _errorView = new ErrorView(container.ErrorView);

            ErrorHandlerService.GetInstance().OnCatchError += ShowErrorView;
            _videoView.OnLoadDataComplete += ShowVideoView;
        }
        
        public void ShowWelcomeView() {
            HideAllView();
            _welcomeView.IsVisible = true;
        }

        public void ShowErrorView(ErrorLog log) {
            HideAllView();
            _errorView.Show(log);
        }

        public async void ProcessVideoView(string url) {
            HideAllView();
            _loadView.Show();

            await _videoView.LoadView(url);   
        }

        private void ShowVideoView() {
            HideAllView();
            _videoView.Show();
        }

        public void UpdateVideoSize() => _videoView.UpdateVideoSize();

        public async Task DownloadThumbnail() => await _videoView.DownloadThumbnail();

        public async Task DownloadFile() => await _videoView.DownloadFile();

        private void HideAllView() {
            _welcomeView.IsVisible = false;
            _loadView.Hide();
            _videoView.Hide();
            _errorView.Hide();
        }

    }

}

