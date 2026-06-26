using System.Net;
using UTubeTake.Resources.Strings;
using YoutubeExplode.Exceptions;



namespace UTubeTake.Code.Tools.ErrorHandler {

    internal sealed class ErrorHandlerService {

        private static readonly ErrorHandlerService _instance;
        public static ErrorHandlerService GetInstance() => _instance;


        static ErrorHandlerService() {
            _instance = new ErrorHandlerService();
        }

        private ErrorHandlerService() { }


        public event Action<ErrorLog> OnCatchError;


        public void CatchError(Exception ex) {
            ErrorLog log = IdentifyTypeError(ex);
            OnCatchError?.Invoke(log);
        }

        private ErrorLog IdentifyTypeError(Exception ex) => ex switch {

            InvalidLinkException => new ErrorLog(AppResources.Error_BadLink_Title, AppResources.Error_BadLink_Hint),
            InvalidDownloadSettingsException => new ErrorLog(AppResources.Error_DownloadSettings_Title, AppResources.Error_DownloadSettings_Hint),
            FfmpegNotFoundException => new ErrorLog(AppResources.Error_Ffmpeg_Title, AppResources.Error_Ffmpeg_Hint),

            HttpRequestException or WebException => new ErrorLog(AppResources.Error_NoConnection_Title, AppResources.Error_NoConnection_Hint),

            FileNotFoundException => new ErrorLog(string.Format(AppResources.Error_FileNotFound_Title, (ex as FileNotFoundException)?.FileName), AppResources.Error_FileNotFound_Hint),
            UnauthorizedAccessException => new ErrorLog(AppResources.Error_NoAccess_Title, AppResources.Error_NoAccess_Hint),

            VideoUnavailableException => new ErrorLog(AppResources.Error_VideoUnavailable_Title, AppResources.Error_VideoUnavailable_Hint),
            VideoRequiresPurchaseException => new ErrorLog(AppResources.Error_PaidVideo_Title, AppResources.Error_PaidVideo_Hint),

            _ => new ErrorLog(ex.Message, AppResources.Error_Unknown_Hint)
        };

    }
}
