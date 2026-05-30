using Microsoft.Maui.Controls.Shapes;
using UTubeTake.Code;
using UTubeTake.Code.Bootstrap;
using UTubeTake.Code.Setting;
using UTubeTake.Code.StartPage;
using UTubeTake.Code.Tools;



namespace UTubeTake;

public partial class StartPage : ContentPage {

	private StartPage_WindowManager _windowManager;
	private BootstrapContainer _container;


	public StartPage() {

		InitializeComponent();

		var xamlContainer = InitializeXAMLContainer();
        _windowManager = new StartPage_WindowManager(xamlContainer);
        _windowManager.ShowWelcomeView();

        _container = InitializeBootCoontainer(xamlContainer);

	}

	private StartPage_XAMLContainer InitializeXAMLContainer() {

		StartPage_XAMLContainer container = new StartPage_XAMLContainer {
			WelcomeView = this.WelcomeView,

			LoadongView = this.LoadingView,
			LoadingDots = new List<Ellipse> { Dot_1, Dot_2, Dot_3 },

			VideoView = this.VideoView,
			QualityPicker = this.PickerQuality,
			BitRatePicker = this.PickerBitRate,
			NameVideoLabel = this.VideoNameLabel,
			AuthorVideoLabel = this.VideoAuthorLabel,
			DurationVideoLabel = this.VideoDurationLabel,
			SizeVideoLabel = this.VideoSizeLabel,

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

		Button button = (Button)sender;
		string link = linkEntry.Text;

		if (StaticFlags.downloadInfo == true) return;
		
		if(_container.LinkTest.testUrl(ref link) == true) {
            button.Text = "Find!";
            StartVideoProcessing(link);

        } else {}

	}

	private async void StartVideoProcessing(string link) {

		_windowManager.ShowLoadingView();
        StaticFlags.downloadInfo = true;
    
        imageVideo.Source = _container.AvatarVideoService.GetImgVideoUrl(link);

		try {

			await VideoProcessing(link);

		} catch (Exception ex) {
			_container.ErrorHandlService.CathcError(ex);
			_windowManager.ShowErrorView();

            StaticFlags.downloadInfo = false;
            return;
        }

        StaticFlags.downloadInfo = false;
		_windowManager.ShowVideoView();

    }

	private async Task VideoProcessing(string url) {

		VideoInformer informer = _container.VideoInformer;

		await informer.LoadInfo(url);
		await informer.LoadInfoForPicker(url);
		
        _container.VideoUiUpdater.UpdateTextVideo(informer.GetInfoForVideo());
        _container.VideoUiUpdater.UpdatePicker(informer.GetQualityList(), informer.GetBitRateList());

		UpdateVideoSize();

	}

	private async void DownloadVideoEvent(object sender, EventArgs e) {

		if (StaticFlags.downloadFile == false) {

			StaticFlags.downloadFile = true;

			IProgress<double> progress = new Progress<double>(GetPercentVideo);

            try {
                await _container.VideoDownloader.DownloadVideo(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex, progress);
            
			} catch (Exception ex) {
                _container.ErrorHandlService.CathcError(ex);
                _windowManager.ShowErrorView();
			}

			StaticFlags.downloadFile = false;
		}
	}

	private void GetPercentVideo(double d) {
        DownloadVideoButton.Text = Math.Truncate(d * 100).ToString() + "%";

		if (Math.Truncate(d * 100) >= 99) DownloadVideoButton.Text = "Download";
    }

	private void DownloadImg(object sender, EventArgs e) {

		if (StaticFlags.downloadImg == false) {
			string link = linkEntry.Text;

			if (link != null && link != "" && _container.LinkTest.testUrl(ref link)) {

				StaticFlags.downloadImg = true;
				//_container.AvatarVideoService.DownloadImg(link, _container.VideoVariable.video.Title.ToString(), SettingStatic.pathForImage);
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