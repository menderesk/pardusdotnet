using Confluent.Kafka;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
 
 
namespace TT.PAAS.Producer.Program
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string topicName = Environment.GetEnvironmentVariable ("TOPIC_NAME");;
            string kafkaURL = Environment.GetEnvironmentVariable ("KAFKA_URL");
            string saslUserName = Environment.GetEnvironmentVariable ("SASL_USERNAME");
            string saslPassword = Environment.GetEnvironmentVariable ("SASL_PASSWORD");

            var config = new ProducerConfig {
                BootstrapServers = kafkaURL,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                //SslCaLocation = "ca-cert",
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = saslUserName,
                SaslPassword = saslPassword,
                Acks = Acks.Leader,
                CompressionType = CompressionType.Lz4,
            };
 
            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                for (int i = 0; i < 100; i++)
                {
                    var message = $"TT PaaS {i}";
 
                    try
                    {
 
                        var deliveryReport = await producer.ProduceAsync(topicName, new Message<string, string> { Key = null, Value = message } );
                    }
                    catch (ProduceException<string, string> e)
                    {
                        Console.WriteLine($"Mesajta hata: {e.Message} [{e.Error.Code}]");
                    }
                }
 
            }
        }
    }
}