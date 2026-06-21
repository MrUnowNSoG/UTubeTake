using UTubeTake.Code.Animation;
using UTubeTake.Code.StartPage.View;
using UTubeTake.Code.Tools;



namespace UTubeTake.Code.StartPage {
    internal sealed class StartPage_ViewManager {

        private readonly Layout _welcomeView;
        private readonly Layout _loadView;
        private readonly VideoView _videoView;
        private readonly Layout _errorView;

        private readonly DotAnimation _dotAnimation;

        public StartPage_ViewManager(StartPage_XAMLContainer container) {
            _welcomeView = container.WelcomeView;
            _loadView = container.LoadongView;
            _videoView = new VideoView(container);
            _errorView = container.ErrorView;

            _dotAnimation = new DotAnimation(container.LoadingDots);

            Subscribe();
        }

        private void Subscribe() {
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

}
