namespace UTubeTake.Code.Tools.ErrorHandler {

    internal sealed class InvalidDownloadSettingsException : UTubeTakeException {

        public InvalidDownloadSettingsException() : base("No video or audio stream was selected for download.") { }

    }

}
