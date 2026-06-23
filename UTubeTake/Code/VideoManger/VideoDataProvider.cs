using UTubeTake.Code.VideoManger.VideoData;
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

        public async Task LoadVideoSetting() {

            StreamManifest steamsManifest = await _client.Videos.Streams.GetManifestAsync(_currentUrl);

            _streamsVideo = steamsManifest.GetVideoOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GroupBy(s => s.VideoQuality.MaxHeight)
                .Select(group =>
                    group.FirstOrDefault(s => s.VideoCodec.StartsWith("avc1"))
                    ?? group.FirstOrDefault(s => s.VideoCodec.StartsWith("av01"))
                    ?? group.First())
                .OrderByDescending(s => s.VideoQuality.MaxHeight)
                .ToList();

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

        public string GetFileName() {
            return _currentVideo?.Title ?? "None";
        }

        public List<string> BuildQualityList() {
            
            List<string> back = new List<string>();
            back.Add("No video");
            
            if(_streamsVideo != null) {

                for (int i = 0, k = 0; i < _streamsVideo.Count; i++, k++) 
                        back.Add(_streamsVideo[i].VideoQuality.Label);

            }

            return back;
        }

        public List<string> BuildBitRateList() {

            List<string> back = new List<string>();
            back.Add("No sound");

            if (_streamsAudio != null) {

                for (int i = 0, k = 0; i < _streamsAudio.Count; i++, k++)
                    back.Add(_streamsAudio[i].Bitrate.ToString());

            }

            return back;
        }

        public string BuildFileSize(int videoId, int bitRateId) {

            videoId -= 1;
            bitRateId -= 1;

            if (videoId < 0 && bitRateId < 0) return "0 Bytes";

            double size = 0;

            if (videoId >= 0 && _streamsVideo != null) {
                size = _streamsVideo[videoId].Size.Bytes;;
            }

            if (bitRateId >= 0 && _streamsAudio != null) {
                size += _streamsAudio[bitRateId].Size.Bytes;
            }

            string typeMemory = "Bytes";
            string[] arrayType = { "Kb", "Mb", "Gb", "B" };

            for (int i = 0; i < arrayType.Length; i++) {
                if (size / 1024.0 >= 1) {
                    size = size / 1024.0;
                    typeMemory = arrayType[i];
                }
            }

            return $"{size:0.##} {typeMemory}";
        }

        public IStreamInfo? GetVideoStreamInfo(int id) {
            
            if(_streamsVideo != null) {
                id -= 1;
                if (id < 0 || id >= _streamsVideo.Count) return null;
                return _streamsVideo[id];
            }

            return null;
        }

        public IStreamInfo? GetAudioStreamInfo(int id) {

            if (_streamsAudio != null) {
                id -= 1;
                if (id < 0 || id >= _streamsAudio.Count) return null;
                return _streamsAudio[id];
            }

            return null;
        }

    }
}
