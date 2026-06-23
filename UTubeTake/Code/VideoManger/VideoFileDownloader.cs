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

        public async Task DownloadVideo(string pathFile, IStreamInfo? video, IStreamInfo? audio, IProgress<double> progress) {

            if (video is null && audio is null) {
                ErrorHandlerService.GetInstance().CathcError(new Exception("Incorrect video download settings selected!"));
            }


            if (video is not null && audio is not null) {
                string ffmpegPath = Path.Combine(AppContext.BaseDirectory, "ffmpeg-windows-x64", "ffmpeg.exe");

                if (File.Exists(ffmpegPath) == false) {
                    ErrorHandlerService.GetInstance().CathcError(new FileNotFoundException("Program can't find ffmpeg.exe!"));
                    return;
                }

                ConversionRequestBuilder convers = new ConversionRequestBuilder(pathFile + ".mp4");
                convers.SetFFmpegPath(ffmpegPath);

                var streamInfos = new IStreamInfo[] { video, audio };
                await _client.Videos.DownloadAsync(streamInfos, convers.Build(), progress);

            } else {

                if (video is not null)
                    await _client.Videos.Streams.DownloadAsync(video, pathFile + ".mp4", progress);

                if (audio is not null)
                    await _client.Videos.Streams.DownloadAsync(audio, pathFile + ".mp3", progress);
            }

        }

    }
}
