using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace satchel.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class ShoppingListController : Controller
	{
		private ShoppingListContext _context;

		public ShoppingListController(ShoppingListContext context)
		{
			_context = context;

			if(_context.ShoppingListItems.Count() == 0)
			{
				_context.ShoppingListItems.Add(new ShoppingListItem{Name="Bananas"});
				_context.SaveChanges();
			}
		}

		[HttpGet]
		public IEnumerable<ShoppingListItem> GetAll()
		{
			return _context.ShoppingListItems.AsNoTracking().ToList();
		}


		[HttpGet("{id}",Name="GetItem")]
		public IActionResult GetById(long id)
		{
			var item = _context.ShoppingListItems.FirstOrDefault(t => t.Id == id);

			if(item == null)
			{
				return NotFound();
			}

			return new ObjectResult(item);
		}

		[HttpPost]
		public IActionResult Create([FromBody] ShoppingListItem item)
		{
			if(item == null)
			{
				return BadRequest();
			}

			_context.ShoppingListItems.Add(item);
			_context.SaveChanges();

			return CreatedAtRoute("GetItem",new {id = item.Id},item);
		}

		[HttpPut("{id}")]
		public IActionResult Update(long id, [FromBody] ShoppingListItem item)
		{
			if(item == null || item.Id != id)
			{
				return BadRequest();
			}

			var slitem = _context.ShoppingListItems.FirstOrDefault(t => t.Id == id);
			if(slitem == null)
			{
				return NotFound();
			}

			slitem.Name = item.Name;
			slitem.Purchased = item.Purchased;

			_context.ShoppingListItems.Update(slitem);
			_context.SaveChanges();

			return new NoContentResult();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(long id)
		{
			var slitem = _context.ShoppingListItems.FirstOrDefault(t => t.Id == id);
			if(slitem == null)
			{
				return NotFound();
			}

			_context.ShoppingListItems.Remove(slitem);
			_context.SaveChanges();

			return new NoContentResult();
		}
	}
}