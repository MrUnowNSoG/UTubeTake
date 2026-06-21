using System;
using System.Collections.Generic;
using System.Text;
using YoutubeExplode;

namespace UTubeTake.Code.VideoManger {
    internal sealed class VideoManager {

        private readonly YoutubeClient _client;
        private readonly VideoDataProvider _dataProvider;
        private readonly VideoFileDownloader _downloader;

        public VideoManager() {
            _client = new YoutubeClient();
            _dataProvider = new VideoDataProvider(_client);
            _downloader = new VideoFileDownloader();
        }

        public async Task DownloadVideoData(string url) {
            _dataProvider.SetVideo(url);
            await _dataProvider.LoadVideoContext();
            await _dataProvider.LoadVideoQuality();
        }

        public VideoTitleData BuildTitleData() => _dataProvider.BuildTitleData();

    }

}
