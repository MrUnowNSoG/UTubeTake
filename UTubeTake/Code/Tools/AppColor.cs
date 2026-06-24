namespace UTubeTake.Code.Tools {

    internal static class AppColor {

        public static readonly Color ErrorColor = new Color(1,0,0);


        private static Color? _mainAccent;
        public static Color MainAccent => _mainAccent ??= ResolveColor("MainAccent");

        private static Color? _subMainAccent;
        public static Color SubMainAccent => _subMainAccent ??= ResolveColor("SubMainAccent");


        private static Color? _secondAccent;
        public static Color SecondAccent => _secondAccent ??= ResolveColor("SecondAccent");

        private static Color? _subSecondAccent;
        public static Color SubSecondAccent => _subSecondAccent ??= ResolveColor("SubSecondAccent");


        private static Color? _border;
        public static Color Border => _border ??= ResolveColor("Border");

        private static Color? _textPrimary;
        public static Color TextPrimary => _textPrimary ??= ResolveColor("TextPrimary");


        private static Color? _accentBackground;
        public static Color AccentBackground => _accentBackground ??= ResolveColor("AccentBackground");


        private static Color ResolveColor(string name) {
            if (Application.Current.Resources.TryGetValue(name, out object? value) && value is Color color) {
                return color;

            } else {
                return ErrorColor;
            }
        }

    }

}
