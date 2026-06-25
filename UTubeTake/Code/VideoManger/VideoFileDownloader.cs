using UTubeTake.Code.Tools.ErrorHandler;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;



namespace UTubeTake.Code.VideoManger {

    internal sealed class VideoFileDownloader {

        private readonly YoutubeClient _client;

        public VideoFileDownloader(YoutubeClient client) {
            _client = client;
        }

        public string DefinityTypeFile(IStreamInfo? video, IStreamInfo? audio) {

            if (video is null && audio is null) return "";
            if (video is null && audio is not null) return ".mp3";
            return ".mp4";

        }

        public async Task<bool> DownloadVideo(string pathFile, IStreamInfo? video, IStreamInfo? audio, IProgress<double> progress) {

            if (video is null && audio is null) {
                ErrorHandlerService.GetInstance().CathcError(new Exception("Incorrect video download settings selected!"));
                return false;
            }


            if (video is not null && audio is not null) {
                string ffmpegPath = Path.Combine(AppContext.BaseDirectory, "ffmpeg-windows-x64", "ffmpeg.exe");

                if (File.Exists(ffmpegPath) == false) {
                    ErrorHandlerService.GetInstance().CathcError(new FileNotFoundException("Program can't find ffmpeg.exe!"));
                    return false;
                }

                ConversionRequestBuilder convers = new ConversionRequestBuilder(pathFile + ".mp4");
                convers.SetFFmpegPath(ffmpegPath);

                var streamInfos = new IStreamInfo[] { video, audio };
                await _client.Videos.DownloadAsync(streamInfos, convers.Build(), progress);
                return true;

            } else {

                if (video is not null) {
                    await _client.Videos.Streams.DownloadAsync(video, pathFile + ".mp4", progress);
                    return true;
                }

                if (audio is not null) {
                    await _client.Videos.Streams.DownloadAsync(audio, pathFile + ".mp3", progress);
                    return true;
                }
            }

            return false;
            
        }

    }
}
