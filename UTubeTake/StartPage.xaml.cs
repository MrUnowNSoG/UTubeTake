using Microsoft.Maui.Controls.Shapes;
using UTubeTake.Code;
using UTubeTake.Code.Animation;
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
		var container = InitializeWindowComponent();
		InitializeOwnComponent(container);

	}

	private StartPage_XAMLContainer InitializeWindowComponent() {

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

			ErrorView = this.ErrorView

		};

		StartPage_WindowManager windowManager = new StartPage_WindowManager(container);
		windowManager.ShowWelcomeView();

		return container;

	}

    private void InitializeOwnComponent(StartPage_XAMLContainer container) {

        SettingStatic.LoadSetting();

        Bootstrap boot = new Bootstrap(container);
		_container = boot.Initialize();
    }

    private void FindVideo(object sender, System.EventArgs e) {

		Button button = (Button)sender;
		string link = linkEntry.Text;

		if (StaticFlags.downloadInfo == true) return;
		
		if(_container.LinkTest.testUrl(ref link) == true) {
            button.Text = "Find!";
			Wait(link);

        } else {
            VideoView.IsVisible = false;

            //_container.DotAnimation.StopAnimation();
			LoadingView.IsVisible = false;

        }

	}

	private async void Wait(string link) {

        StaticFlags.downloadInfo = true;

        //_container.DotAnimation.StartAnimation();
        LoadingView.IsVisible = true;
        VideoView.IsVisible = false;

        imageVideo.Source = _container.AvatarVideoService.GetImgVideoUrl(link);
        await DownloadInfoForVideo(link);

        //_container.DotAnimation.StopAnimation();
        LoadingView.IsVisible = false;

        VideoView.IsVisible = true;

        StaticFlags.downloadInfo = false;

    }

	private async Task DownloadInfoForVideo(string url) {

		VideoInformer informer = _container.VideoInformer;

		await informer.LoadInfo(url);
		await informer.LoadInfoForPicker(url);
		
        _container.VideoUiUpdater.UpdateTextVideo(informer.GetInfoForVideo());
        _container.VideoUiUpdater.UpdatePicker(informer.GetQualityList(), informer.GetBitRateList());

		UpdateVideoSize();

	}

	//Download Button
	private async void DownloadVideo(object sender, EventArgs e) {

		if (StaticFlags.downloadFile == false) {

			StaticFlags.downloadFile = true;

			IProgress<double> progress = new Progress<double>(GetPercentVideo);
			await _container.VideoDownloader.DownloadVideo(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex, progress);
		
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
			//	_container.AvatarVideoService.DownloadImg(link, _container.VideoVariable.video.Title.ToString(), SettingStatic.pathForImage);
			}
		}
	}

	//Size video
    private void PickerBitRate_SelectedIndexChanged(object sender, EventArgs e) {
		UpdateVideoSize();
    }

    private void PickerQuality_SelectedIndexChanged(object sender, EventArgs e) {
		UpdateVideoSize();
    }

	private void UpdateVideoSize() {
		string sizeVideo = _container.VideoInformer.GetSizeFile(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex);
        _container.VideoUiUpdater.UpdateSizeVideo(sizeVideo);
	}


    //Pages
    private async void SettingPage(object sender, EventArgs eventArgs) {
        await Navigation.PushModalAsync(new SettingPage(), false);
    }

}