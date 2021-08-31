using RabbitMQ.Client;
using System;
using System.Text;

namespace NewTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Sending");

            var factory = new ConnectionFactory() { HostName = "ubuntuserver", UserName = "guest", Password = "M@TTOSs01" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                for (int i = 0; i < 10; i++)
                {
                    var message = GetMessage(new string[] { "Message", "".PadLeft(i + 1, '.'), (i + 1).ToString() });
                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: "task_queue",
                                         basicProperties: properties,
                                         body: body);
                }

            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
