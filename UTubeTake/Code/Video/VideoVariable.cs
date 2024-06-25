using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace UTubeTake.Code {
    internal class VideoVariable {

        //
        public YoutubeClient youtube;

        //
        public Video video; //Text
        public StreamManifest currentVideoStreams; //GetsStreams

        //Streams
        public List<VideoOnlyStreamInfo> streamsVideo;
        public List<AudioOnlyStreamInfo> streamsAudio;

        //Picker
        public Dictionary<string, int> videoQuality;
        public Dictionary<string, int> videoBitRate;


        //
        private static VideoVariable _instanite;


        private VideoVariable() {

            youtube = new YoutubeClient();

            streamsVideo = new List<VideoOnlyStreamInfo>();
            streamsAudio = new List<AudioOnlyStreamInfo>();

            videoQuality = new Dictionary<string, int>();
            videoBitRate = new Dictionary<string, int>();

        }

        public static VideoVariable Instanite() {
            if(_instanite == null) {
                _instanite = new VideoVariable();
            }

            return _instanite;
        }

    }
}
