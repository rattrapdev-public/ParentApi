using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ToyStoreApi.Models;
using ToyStoreApi.Services;

namespace ToyStoreApi.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchaseController : ControllerBase
{
    private readonly PurchaseDbContext _purchaseDbContext;
    private readonly IToyCatalogCache _cache;

    public PurchaseController(PurchaseDbContext purchaseDbContext, IToyCatalogCache cache)
    {
        _purchaseDbContext = purchaseDbContext;
        _cache = cache;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Purchase(PurchaseModel purchase)
    {
        var toyToCheck = await _cache.GetByUpc(purchase.UPC);

        if (purchase.Cost != toyToCheck.Cost)
        {
            return BadRequest("Cost is invalid");
        }

        _purchaseDbContext.Add(purchase);

        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare("ToyPurchased", exclusive: false);
            var model = new PurchasedToyMessage
            {
                UPC = purchase.UPC,
                ToyName = toyToCheck.Name,
                PurchaserEmail = purchase.PurchaserEmail,
                ChildId = purchase.ChildId,
            };
            var json = JsonConvert.SerializeObject(model);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "ToyPurchased", body: body);
        }

        connection.Close();
        
        return Ok("Your purchase has been processed.");
    }
}