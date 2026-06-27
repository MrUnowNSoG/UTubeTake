namespace UTubeTake.Code.Tools.ErrorHandler {

    internal sealed class InvalidLinkException : UTubeTakeException {

        public InvalidLinkException() : base("The provided URL is not a valid YouTube link.") { }

    }

}
