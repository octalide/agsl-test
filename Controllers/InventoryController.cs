using AGLS.Models;
using agsl_test.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace agsl_test.Controllers
{
    // InventoryController is a RESTful controller that handles requests for the inventory resource. CRUD operations are supported.
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryContext _context;

        public InventoryController(InventoryContext context)
        {
            _context = context;
        }

        // GET: /inventory
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.InventoryItems);
        }

        // GET: /inventory/{id}
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var item = _context.InventoryItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST: /inventory
        [HttpPost]
        public IActionResult Post([FromBody] InventoryItem inventoryItem)
        {
            if (inventoryItem == null)
            {
                return BadRequest();
            }

            // validate the provided data
            if (!inventoryItem.Validate())
            {
                return BadRequest();
            }

            _context.InventoryItems.Add(inventoryItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = inventoryItem.Id }, inventoryItem);
        }

        // PUT: /inventory/{id}
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] InventoryItem inventoryItem)
        {
            if (inventoryItem == null || inventoryItem.Id != id)
            {
                return BadRequest();
            }

            // validate the provided data
            if (!inventoryItem.Validate())
            {
                return BadRequest();
            }

            var item = _context.InventoryItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Barcode = inventoryItem.Barcode;
            item.Name = inventoryItem.Name;
            item.Price = inventoryItem.Price;

            _context.InventoryItems.Update(item);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: /inventory/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var item = _context.InventoryItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.InventoryItems.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}