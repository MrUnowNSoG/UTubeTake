namespace UTubeTake.Code.Tools {

    internal static class FfmpegConfig {

        public const string FolderName = "ffmpeg-windows-x64";
        public const string ExecutableName = "ffmpeg.exe";

        public static string ExecutablePath => Path.Combine(AppContext.BaseDirectory, FolderName, ExecutableName);

    }

}
