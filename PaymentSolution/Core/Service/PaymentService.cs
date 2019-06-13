using Microsoft.EntityFrameworkCore;
using PaymentSolution.Core.Application.Interface;
using PaymentSolution.Core.DTO;
using PaymentSolution.Data;
using PaymentSolution.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSolution.Core.Service
{
    public class PaymentService : IPayment


    {
        private readonly PaymentDbContext  _context;

        public PaymentService(PaymentDbContext context)
        {
            _context = context;
        }
        public async Task<PaymentResult> MakePayment(PaymentDetailsDTO paymentDetails)
        {

            var newPayment = new PaymentDetail
            {
                CardNumber = paymentDetails.CardNumber,
                CVV = paymentDetails.CVV,
                CardOwnerName = paymentDetails.CardOwnerName,
                ExpirationDate = paymentDetails.ExpirationDate
            };

         await   _context.PaymentDetails.AddAsync(newPayment);
        await    _context.SaveChangesAsync();

            return new PaymentResult() { IsComplete = true};

        }



        public async Task<IEnumerable<PaymentDetailsDTO>> GetPayments()
        {
            var paymentsDTO = (from payment in _context.PaymentDetails

                               select new PaymentDetailsDTO()
                               {
                                   CardNumber = payment.CardNumber,
                                   CVV = payment.CVV,
                                   CardOwnerName = payment.CardOwnerName,
                                   ExpirationDate = payment.ExpirationDate

                               }).ToListAsync();

            return await paymentsDTO;
        }
    }
}
