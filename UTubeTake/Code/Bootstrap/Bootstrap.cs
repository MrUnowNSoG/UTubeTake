namespace UTubeTake.Code.Bootstrap {

    internal sealed class Bootstrap {
    
        public Bootstrap() { }

        public BootstrapContainer Initialize() {

            BootstrapContainer container = new BootstrapContainer();

            container.LinkTest = new LinkTest();
            container.AvatarVideoService = new ImgVideoDownload();

            container.VideoVariable = VideoVariable.Instanite();

            container.VideoInformer = new VideoInformer(container.VideoVariable);
            container.VideoDownloader = new VideoDownloader(container.VideoVariable);

            return container;
        }

    }

}
