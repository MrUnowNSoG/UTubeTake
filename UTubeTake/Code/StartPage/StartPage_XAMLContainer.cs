using UTubeTake.Code.StartPage.Error;
using UTubeTake.Code.StartPage.Loading;
using UTubeTake.Code.StartPage.Video.Elements;



namespace UTubeTake.Code.StartPage {
    
    internal sealed class StartPage_XAMLContainer {
    
        public required Layout WelcomeView { get; init; }
        
        public required LoadingViewElements LoadingView { get; init; }

        public required VideoViewElements VideoView { get; init; }

        public required ErrorViewEelemetns ErrorView { get; init; }

    }

}
