using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSolution.Core.DTO;
using PaymentSolution.Core.Service;
using PaymentSolution.Data;
using PaymentSolution.Model.Entity;

namespace PaymentSolution.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService  _pService;

        public PaymentController(PaymentService pService)
        {
            _pService = pService;
        }

        // GET: api/Payment
        [Route("{controller}/{action}")]
        [ProducesResponseType(statusCode:200, Type =typeof(IEnumerable<PaymentDetailsDTO>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetailsDTO>>> GetPaymentDes()
        {
            return  Ok( await _pService.GetPayments());
        }

        // GET: api/Payment/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        //{
        //    var paymentDetail = await _context.PaymentDetails.FindAsync(id);

        //    if (paymentDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return paymentDetail;
        //}

        // PUT: api/Payment/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        //{
        //    if (id != paymentDetail.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(paymentDetail).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PaymentDetailExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Payment
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetailsDTO paymentDetail)
        {
          await  _pService.MakePayment(paymentDetail);


            return Ok(new PaymentResult { IsComplete = true });
        //    return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.Id }, paymentDetail);
        }

        // DELETE: api/Payment/5
      //  [HttpDelete("{id}")]
        //public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        //{
        //    var paymentDetail = await _context.PaymentDetails.FindAsync(id);
        //    if (paymentDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PaymentDetails.Remove(paymentDetail);
        //    await _context.SaveChangesAsync();

        //    return paymentDetail;
        //}

        //private bool PaymentDetailExists(int id)
        //{
        //    return _context.PaymentDetails.Any(e => e.Id == id);
        //}
    }
}
