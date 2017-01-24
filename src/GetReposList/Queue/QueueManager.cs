using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GetReposList.Queue
{
    public class QueueManager : IQueueManager, IDisposable
    {
        private IModel _publisher;
        private IModel _consumerChannel;
        private EventingBasicConsumer _consumer;
        private IMessageHandler _handler;

        public QueueManager(IMessageHandler handler)
        {
            _handler = handler;
            //CreateProducer();
            //CreateConsumer();
        }

        private void CreateProducer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                _publisher = connection.CreateModel();

                _publisher.QueueDeclare(queue: "commandQ",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
            }
        }

        public void PublishMessage(string message)
        {
            //var body = Encoding.UTF8.GetBytes(message);

            //_publisher.BasicPublish(exchange: "",
            //                     routingKey: "commandQ",
            //                     basicProperties: null,
            //                     body: body);
            //Console.WriteLine(" [x] Sent {0}", message);
            _handler.Handle(message);
        }

        private void CreateConsumer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                _consumerChannel = connection.CreateModel();

                    _consumerChannel.QueueDeclare(queue: "commandQ",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    _consumer = new EventingBasicConsumer(_consumerChannel);
                    _consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        _handler.Handle(message);
                    };

                    _consumerChannel.BasicConsume(queue: "commandQ",
                                         noAck: true,
                                         consumer: _consumer);
            }
        }

        public void Dispose()
        {
            _publisher.Dispose();
            _publisher = null;
            _consumerChannel.Dispose();
            _consumerChannel = null;
            _consumer = null;
        }
    }
}
