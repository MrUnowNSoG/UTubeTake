using UTubeTake.Code.StartPage;



namespace UTubeTake.Code.Bootstrap {

    internal sealed class Bootstrap {

        private readonly StartPage_XAMLContainer _xamlContainer;

        public Bootstrap(StartPage_XAMLContainer container) {
            _xamlContainer = container;
        }

        public BootstrapContainer Initialize() {

            VideoVariable variable = new VideoVariable();

            BootstrapContainer container = new BootstrapContainer {

                LinkTest = new LinkTest(),
                AvatarVideoService = new ImgVideoDownload(),

                VideoInformer = new VideoInformer(variable),
                VideoDownloader = new VideoDownloader(variable),
                VideoUiUpdater = new VideoUiUpdater(_xamlContainer)
            };

            return container;

        }

    }

}
