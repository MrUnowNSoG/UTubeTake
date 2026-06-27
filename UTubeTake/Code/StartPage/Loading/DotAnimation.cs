using Microsoft.Maui.Controls.Shapes;



namespace UTubeTake.Code.StartPage.Loading {

    internal class DotAnimation {

        private const float MIN_SIZE = 0.5f;
        private const float MAX_SIZE = 1.0f;

        private readonly List<Ellipse> _dots;

        private CancellationTokenSource _cancellToken;
        private bool _animationState;
        public bool GetAnimationState() => _animationState;

        public DotAnimation(List<Ellipse> dots) {
            _dots = dots;
            _cancellToken = new CancellationTokenSource();
            _animationState = false;
        }

        public void StartAnimation() {

            if (_animationState == true) return;

            _animationState = true;

            foreach (Ellipse dot in _dots) {
                dot.Scale = MIN_SIZE;
            }

            Animation(_cancellToken.Token);
        }

        private async void Animation(CancellationToken token) {

            while (token.IsCancellationRequested == false) {

                foreach (Ellipse dot in _dots) {
                    await dot.ScaleToAsync(MAX_SIZE, 200, Easing.CubicOut);
                    await dot.ScaleToAsync(MIN_SIZE, 200, Easing.CubicIn);
                    await Task.Delay(100);
                }

            }
         
        }

        public void StopAnimation() {

            if (_animationState == true) {
                _cancellToken.Cancel();
                _cancellToken = new CancellationTokenSource();
            
                _animationState = false;
            }

        }

    }

}
