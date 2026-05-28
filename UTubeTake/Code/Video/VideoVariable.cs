using YoutubeExplode.Videos.Streams;
using YoutubeExplode;
using YoutubeExplode.Videos;



namespace UTubeTake.Code {

    internal sealed class VideoVariable {

        public YoutubeClient youtube;
        
        public Video video; //Text
        public StreamManifest currentVideoStreams; //GetsStreams

        public List<VideoOnlyStreamInfo> streamsVideo;
        public List<AudioOnlyStreamInfo> streamsAudio;

        public Dictionary<string, int> videoQuality;
        public Dictionary<string, int> videoBitRate;


        public VideoVariable() {

            youtube = new YoutubeClient();

            streamsVideo = new List<VideoOnlyStreamInfo>();
            streamsAudio = new List<AudioOnlyStreamInfo>();

            videoQuality = new Dictionary<string, int>();
            videoBitRate = new Dictionary<string, int>();

        }

    }
}
