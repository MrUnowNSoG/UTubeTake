namespace UTubeTake.Code.Tools.ErrorHandler {

    internal abstract class UTubeTakeException : Exception {

        protected UTubeTakeException(string message) : base(message) { }

    }

}
