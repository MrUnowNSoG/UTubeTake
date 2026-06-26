using UTubeTake.Code.Setting;
using UTubeTake.Code.StartPage;
using UTubeTake.Code.StartPage.Error;
using UTubeTake.Code.StartPage.Loading;
using UTubeTake.Code.StartPage.Video.Elements;
using UTubeTake.Code.Tools;
using UTubeTake.Code.Tools.ErrorHandler;



namespace UTubeTake;

public partial class StartPage : ContentPage {

	private StartPage_ViewManager _viewManager;


	public StartPage() {

		InitializeComponent();

		SettingStatic.LoadSetting();

		var xamlContainer = BuildXAMLContainer();
        _viewManager = new StartPage_ViewManager(xamlContainer);
        _viewManager.ShowWelcomeView();

	}

    private StartPage_XAMLContainer BuildXAMLContainer() {

		var loadingView = new LoadingViewElements(LoadingView, Dot_1, Dot_2, Dot_3);


		var videoTitle = new VideoTitleElements(VideoNameLabel, VideoAuthorLabel, 
												VideoDurationLabel, VideoSizeLabel);

		var videoThumbnail = new VideoThumbnailElements(ThumbnailImage, ThumbnailButtonBorder, ThumbnailButtonImage, 
														ThumbnailButtonLabel);

		var videoPicker = new VideoPickerElements(VideoQualityPicker, VideoBitratePicker, VideoPickerButtonBorder, 
												  VideoPickerButtonImage, VideoPickerButtonLabel);

        var videoBar = new VideoDownloaderBarElements(BarBorder, BarEllipse, BarStatusLabel, BarPercentLabel, BarNameFileLabel, BarProgressBar);
        
		var videoView = new VideoViewElements(VideoView, videoTitle, videoThumbnail, videoPicker, videoBar);


		var errorView = new ErrorViewEelemetns(ErrorView, ErrorCodeLabel, ErrorResolveLabel);


		StartPage_XAMLContainer container = new StartPage_XAMLContainer {
			WelcomeView = this.WelcomeView,

			LoadingView = loadingView,
			VideoView = videoView,
			ErrorView = errorView
		};

		return container;

	}

    private void FindVideoEvent(object sender, EventArgs e) {

        if (StaticFlags.downloadInfo == true) return;

		string? url = StringHelper.ValidationUrl(linkEntry.Text);
		
		if(url != null) {
			_viewManager.ProcessVideoView(url);
        } else {
			ErrorHandlerService.GetInstance().CathcError(new Exception("Bad link!"));
		}

	}



    private void DownloadThumbnailEvent(object sender, EventArgs e) {

		if (StaticFlags.downloadImg == false)
					_viewManager.DownloadThumbnail();

    }

    private async void DownloadVideoEvent(object sender, EventArgs e) {

		if (StaticFlags.downloadFile == false)
			_viewManager.DownloadFile();
	}



    private void PickerBitRate_SelectedIndexChanged(object sender, EventArgs e) => UpdateVideoSize();
    private void PickerQuality_SelectedIndexChanged(object sender, EventArgs e) => UpdateVideoSize();
	private void UpdateVideoSize() => _viewManager.UpdateVideoSize();


    private void TryAgainEvent(object sender, EventArgs e) => FindVideoEvent(sender, e);
    private void DismissErrorEvent(object sender, EventArgs e) => _viewManager.ShowWelcomeView();


    private async void SettingPageEvent(object sender, EventArgs eventArgs) {
        await Navigation.PushModalAsync(new SettingPage(), false);
    }

}