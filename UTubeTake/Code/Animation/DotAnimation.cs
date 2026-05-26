using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace UTubeTake.Code.Animation {

    internal class DotAnimation {

        private const float MIN_SIZE = 0.5f;
        private const float MAX_SIZE = 1.0f;

        private List<Ellipse> _dots;
        private CancellationTokenSource _cancellToken;

        public DotAnimation(params Ellipse[] dots) {

            _dots = new List<Ellipse>();
            _cancellToken = new CancellationTokenSource();

            foreach (Ellipse dot in dots) { 
                _dots.Add(dot);
            }

        }

        public void StartAnimation() {
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
            _cancellToken.Cancel();
            _cancellToken = new CancellationTokenSource();
        }

    }

}
