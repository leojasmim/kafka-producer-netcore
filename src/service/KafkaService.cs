using Confluent.Kafka;
using KafkaProducerNetCore.Src.Models;

namespace KafkaProducerNetCore.Src.Service{

    public class KafkaService{
        private readonly KafkaConfigurationOptions _config;
        private ProducerConfig _producerConfig;
        private IProducer<string, string> _producer;
        private IAdminClient _adminClient;  

        public KafkaService(IConfiguration config)
        {
            _config = new KafkaConfigurationOptions(
                config.GetSection(KafkaConfigurationOptions.KafkaConfiguration)["HostName"],
                config.GetSection(KafkaConfigurationOptions.KafkaConfiguration)["TopicName"]
            );

            Console.WriteLine($"Config Kafka: Host = {_config.HostName} ; Topic = {_config.TopicName} ");

            _producerConfig = new ProducerConfig
            {
                BootstrapServers = _config.HostName
            };

            _producer = new ProducerBuilder<string, string>(_producerConfig).Build();
        }

        public async Task<bool> SendEventAsync(IEvent @event)
        {
            var eventId = @event.GetEvent().Item1;
            var eventPayload = @event.GetEvent().Item2;

            try
            {
                var result = await _producer.ProduceAsync(
                        _config.TopicName,
                        new Message<string, string>
                        {
                            Key = eventId,
                            Value = eventPayload
                        });

                Console.WriteLine($"Enviada => Mensagem: {eventPayload} | Status: {result.Status.ToString()}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}