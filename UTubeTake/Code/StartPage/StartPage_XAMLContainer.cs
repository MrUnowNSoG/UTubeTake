using Microsoft.Maui.Controls.Shapes;

namespace UTubeTake.Code.StartPage {
    
    internal sealed class StartPage_XAMLContainer {
    
        public required Layout WelcomeView { get; init; }
        
        public required Layout LoadongView { get; init; }
        public required List<Ellipse> LoadingDots { get; init; }
        
        public required Layout VideoView { get; init; }
        public required Picker QualityPicker { get; init; }
        public required Picker BitRatePicker { get; init; }
        public required Label NameVideoLabel { get; init; }
        public required Label AuthorVideoLabel { get; init; }
        public required Label DurationVideoLabel { get; init; }
        public required Label SizeVideoLabel { get; init; }


        public required Layout ErrorView;
        public required Label ErrorCodeLabel { get; init; }
        public required Label ErrorResolveLabel { get; init; }

    }

}
