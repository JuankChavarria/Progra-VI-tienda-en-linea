using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Progra_VI_Tienda.Data;
using Progra_VI_Tienda.Models;

namespace Progra_VI_Tienda.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var cliente = await _context.Cliente.ToListAsync();
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
                return NotFound();
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id_cliente)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id_cliente))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);

        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var cliente = await _context.Cliente
            .FirstOrDefaultAsync(e => e.Id_cliente == id);
            if (cliente == null)
                return NotFound();
            return View(cliente);
        }
  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id_cliente == id);
        }

    }
}