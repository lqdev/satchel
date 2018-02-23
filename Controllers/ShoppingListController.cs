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
	}
}