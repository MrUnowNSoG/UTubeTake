using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Microsoft.Maui.Controls.Shapes;
using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.Tools;



namespace UTubeTake.Code.StartPage.Video.Presenters {
    
    internal sealed class VideoDownloaderBarPresenter {

        private readonly Border _viewBorder;
        private readonly Ellipse _ellipse;
        private readonly Label _status;
        private readonly Label _persent;
        private readonly Label _nameFile;
        private readonly ProgressBar _progressBar;

        public VideoDownloaderBarPresenter(VideoDownloaderBarElements elements) {
            _viewBorder = elements.Border;
            _ellipse = elements.Ellipse;
            _status = elements.Status;
            _persent = elements.Persent;
            _nameFile = elements.NameFile;
            _progressBar = elements.ProgressBar;
        }

        public void UpdateFileDownload(string percent, double value) {
            _persent.Text = percent;
            _progressBar.Progress = value;
        }

        public void SetDefaultState() {
            _viewBorder.IsVisible = false;
            _nameFile.Text = "";

            SetState("Downloding...", "0%", 0f, AppColor.MainAccent);
        }

        public void SetLoadingState(string nameFile, string typeFile) {
            _viewBorder.IsVisible = true;

            _nameFile.Text = nameFile + typeFile;
            SetState("Downloding...", "0%", 0f, AppColor.MainAccent);
        }

        public void SetCompleteState() {
            SetState("Done", "100%", 1f, AppColor.SecondAccent);
        }

        private void SetState(string status, string textLabel, double barProgress, Color color) {
            _status.Text = status;
            _persent.Text = textLabel;
            _progressBar.Progress = barProgress;

            _viewBorder.Stroke = color;
            _ellipse.BackgroundColor = color;
            _progressBar.ProgressColor = color;
            _persent.TextColor = color;
        }
    }

}
