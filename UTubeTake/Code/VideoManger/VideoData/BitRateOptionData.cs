using YoutubeExplode.Videos.Streams;



namespace UTubeTake.Code.VideoManger.VideoData {
    internal sealed class BitRateOptionData {

        private readonly string _label;
        private readonly AudioOnlyStreamInfo? _stream;


        public BitRateOptionData(string Label, AudioOnlyStreamInfo? stream) {
            _label = Label;
            _stream = stream;
        }

        public override string ToString() {
            return _label;
        }

        public AudioOnlyStreamInfo? GetStream() => _stream;

    }

}
