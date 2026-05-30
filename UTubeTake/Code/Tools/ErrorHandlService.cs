using System.Net;
using UTubeTake.Code.StartPage;
using YoutubeExplode.Exceptions;



namespace UTubeTake.Code.Tools {

    internal sealed class ErrorHandlService {

        public struct ErrorLog {

            public string Message;
            public string Resolve;

            public ErrorLog(string message, string resolve) {
                Message = message;
                Resolve = resolve;
            }

        }

        public readonly Label _errorCodeLabel;
        public readonly Label _errorResolveLabel;

        public ErrorHandlService(StartPage_XAMLContainer container) {
            _errorCodeLabel = container.ErrorCodeLabel;
            _errorResolveLabel = container.ErrorResolveLabel;
        }

        public void CathcError(Exception ex) {
            ErrorLog log = DefinityError(ex);
            _errorCodeLabel.Text = log.Message;
            _errorResolveLabel.Text = log.Resolve;
        }

        private ErrorLog DefinityError(Exception ex) => ex switch {

            HttpRequestException or WebException => new ErrorLog("Немає підключення", "Перевірте своє інтернет підключення та спробуйте ще раз."),
               
            // Файлові помилки
            FileNotFoundException => new ErrorLog($"Файл відсутній: {(ex as FileNotFoundException)?.FileName}", "Перевірте наявність файлу."),
            UnauthorizedAccessException => new ErrorLog("Немає доступу", "Програма не має прав доступу до файлу або папки."),

            // YouTube помилки
            VideoUnavailableException => new ErrorLog("Відео недоступне.", "Це відео недоступне або було видалено."),
            VideoRequiresPurchaseException => new ErrorLog("Платне відео", "Це відео потребує придбання і не може бути завантажене."),

            _ => new ErrorLog(ex.Message, "")
        };

    }
}
