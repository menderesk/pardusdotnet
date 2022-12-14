using Confluent.Kafka;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TT.PAAS.Consumer.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumergroup = Environment.GetEnvironmentVariable("CONSUMER_GROUP");
            var topicName = Environment.GetEnvironmentVariable("TOPIC_NAME");
            var kafkaURL = Environment.GetEnvironmentVariable("KAFKA_URL");
            string saslUserName = Environment.GetEnvironmentVariable ("SASL_USERNAME");
            string saslPassword = Environment.GetEnvironmentVariable ("SASL_PASSWORD");
            
            var config = new ConsumerConfig {
                BootstrapServers = kafkaURL,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                //SslCaLocation = "ca-cert",
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = saslUserName,
                SaslPassword = saslPassword,
                ClientId = consumergroup;
            };

            using (var consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                consumer.Subscribe(topicName);
                while (true)
                {
                    var consumeResult = consumer.Consume();
                    Console.WriteLine(consumeResult.Value);
                    //consumer.Commit();
                }
            }
        }
    }
}
