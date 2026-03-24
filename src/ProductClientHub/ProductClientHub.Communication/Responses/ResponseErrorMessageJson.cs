namespace ProductClientHub.Communication.Responses
{
    public class ResponseErrorMessageJson
    {
        public List<string> Errors { get; private set; }

        /// pode receber apenas um erro
        public ResponseErrorMessageJson(string message)
        {
            Errors = new List<string> { message };
        }

        // ou varios
        public ResponseErrorMessageJson(List<string> messages)
        {
            Errors = messages;
        }
    }
}
