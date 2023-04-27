namespace KafkaProducerNetCore.Src.Application{

    public class MessageRequest{
        public MessageRequest(string payload)
        {
            Payload = payload;
        }

        public string Payload { get; set; }
    }
}