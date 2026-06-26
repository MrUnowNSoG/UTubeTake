using UTubeTake.Code.Tools;
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

        public string IdentifyTypeFile(IStreamInfo? video, IStreamInfo? audio) {

            if (video is null && audio is null) return "";
            if (video is null && audio is not null) return FileExtensions.Mp3;
            return FileExtensions.Mp4;

        }

        public async Task<bool> DownloadVideo(string pathFile, IStreamInfo? video, IStreamInfo? audio, IProgress<double> progress) {

            if (video is null && audio is null) {
                ErrorHandlerService.GetInstance().CatchError(new Exception("Incorrect video download settings selected!"));
                return false;
            }


            if (video is not null && audio is not null) {
                string ffmpegPath = FfmpegConfig.ExecutablePath;

                if (File.Exists(ffmpegPath) == false) {
                    ErrorHandlerService.GetInstance().CatchError(new FileNotFoundException("Program can't find ffmpeg.exe!"));
                    return false;
                }

                ConversionRequestBuilder convers = new ConversionRequestBuilder(pathFile + FileExtensions.Mp4);
                convers.SetFFmpegPath(ffmpegPath);

                var streamInfos = new IStreamInfo[] { video, audio };
                await _client.Videos.DownloadAsync(streamInfos, convers.Build(), progress);
                return true;

            } else {

                if (video is not null) {
                    await _client.Videos.Streams.DownloadAsync(video, pathFile + FileExtensions.Mp4, progress);
                    return true;
                }

                if (audio is not null) {
                    await _client.Videos.Streams.DownloadAsync(audio, pathFile + FileExtensions.Mp3, progress);
                    return true;
                }
            }

            return false;
            
        }

    }
}
