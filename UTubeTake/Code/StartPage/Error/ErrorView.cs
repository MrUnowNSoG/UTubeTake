using UTubeTake.Code.Tools.ErrorHandler;


namespace UTubeTake.Code.StartPage.Error {
    
    internal sealed class ErrorView {

        private readonly Border _view;
        private readonly Label _errorLabel;
        private readonly Label _resolveLabel;

        public ErrorView(ErrorViewElements elements) {
            _view = elements.View;
            _errorLabel = elements.Code;
            _resolveLabel = elements.Resolve;
        }

        public void Show(ErrorLog log) {
            _errorLabel.Text = log.Message;
            _resolveLabel.Text = log.Resolve;

            _view.IsVisible = true;
        }

        public void Hide() {
            _view.IsVisible = false;
        }

    }

}
