using System;
using System.Collections.Generic;
using System.Text;

namespace UTubeTake.Code.StartPage.Error {
    
    internal sealed class ErrorView {

        private readonly Layout _view;
        private readonly Label _errorLabel;
        private readonly Label _resolveLabel;

        public ErrorView(ErrorViewEelemetns elements) {
            _view = elements.View;
            _errorLabel = elements.Code;
            _resolveLabel = elements.Resolve;
        }

    }

}
