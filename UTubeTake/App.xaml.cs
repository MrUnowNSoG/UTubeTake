namespace UTubeTake {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new StartPage();
        }

        protected override Window CreateWindow(IActivationState activationState) {
            var window = base.CreateWindow(activationState);
            window.Title = "UTube Take";

            return window;
        }
    }
}
