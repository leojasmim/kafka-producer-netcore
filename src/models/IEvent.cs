
namespace KafkaProducerNetCore.Src.Models{
    public interface IEvent {
        Tuple<string, string> GetEvent();
    }
}