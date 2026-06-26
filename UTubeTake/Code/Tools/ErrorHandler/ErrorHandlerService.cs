using System.Net;
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

            HttpRequestException or WebException => new ErrorLog("Немає підключення", "Перевірте своє інтернет підключення та спробуйте ще раз."),
               
            FileNotFoundException => new ErrorLog($"Файл відсутній: {(ex as FileNotFoundException)?.FileName}", "Перевірте наявність файлу."),
            UnauthorizedAccessException => new ErrorLog("Немає доступу", "Програма не має прав доступу до файлу або папки."),

            VideoUnavailableException => new ErrorLog("Відео недоступне.", "Це відео недоступне або було видалено."),
            VideoRequiresPurchaseException => new ErrorLog("Платне відео", "Це відео потребує придбання і не може бути завантажене."),

            _ => new ErrorLog(ex.Message, "")
        };

    }
}
