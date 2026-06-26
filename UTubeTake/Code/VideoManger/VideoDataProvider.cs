using UTubeTake.Code.VideoManger.VideoData;
using UTubeTake.Resources.Strings;
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

            StreamManifest streamsManifest = await _client.Videos.Streams.GetManifestAsync(_currentUrl);

            _streamsVideo = streamsManifest.GetVideoOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .GroupBy(s => s.VideoQuality.MaxHeight)
                .Select(group =>
                    group.FirstOrDefault(s => s.VideoCodec.StartsWith("avc1"))
                    ?? group.FirstOrDefault(s => s.VideoCodec.StartsWith("av01"))
                    ?? group.First())
                .OrderByDescending(s => s.VideoQuality.MaxHeight)
                .ToList();

            _streamsAudio = streamsManifest.GetAudioOnlyStreams()
                .Where(s => s.Container == Container.Mp4)
                .OrderByDescending(item => item.Bitrate).ToList();
        }

        public VideoTitleData BuildTitleData() {

            if (_currentVideo == null) return new VideoTitleData(AppResources.Common_None, AppResources.Common_None, "00:00");

            TimeSpan span = _currentVideo.Duration ?? TimeSpan.Zero;
            string timeVideo = $"{(int)span.TotalMinutes}:{span.Seconds:D2}";

            return new VideoTitleData(_currentVideo.Title, _currentVideo.Author.ToString(), timeVideo);
        }

        public string GetFileName() {
            return _currentVideo?.Title ?? AppResources.Common_None;
        }

        public List<QualityOptionData> BuildQualityList() {
            
            List<QualityOptionData> back = new List<QualityOptionData>();
            back.Add(new QualityOptionData(AppResources.Picker_NoVideo, null));
            
            if(_streamsVideo != null) {

                for (int i = 0; i < _streamsVideo.Count; i++) {
                    var temp = new QualityOptionData(_streamsVideo[i].VideoQuality.Label, _streamsVideo[i]);
                    back.Add(temp);
                } 

            }

            return back;
        }

        public List<BitRateOptionData> BuildBitRateList() {

            List<BitRateOptionData> back = new List<BitRateOptionData>();
            back.Add(new BitRateOptionData(AppResources.Picker_NoSound, null));

            if (_streamsAudio != null) {

                for (int i = 0; i < _streamsAudio.Count; i++) {
                    var temp = new BitRateOptionData(_streamsAudio[i].Bitrate.ToString(), _streamsAudio[i]);
                    back.Add(temp);
                } 

            }

            return back;
        }

        public string BuildFileSize(VideoOnlyStreamInfo? video, AudioOnlyStreamInfo? audio) {

            if (video is null && audio is null) return "0 Bytes";

            double size = 0;

            if (video is not null) {
                size = video.Size.Bytes;
            }

            if (audio is not null) {
                size += audio.Size.Bytes;
            }

            string typeMemory = "Bytes";
            string[] arrayType = { "Kb", "Mb", "Gb" };

            for (int i = 0; i < arrayType.Length; i++) {
                if (size / 1024.0 >= 1) {
                    size = size / 1024.0;
                    typeMemory = arrayType[i];
                }
            }

            return $"{size:0.##} {typeMemory}";
        }

    }
}
