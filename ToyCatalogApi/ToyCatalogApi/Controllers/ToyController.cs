using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ToyCatalogApi.Models;

namespace ToyCatalogApi.Controllers;

[Route("api/toys")]
public class ToyController : ControllerBase
{
    private readonly ToyDbContext _toyDbContext;

    public ToyController(ToyDbContext toyDbContext)
    {
        _toyDbContext = toyDbContext;
    }
    
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var toys = await _toyDbContext.Toys.Select(x => x).ToListAsync();

        return Ok(toys);
    }

    [Route("{upc}")]
    [HttpGet]
    public async Task<IActionResult> GetBy(string upc)
    {
        var toy = await _toyDbContext.Toys.FirstOrDefaultAsync(x => x.UPC == upc);

        return Ok(toy);
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ToyModel toy)
    {
        if (_toyDbContext.Toys.Any(x => x.UPC == toy.UPC))
        {
            return Conflict("Toy already exists");
        }

        await _toyDbContext.Toys.AddAsync(toy);
        await _toyDbContext.SaveChangesAsync();

        return Ok();
    }

    [Route("{upc}")]
    [HttpPatch]
    public async Task<IActionResult> PatchName(string upc, [FromBody] ToyModel toy)
    {
        if (toy.UPC != upc)
        {
            return BadRequest($"UPC {upc} does not match given toy");
        }
        
        if (_toyDbContext.Toys.Any(x => x.UPC == toy.UPC))
        {
            var toyToUpdate = await _toyDbContext.Toys.FirstAsync(x => x.UPC == upc);

            toyToUpdate.Name = toy.Name;

            _toyDbContext.ChangeTracker.Clear();
            _toyDbContext.Toys.Update(toyToUpdate);
        }
        else
        {
            return NotFound("Toy was not found");
        }

        await _toyDbContext.SaveChangesAsync();
        
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        var connection = connectionFactory.CreateConnection();
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare("ToyNameChanged", exclusive: false);
            var model = new ToyNameChangedEvent()
            {
                UPC = toy.UPC,
                UpdatedName = toy.Name,
            };
            var json = JsonConvert.SerializeObject(model);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "ToyNameChanged", body: body);
        }

        connection.Close();

        return Ok();
    }

    [Route("{upc}")]
    [HttpPut]
    public async Task<IActionResult> Upsert(string upc, [FromBody] ToyModel toy)
    {
        if (toy.UPC != upc)
        {
            return BadRequest($"UPC {upc} does not match given toy");
        }
        
        if (_toyDbContext.Toys.Any(x => x.UPC == toy.UPC))
        {
            var toyToUpdate = await _toyDbContext.Toys.FirstAsync(x => x.UPC == upc);

            toyToUpdate.Name = toy.Name;
            toyToUpdate.Description = toy.Description;
            toyToUpdate.Cost = toy.Cost;
            
            _toyDbContext.Toys.Update(toy);
        }
        else
        {
            await _toyDbContext.Toys.AddAsync(toy);
        }

        await _toyDbContext.SaveChangesAsync();

        return Ok();
    }

    [Route("{upc}")]
    [HttpDelete]
    public async Task<IActionResult> Delete(string upc)
    {
        var toyToDelete = await _toyDbContext.Toys.FirstAsync(x => x.UPC == upc);

        _toyDbContext.Remove(toyToDelete);
        
        await _toyDbContext.SaveChangesAsync();

        return Ok();
    }
}