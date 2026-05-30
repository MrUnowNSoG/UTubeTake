using UTubeTake.Code.Animation;



namespace UTubeTake.Code.StartPage {
    
    internal sealed class StartPage_WindowManager {

        private readonly Layout _welcomeView;
        private readonly Layout _loadView;
        private readonly Layout _videoView;
        private readonly Layout _errorView;

        private readonly DotAnimation _dotAnimation;

        public StartPage_WindowManager(StartPage_XAMLContainer container) {


            _welcomeView = container.WelcomeView;
            _loadView = container.LoadongView;
            _videoView = container.VideoView;
            _errorView = container.ErrorView;

            _dotAnimation = new DotAnimation(container.LoadingDots);

        }

        public void ShowWelcomeView() {
            StopLoadingAnimation();
            HideAllView();

            _welcomeView.IsVisible = true;
        }

        public void ShowLoadingView() {
            HideAllView();

            _loadView.IsVisible = true;
            _dotAnimation.StartAnimation();
        }

        public void ShowVideoView() {
            StopLoadingAnimation();
            HideAllView();

            _videoView.IsVisible = true;
        }

        public void ShowErrorView() {
            StopLoadingAnimation();
            HideAllView();

            _errorView.IsVisible = true;
        }

        private void StopLoadingAnimation() {
            if (_dotAnimation.GetAnimationState() == true) _dotAnimation.StopAnimation();
        }

        private void HideAllView() {
            _welcomeView.IsVisible = false;
            _loadView.IsVisible = false;
            _videoView.IsVisible = false;
            _errorView.IsVisible = false;
        }

    }

}
