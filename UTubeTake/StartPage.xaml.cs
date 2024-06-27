using Microsoft.Maui.Graphics.Platform;
using UTubeTake.Code;

namespace UTubeTake;

public partial class StartPage : ContentPage {

	private LinkTest _linkTest;
	private ImgVideoDownload _imgVideoDownload;
	
	private VideoVariable _videoVariable;
	private VideoInformer _videoInformer;
	private VideoUiUpdater _videoUiUpdater;
	private VideoDownloader _videoDownloader;

	private bool _loadInfo = false;
	private bool _loadImg = false;
	private bool _loadMedia = false;

	public StartPage() {
		InitializeComponent();

		//
		SettingStatic.LoadSetting();

		//FirstStage
		_linkTest = new LinkTest();
		_imgVideoDownload = new ImgVideoDownload();

		//SecondStage
		_videoVariable = VideoVariable.Instanite();

		_videoInformer = new VideoInformer(_videoVariable);
		_videoUiUpdater = new VideoUiUpdater(PickerQuality, PickerBitRate, VideoNameText, VideoAuthorText, VideoDurationText, VideoSizeText);
		
		_videoDownloader = new VideoDownloader(_videoVariable);
	}


	private void FindVideo(object sender, System.EventArgs e) {
		
		//Variables
		Button button = (Button)sender;
		string link = linkEntry.Text;

		//
		if(_linkTest.testUrl(ref link) && _loadInfo == false) {

			imageVideo.Source = _imgVideoDownload.GetImgVideoUrl(link);
			DownloadInfoForVideo(link);
			
			
        } else {
            button.Text = "Linc can't recognize";
        }
	}

	private async void DownloadInfoForVideo(string url) {

		_loadInfo = true;

		Task video = _videoInformer.LoadInfo(url);
		Task picker = _videoInformer.LoadInfoForPicker(url);

		await video;
		_videoUiUpdater.UpdateTextVideo(_videoInformer.GetInfoForVideo());

		await picker;
		_videoUiUpdater.UpdatePicker(_videoInformer.GetQualityList(), _videoInformer.GetBitRateList());

		UpdateVideoSize();

		_loadInfo = false;
	}

	//Download Button
	private async void DownloadVideo(object sender, EventArgs e) {

		if (StaticFlags.downloadFile == false) {

			StaticFlags.downloadFile = true;

			VideoSizeText.Text = PickerQuality.SelectedIndex.ToString();
			await _videoDownloader.DownloadVideo(PickerQuality.SelectedIndex, PickerBitRate.SelectedIndex);
		
			StaticFlags.downloadFile = false;
		}
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

	private async void HistoryPage(object sender, EventArgs e) {
		await Navigation.PushModalAsync(new HistoryPage(), false);
	}
}