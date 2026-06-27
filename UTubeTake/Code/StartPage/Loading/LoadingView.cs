namespace UTubeTake.Code.StartPage.Loading {
    
    internal sealed class LoadingView {

        private readonly Layout _view;
        private readonly DotAnimation _animation;

        public LoadingView(LoadingViewElements elements) {
            _view = elements.View;
            _animation = new DotAnimation(elements.Ellipse.ToList());
        }

        public void Show() {
            _animation.StartAnimation();
            _view.IsVisible = true;
        }

        public void Hide() {
            _animation.StopAnimation();
            _view.IsVisible = false;
        }

    }

}
