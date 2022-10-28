// See https://aka.ms/new-console-template for more information

using System.Text;
using Newtonsoft.Json;
using ParentApiSubscriber;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Starting ParentApi subscriber");

var factory = new ConnectionFactory {HostName = "localhost"};

var connection = factory.CreateConnection();
using
var channel = connection.CreateModel();
channel.QueueDeclare("ToyPurchased", exclusive: false);
channel.QueueDeclare("ToyNameChanged", exclusive: false);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, eventArgs) =>
{
   var body = eventArgs.Body.ToArray();
   var message = Encoding.UTF8.GetString(body);
   var messageModel = JsonConvert.DeserializeObject<PurchasedToyMessage>(message);
   var httpClient = new HttpClient();
   var response = await httpClient.PostAsync($"https://localhost:7271/api/children/{messageModel.ChildId}/toy",
      new StringContent(message, Encoding.UTF8, "application/json"));
   if (response.IsSuccessStatusCode)
   {
      channel.BasicAck(eventArgs.DeliveryTag, false);
   }
};
var consumer2 = new EventingBasicConsumer(channel);
consumer2.Received += async (model, eventArgs) =>
{
   var body = eventArgs.Body.ToArray();
   var message = Encoding.UTF8.GetString(body);
   var messageModel = JsonConvert.DeserializeObject<ToyNameChangedEvent>(message);
   var httpClient = new HttpClient();
   var response = await httpClient.PatchAsync($"https://localhost:7271/api/toys/{messageModel.UPC}",
      new StringContent(message, Encoding.UTF8, "application/json"));
   if (response.IsSuccessStatusCode)
   {
      channel.BasicAck(eventArgs.DeliveryTag, false);
   }
};
channel.BasicConsume(queue: "ToyPurchased", autoAck: false, consumer: consumer);
channel.BasicConsume(queue: "ToyNameChanged", autoAck: false, consumer: consumer2);



Console.ReadKey();