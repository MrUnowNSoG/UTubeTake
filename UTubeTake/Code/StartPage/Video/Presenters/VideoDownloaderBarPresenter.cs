using Microsoft.Maui.Controls.Shapes;
using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.Tools;



namespace UTubeTake.Code.StartPage.Video.Presenters {
    
    internal sealed class VideoDownloaderBarPresenter {

        private readonly Border _viewBorder;
        private readonly Ellipse _ellipse;
        private readonly Label _status;
        private readonly Label _percent;
        private readonly Label _nameFile;
        private readonly ProgressBar _progressBar;

        public VideoDownloaderBarPresenter(VideoDownloaderBarElements elements) {
            _viewBorder = elements.Border;
            _ellipse = elements.Ellipse;
            _status = elements.Status;
            _percent = elements.Percent;
            _nameFile = elements.NameFile;
            _progressBar = elements.ProgressBar;
        }

        public void UpdateFileDownload(string percent, double value) {
            _percent.Text = percent;
            _progressBar.Progress = value;
        }

        public void SetDefaultState() {
            _viewBorder.IsVisible = false;
            _nameFile.Text = "";

            SetState("Downloading...", "0%", 0f, AppColor.MainAccent);
        }

        public void SetLoadingState(string nameFile, string typeFile) {
            _viewBorder.IsVisible = true;

            _nameFile.Text = nameFile + typeFile;
            SetState("Downloading...", "0%", 0f, AppColor.MainAccent);
        }

        public void SetCompleteState() {
            SetState("Done", "100%", 1f, AppColor.SecondAccent);
        }

        private void SetState(string status, string textLabel, double barProgress, Color color) {
            _status.Text = status;
            _percent.Text = textLabel;
            _progressBar.Progress = barProgress;

            _viewBorder.Stroke = color;
            _ellipse.BackgroundColor = color;
            _progressBar.ProgressColor = color;
            _percent.TextColor = color;
        }
    }

}
