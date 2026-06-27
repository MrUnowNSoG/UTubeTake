using UTubeTake.Code.StartPage.Error;
using UTubeTake.Code.StartPage.Loading;
using UTubeTake.Code.StartPage.Video.Elements;



namespace UTubeTake.Code.StartPage {
    
    internal sealed class StartPageXamlContainer {
    
        public required Layout WelcomeView { get; init; }
        
        public required LoadingViewElements LoadingView { get; init; }

        public required VideoViewElements VideoView { get; init; }

        public required ErrorViewElements ErrorView { get; init; }

    }

}
