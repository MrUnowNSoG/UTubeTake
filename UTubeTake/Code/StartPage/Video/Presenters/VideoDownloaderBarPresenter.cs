using Microsoft.Maui.Controls.Shapes;
using UTubeTake.Code.StartPage.Video.Elements;



namespace UTubeTake.Code.StartPage.Video.Presenters {
    
    internal sealed class VideoDownloaderBarPresenter {

        private readonly VisualElement _view;
        private readonly Border _viewBorder;
        private readonly Ellipse _ellipse;
        private readonly Label _status;
        private readonly Label _persent;
        private readonly Label _nameFile;
        private readonly ProgressBar _progressBar;

        public VideoDownloaderBarPresenter(VideoDownloaderBarElements elements) {
            _view = elements.View;
            _viewBorder = elements.Border;
            _ellipse = elements.Ellipse;
            _status = elements.Status;
            _persent = elements.Persent;
            _nameFile = elements.NameFile;
            _progressBar = elements.ProgressBar;
        }

    }

}
