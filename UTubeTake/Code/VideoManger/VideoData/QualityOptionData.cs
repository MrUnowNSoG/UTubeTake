using YoutubeExplode.Videos.Streams;

namespace UTubeTake.Code.VideoManger.VideoData {
    internal sealed class QualityOptionData {

        private readonly string _label;
        private readonly VideoOnlyStreamInfo? _stream;


        public QualityOptionData(string Label, VideoOnlyStreamInfo? stream) {
            _label = Label;
            _stream = stream;
        }

        public override string ToString() {
            return _label;
        }

        public VideoOnlyStreamInfo? GetStream() => _stream;

    }

}
