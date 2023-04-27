using System.Text.Json;
using Visus.Cuid;

namespace KafkaProducerNetCore.Src.Models{
    
    public class Message : IEvent {
        
        public Message(string message)
        {
            Id = "msg_" + new Cuid2(); 
            Payload = message;
            CreateDate = DateTime.Now;
            Source = "netcore";
        }

        public string Id { get; set; }
        public string Payload { get; set; }
        
        public DateTime CreateDate { get; set; }

        public string Source { get; set; }

        public Tuple<string, string> GetEvent()
        {
            return new Tuple<string, string>(Id, JsonSerializer.Serialize(this));
        }
    }
}
