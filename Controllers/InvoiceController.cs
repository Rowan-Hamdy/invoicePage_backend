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
    public class InvoiceController : ControllerBase
    {
        private readonly SalesDbContext _context;

        public InvoiceController(SalesDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
          if (_context.Invoices == null)
          {
              return NotFound();
          }
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = (from i in _context.Invoices
                           where i.inv_ID == id
                           select new
                           {
                               i.inv_ID,
                               i.cust_name,
                               i.inv_date,
                               i.items_count,
                               i.total_price
                           }).FirstOrDefault();
            var invoiceDetails = (from inv_det in _context.Invoice_Details 
                                  where inv_det.inv_ID==id
                                  select new
                                  {
                                      inv_det.inv_ID,
                                      inv_det.item,
                                      inv_det.price,
                                      inv_det.quantity,
                                      inv_det.total_per_Quantity
                                  }).ToList();
            return Ok(new{ invoice, invoiceDetails});
        }

        // PUT: api/Invoice/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice)
        {
            if (id != invoice.inv_ID)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {
            try
            {
                //Invoice Table
                if (invoice.inv_ID == 0)
                    _context.Invoices.Add(invoice);

                else
                    _context.Entry(invoice).State = EntityState.Modified;


                //Invoice_Details Table
                foreach (var item in invoice.Invoice_Details)
                {
                    if (item.inv_ID == 0)
                        _context.Invoice_Details.Add(item);
                    else
                        _context.Entry(item).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return (_context.Invoices?.Any(e => e.inv_ID == id)).GetValueOrDefault();
        }
        //calling the total_price procedure
        [HttpGet("total_priceAsync")]
        public async Task <int>Total_PricesAsync(int? inv_ID)
        {
            return await _context.GetProcedures().total_priceAsync(inv_ID);

        }

        //calling the items_count procedure
        [HttpGet("items_countAsync")]
        public async Task<int> items_countAsync(int? inv_ID)
        {
            return await _context.GetProcedures().items_CountAsync(inv_ID);

        }

    }
}
