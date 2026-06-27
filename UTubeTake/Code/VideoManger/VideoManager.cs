using UTubeTake.Code.VideoManger.VideoData;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;



namespace UTubeTake.Code.VideoManger {

    internal sealed class VideoManager {

        private readonly YoutubeClient _client;
        private readonly VideoDataProvider _dataProvider;
        private readonly VideoFileDownloader _downloader;

        public VideoManager() {
            _client = new YoutubeClient();
            _dataProvider = new VideoDataProvider(_client);
            _downloader = new VideoFileDownloader(_client);
        }

        public async Task DownloadVideoData(string url) {
            _dataProvider.SetVideo(url);
            await _dataProvider.LoadVideoContext();
            await _dataProvider.LoadVideoSetting();
        }

        public VideoTitleData BuildTitleData() => _dataProvider.BuildTitleData();
        public string GetFileName() => _dataProvider.GetFileName();
        public List<QualityOptionData> BuildQualityList() => _dataProvider.BuildQualityList();
        public List<BitRateOptionData> BuildBitRateList() => _dataProvider.BuildBitRateList();

        public string BuildFileSize(VideoOnlyStreamInfo? video, AudioOnlyStreamInfo? audio) => _dataProvider.BuildFileSize(video, audio);

        public string IdentifyTypeFile(VideoOnlyStreamInfo? video, AudioOnlyStreamInfo? audio) {
            return _downloader.IdentifyTypeFile(video, audio);
        }

        public async Task<bool> DownloadVideo(string nameFile, string pathFile, VideoOnlyStreamInfo? video, AudioOnlyStreamInfo? audio, IProgress<double> progress) {

            string path = Path.Combine(pathFile, nameFile);
            return await _downloader.DownloadVideo(path, video, audio, progress);

        }

    }

}
