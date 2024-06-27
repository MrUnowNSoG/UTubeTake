using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace UTubeTake.Code {
    internal class VideoDownloader {

        private VideoVariable _variable;

        public VideoDownloader(VideoVariable variable) { 
            _variable = variable;
        }

        public async Task DownloadVideo(int quality, int bitrate) {

            //
            if (quality <= 0 && bitrate <= 0) return;


            //Need creat file
            IStreamInfo? videoStream = null;
            if (quality > 0) {
                videoStream = _variable.streamsVideo[_variable.videoQuality.ElementAt(quality).Value];
            }

            IStreamInfo? audioStream = null;
            if (bitrate > 0) {
                audioStream = _variable.streamsAudio[_variable.videoBitRate.ElementAt(bitrate).Value];
            }

            string nameFile = SettingStatic.pathForVideo + @"\" + RemoveInvalidPathChars(_variable.video.Title);

            //Select Type File
            if (videoStream != null && audioStream != null) {

                ConversionRequestBuilder convers = new ConversionRequestBuilder(nameFile + ".mp4");
                convers.SetFFmpegPath(@"D:\Download\ffmpeg-windows-x64\ffmpeg.exe");
                var streamInfos = new IStreamInfo[] { audioStream, videoStream };

                await _variable.youtube.Videos.DownloadAsync(streamInfos, convers.Build());

            } else {

                if (videoStream != null) {
                    await _variable.youtube.Videos.Streams.DownloadAsync(videoStream, nameFile + ".mp4");
                }

                if (audioStream != null) {
                    await _variable.youtube.Videos.Streams.DownloadAsync(audioStream, nameFile + ".mp3");
                }
            }
        }


        private string RemoveInvalidPathChars(string name) {

            char[] invalidChars = Path.GetInvalidPathChars().Concat(new char[] { '\\', '/', '?' }).ToArray();

            foreach (char c in invalidChars) {
                name = name.Replace(c.ToString(), " ");
            }

            return name;
        }

    }
}
