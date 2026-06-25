using UTubeTake.Code.VideoManger.VideoData;
using YoutubeExplode;
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
        public List<string> BuildQualityList() => _dataProvider.BuildQualityList();
        public List<string> BuildBitRateList() => _dataProvider.BuildBitRateList();

        public string BuildFileSize(int videoId, int bitRateId) => _dataProvider.BuildFileSize(videoId, bitRateId);

        public string DefinityTypeFile(int qualityId, int bitRateId) {
            IStreamInfo? video = _dataProvider.GetVideoStreamInfo(qualityId);
            IStreamInfo? audio = _dataProvider.GetAudioStreamInfo(bitRateId);
            return _downloader.DefinityTypeFile(video, audio);
        }

        public async Task<bool> DownloadVideo(string nameFile, string pathFile,int qualityId, int bitRateId, IProgress<double> progress) {

            string path = Path.Combine(pathFile, nameFile);
            IStreamInfo? video = _dataProvider.GetVideoStreamInfo(qualityId);
            IStreamInfo? audio = _dataProvider.GetAudioStreamInfo(bitRateId);
            return await _downloader.DownloadVideo(path, video, audio, progress);

        }

    }

}
