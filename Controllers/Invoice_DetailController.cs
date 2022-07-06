using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using invoicePage.Data;
using invoicePage.Models;

namespace invoicePage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Invoice_DetailController : ControllerBase
    {
        private readonly SalesDbContext _context;

        public Invoice_DetailController(SalesDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoice_Detail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice_Detail>>> GetInvoice_Details()
        {
          if (_context.Invoice_Details == null)
          {
              return NotFound();
          }
            return await _context.Invoice_Details.ToListAsync();
        }

        // GET: api/Invoice_Detail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice_Detail>> GetInvoice_Detail(int id)
        {
          if (_context.Invoice_Details == null)
          {
              return NotFound();
          }
            var invoice_Detail = await _context.Invoice_Details.FindAsync(id);

            if (invoice_Detail == null)
            {
                return NotFound();
            }

            return invoice_Detail;
        }

        // PUT: api/Invoice_Detail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice_Detail(int id, Invoice_Detail invoice_Detail)
        {
            if (id != invoice_Detail.inv_ID)
            {
                return BadRequest();
            }

            _context.Entry(invoice_Detail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Invoice_DetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Invoice_Detail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice_Detail>> PostInvoice_Detail(Invoice_Detail invoice_Detail)
        {
          if (_context.Invoice_Details == null)
          {
              return Problem("Entity set 'SalesDbContext.Invoice_Details'  is null.");
          }
            _context.Invoice_Details.Add(invoice_Detail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Invoice_DetailExists(invoice_Detail.inv_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvoice_Detail", new { id = invoice_Detail.inv_ID }, invoice_Detail);
        }

        // DELETE: api/Invoice_Detail/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice_Detail(int id)
        {
            if (_context.Invoice_Details == null)
            {
                return NotFound();
            }
            var invoice_Detail = await _context.Invoice_Details.FindAsync(id);
            if (invoice_Detail == null)
            {
                return NotFound();
            }

            _context.Invoice_Details.Remove(invoice_Detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Invoice_DetailExists(int id)
        {
            return (_context.Invoice_Details?.Any(e => e.inv_ID == id)).GetValueOrDefault();
        }

    }
}
