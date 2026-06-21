using Microsoft.Maui.Controls.Shapes;
using UTubeTake.Code;
using UTubeTake.Code.Bootstrap;
using UTubeTake.Code.Setting;
using UTubeTake.Code.StartPage;
using UTubeTake.Code.Tools;



namespace UTubeTake;

public partial class StartPage : ContentPage {

	private StartPage_ViewManager _viewManager;
	private BootstrapContainer _container;


	public StartPage() {

		InitializeComponent();

		var xamlContainer = InitializeXAMLContainer();
        _viewManager = new StartPage_ViewManager(xamlContainer);
        _viewManager.ShowWelcomeView();

        _container = InitializeBootCoontainer(xamlContainer);

	}

    private StartPage_XAMLContainer InitializeXAMLContainer() {

		StartPage_XAMLContainer container = new StartPage_XAMLContainer {
			WelcomeView = this.WelcomeView,

			LoadongView = this.LoadingView,
			LoadingDots = new List<Ellipse> { Dot_1, Dot_2, Dot_3 },

			VideoLayout = this.VideoView,
			QualityPicker = this.PickerQuality,
			BitRatePicker = this.PickerBitRate,
			TitleVideoLabel = this.VideoNameLabel,
			AuthorVideoLabel = this.VideoAuthorLabel,
			DurationVideoLabel = this.VideoDurationLabel,
			SizeVideoLabel = this.VideoSizeLabel,
			ThumbnailImage = this.ThumbnailImage,
			ThumbnailButtonBorder = this.ThumbnailButtonBorder,
			ThumbnailButtonImage = this.ThumbnailButtonImage,
			ThumbnailButtonLabel = this.ThumbnailButtonLabel,

			ErrorView = this.ErrorView,
			ErrorCodeLabel = this.ErrorCodeLabel,
			ErrorResolveLabel = this.ErrorResolveLabel,
		};

		return container;

	}
    private BootstrapContainer InitializeBootCoontainer(StartPage_XAMLContainer container) {

        SettingStatic.LoadSetting();

        Bootstrap boot = new Bootstrap(container);
		return boot.Initialize();
    }


    private void FindVideoEvent(object sender, System.EventArgs e) {

        if (StaticFlags.downloadInfo == true) return;

        Button button = (Button)sender;
		string url = linkEntry.Text;
		
		if(_container.LinkTest.testUrl(ref url) == true) {
            button.Text = "Find!";
			_viewManager.ProcessVideoView(url);
        } else {}

	}

	private async void DownloadVideoEvent(object sender, EventArgs e) {

		if (StaticFlags.downloadFile == false) {

			StaticFlags.downloadFile = true;

			IProgress<double> progress = new Progress<double>(GetPercentVideo);

            try {
                await _container.VideoDownloader.DownloadVideo(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex, progress);
            
			} catch (Exception ex) {
                _container.ErrorHandlService.CathcError(ex);
                _viewManager.ShowErrorView();
			}

			StaticFlags.downloadFile = false;
		}
	}

	private void GetPercentVideo(double d) {
        DownloadVideoLabel.Text = Math.Truncate(d * 100).ToString() + "%";

		if (Math.Truncate(d * 100) >= 99) DownloadVideoLabel.Text = "Download";
    }

	private void DownloadImg(object sender, EventArgs e) {

		if (StaticFlags.downloadImg == false) {
			string link = linkEntry.Text;

			if (link != null && link != "" && _container.LinkTest.testUrl(ref link)) {

				StaticFlags.downloadImg = true;
				//_container.AvatarVideoService.DownloadImg(link, _container.VideoVariable.video.Title.ToString(), SettingStatic.pathForImage);
                _container.AvatarVideoService.DownloadImg(link, "Image", SettingStatic.pathForImage);

            }
        }
	}

	

    private void PickerBitRate_SelectedIndexChanged(object sender, EventArgs e) => UpdateVideoSize();
    private void PickerQuality_SelectedIndexChanged(object sender, EventArgs e) => UpdateVideoSize();

    private void UpdateVideoSize() {
		string sizeVideo = _container.VideoInformer.GetSizeFile(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex);
        _container.VideoUiUpdater.UpdateSizeVideo(sizeVideo);
	}


    private async void SettingPageEvent(object sender, EventArgs eventArgs) {
        await Navigation.PushModalAsync(new SettingPage(), false);
    }

}