namespace UTubeTake.Code.Bootstrap {

    internal sealed class BootstrapContainer {

        public required LinkTest LinkTest { get; init; }
        public required ImgVideoDownload AvatarVideoService {  get; init; }

        public required VideoInformer VideoInformer { get; init; }
        public required VideoUiUpdater VideoUiUpdater { get; init; }
        public required VideoDownloader VideoDownloader { get; init; }

    }

}
