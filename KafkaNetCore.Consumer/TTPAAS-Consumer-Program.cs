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
            var brokerList = Environment.GetEnvironmentVariable("KAFKA_URL");
            
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
