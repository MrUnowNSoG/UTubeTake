using UTubeTake.Code.StartPage.Error;
using UTubeTake.Code.StartPage.Loading;
using UTubeTake.Code.StartPage.Video;
using UTubeTake.Code.Tools;



namespace UTubeTake.Code.StartPage {
    internal sealed class StartPage_ViewManager {

        private readonly Layout _welcomeView;
        private readonly LoadingView _loadView;
        private readonly VideoView _videoView;
        private readonly ErrorView _errorView;

        public StartPage_ViewManager(StartPage_XAMLContainer container) {
            _welcomeView = container.WelcomeView;
            _loadView = new LoadingView(container.LoadingView);
            _videoView = new VideoView(container.VideoView);
            _errorView = new ErrorView(container.ErrorView);

            ErrorHandlerService.GetInstance().OnCatchError += ShowErrorView;
            _videoView.OnLoadDataComplite += ShowVideoView;
        }
        
        public void ShowWelcomeView() {
            HideAllView();

            _welcomeView.IsVisible = true;
        }

        public void ShowErrorView(ErrorLog log) {
            HideAllView();
            //TODO: Add error view set log
            _errorView.IsVisible = true;
        }


        public void ProcessVideoView(string url) {
            HideAllView();

            _loadView.IsVisible = true;
            _dotAnimation.StartAnimation();

            _videoView.LoadView(url);   
        }

        private void ShowVideoView() {
            HideAllView();
            _videoView.Show();
        }

        private void StopLoadingAnimation() {
            if (_dotAnimation.GetAnimationState() == true) _dotAnimation.StopAnimation();
        }

        private void HideAllView() {
            StopLoadingAnimation();

            _welcomeView.IsVisible = false;
            _loadView.IsVisible = false;
            _videoView.IsVisible = false;
            _errorView.IsVisible = false;
        }


    }



    }

