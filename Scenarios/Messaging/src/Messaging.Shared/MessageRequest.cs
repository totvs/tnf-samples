namespace Messaging
{
    public class MessageRequest
    {
        public MessageRequest()
        {
        }

        public MessageRequest(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
