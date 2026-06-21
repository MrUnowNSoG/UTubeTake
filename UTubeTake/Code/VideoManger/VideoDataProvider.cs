using System;
using System.Collections.Generic;
using System.Text;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace UTubeTake.Code.VideoManger {

    internal sealed class VideoDataProvider {

        private readonly YoutubeClient _client;

        private string _currentUrl;

        private Video? _currentVideo;
        private List<VideoOnlyStreamInfo>? _streamsVideo;
        private List<AudioOnlyStreamInfo>? _streamsAudio;

        public VideoDataProvider(YoutubeClient client) {
            _client = client;
        }

        public void SetVideo(string url) {
            _currentUrl = url;

            _currentVideo = null;
            _streamsVideo = null;
            _streamsAudio = null;
        }

        public async Task LoadVideoContext() {
            _currentVideo = await _client.Videos.GetAsync(_currentUrl);
        }

        public async Task LoadVideoQuality() {

            StreamManifest steamsManifest = await _client.Videos.Streams.GetManifestAsync(_currentUrl);

            _streamsVideo = steamsManifest.GetVideoOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .OrderByDescending(item => item.VideoQuality).ToList();

            _streamsAudio = steamsManifest.GetAudioOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .OrderByDescending(item => item.Bitrate).ToList();
        }

        public VideoTitleData BuildTitleData() {

            if (_currentVideo == null) return new VideoTitleData("None", "None", "00:00");

            TimeSpan span = _currentVideo.Duration ?? TimeSpan.Zero;
            string timeVideo = $"{(int)span.TotalMinutes}:{span.Seconds:D2}";

            return new VideoTitleData(_currentVideo.Title, _currentVideo.Author.ToString(), timeVideo);
        }


    }
}
