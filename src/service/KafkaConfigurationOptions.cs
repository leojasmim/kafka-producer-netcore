namespace KafkaProducerNetCore.Src.Service{
    public class KafkaConfigurationOptions
    {
        public const string KafkaConfiguration = "KafkaConfiguration";

        public KafkaConfigurationOptions() { }

        public KafkaConfigurationOptions(string hostname, string topicname)
        {
            HostName = hostname;
            TopicName = topicname;
        }

        public string HostName { get; set; }
        public string TopicName { get; set; }
    }
}