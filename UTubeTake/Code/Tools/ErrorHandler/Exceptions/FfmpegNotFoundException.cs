namespace UTubeTake.Code.Tools.ErrorHandler {

    internal sealed class FfmpegNotFoundException : UTubeTakeException {

        public FfmpegNotFoundException() : base("ffmpeg.exe was not found in the application directory.") { }

    }

}
