using System;
using System.Collections.Generic;
using System.Text;
using UTubeTake.Code.Animation;

namespace UTubeTake.Code.StartPage {
    
    internal sealed class StartPage_WindowManager {

        private readonly StartPage_XAMLContainer _xamlContainer;
        private readonly DotAnimation _dotAnimation;

        public StartPage_WindowManager(StartPage_XAMLContainer container) {

            _xamlContainer = container;
            _dotAnimation = new DotAnimation(container.LoadingDots);

        }

        public void ShowWelcomeView() {

        }


    }

}
