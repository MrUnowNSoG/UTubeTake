using UTubeTake.Code;


namespace UTubeTake;

public partial class StartPage : ContentPage {

	private LinkTest _linkTest;
	private ImgVideoDownload _imgVideoDownload;
	
	private VideoVariable _videoVariable;
	private VideoInformer _videoInformer;
	private VideoUiUpdater _videoUiUpdater;
	private VideoDownloader _videoDownloader;

	private DotAnimation _dotAnimation;

	public StartPage() {
		InitializeComponent();

		SettingStatic.LoadSetting();

        CreateOwnComponent();

		VideoView.IsVisible = false;
        LoadingView.IsVisible = false;
	}

    private void CreateOwnComponent() {
        _linkTest = new LinkTest();
        _imgVideoDownload = new ImgVideoDownload();

        _videoVariable = VideoVariable.Instanite();

        _videoInformer = new VideoInformer(_videoVariable);
        _videoUiUpdater = new VideoUiUpdater(PickerQuality, PickerBitRate, VideoNameText, VideoAuthorText, VideoDurationText, VideoSizeText);

        _videoDownloader = new VideoDownloader(_videoVariable);

        _dotAnimation = new DotAnimation(Dot_1, Dot_2, Dot_3);
    }

    private void FindVideo(object sender, System.EventArgs e) {

		Button button = (Button)sender;
		string link = linkEntry.Text;

		if (StaticFlags.downloadInfo == true) return;
		
		if(_linkTest.testUrl(ref link) == true) {
            button.Text = "Find!";
			Wait(link);

        } else {
            VideoView.IsVisible = false;

			_dotAnimation.StopAnimation();
			LoadingView.IsVisible = false;

        }

	}

	private async void Wait(string link) {

        StaticFlags.downloadInfo = true;

        _dotAnimation.StartAnimation();
        LoadingView.IsVisible = true;
        VideoView.IsVisible = false;

        imageVideo.Source = _imgVideoDownload.GetImgVideoUrl(link);
        await DownloadInfoForVideo(link);

        _dotAnimation.StopAnimation();
        LoadingView.IsVisible = false;

        VideoView.IsVisible = true;

        StaticFlags.downloadInfo = false;

    }

	private async Task DownloadInfoForVideo(string url) {

		await _videoInformer.LoadInfo(url);
		await _videoInformer.LoadInfoForPicker(url);
		
        _videoUiUpdater.UpdateTextVideo(_videoInformer.GetInfoForVideo());
        _videoUiUpdater.UpdatePicker(_videoInformer.GetQualityList(), _videoInformer.GetBitRateList());

		UpdateVideoSize();

	}

	//Download Button
	private async void DownloadVideo(object sender, EventArgs e) {

		if (StaticFlags.downloadFile == false) {

			StaticFlags.downloadFile = true;

			IProgress<double> progress = new Progress<double>(GetPercentVideo);
			await _videoDownloader.DownloadVideo(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex, progress);
		
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

			if (link != null && link != "" && _linkTest.testUrl(ref link)) {

				StaticFlags.downloadImg = true;
				_imgVideoDownload.DownloadImg(link, _videoVariable.video.Title.ToString(), SettingStatic.pathForImage);
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

	private void UpdateVideoSize() => _videoUiUpdater.UpdateSizeVideo(_videoInformer.GetSizeFile(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex));


    //Pages
    private async void SettingPage(object sender, EventArgs eventArgs) {
        await Navigation.PushModalAsync(new SettingPage(), false);
    }

}