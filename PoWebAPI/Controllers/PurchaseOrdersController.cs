using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoWebAPI.Data;
using PoWebAPI.Models;

namespace PoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly PoContext _context;

        public PurchaseOrdersController(PoContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrders()
        {
            return await _context.PurchaseOrders.Include(x => x.Employee)
                .ToListAsync();
        }

        // GET: api/PurchaseOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.Include(x => x.Employee)
                                    .SingleOrDefaultAsync(x => x.Id == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return purchaseOrder;
        }



        [HttpGet("needsreview")]
       public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPoThatNeedReview()
        {
            return await _context.PurchaseOrders.Where(e => e.Status == PurchaseOrder.StatusReview)
                                                .Include(e => e.Employee)
                                                .ToListAsync();
        }



        [HttpPut("{id}/approved")]
        public async Task<IActionResult> PutPoToApproved(int id)
        {
            var po = await _context.PurchaseOrders.FindAsync();

            if (po == null)
            {
                return NotFound();
            }
         
            po.Status = "approved";

            return await PutPurchaseOrder(id, po);
        }


        [HttpPut("{id}/rejected")]
        public async Task<IActionResult> PutPotoRejected(int id)
        {
            var po = await _context.PurchaseOrders.FindAsync();

            if (po == null)
            {
                return NotFound();
            }

            po.Status = "rejected";

            return await PutPurchaseOrder(id, po);
        }



        [HttpPut("{id}/review")]
        public async Task<IActionResult> PutPoToReviewOrApproved(int id)
        {
            var po = await _context.PurchaseOrders.FindAsync(id);

            if (po == null)
            {
                return NotFound();
            }      
            if(po.Total == 0)
            {
                return BadRequest();
            }
            po.Status = (po.Total <= 100 && po.Total > 0) ? "Approved" : "Review";
            
            return await PutPurchaseOrder(id, po);

        }


        [HttpPut("{id}/edit")]
        public async Task<IActionResult> PutPoToEdit(int id)
        {
            var po = await _context.PurchaseOrders.FindAsync(id);
            if(po == null)
            {
                return NotFound();
            }
            po.Status = "Edit";

            return await PutPurchaseOrder(id, po);
        }
      

        // PUT: api/PurchaseOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
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

        // POST: api/PurchaseOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseOrder", new { id = purchaseOrder.Id }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseOrder>> DeletePurchaseOrder(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();

            return purchaseOrder;
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrders.Any(e => e.Id == id);
        }
    }
}
