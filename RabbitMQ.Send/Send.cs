using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Send
{
    class Send
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Sending");

            var factory = new ConnectionFactory() { HostName = "ubuntuserver", UserName = "guest", Password = "M@TTOSs01"  };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
