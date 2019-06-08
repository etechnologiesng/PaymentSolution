using PaymentSolution.Core.Application.Interface;
using PaymentSolution.Core.DTO;
using PaymentSolution.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSolution.Core.Service
{
    public class PaymentService : IPayment


    {


        public PaymentService()
        {

        }
        public Task<PaymentResult> MakePayment(PaymentDetailsDTO paymentDetails)
        {
            throw new NotImplementedException();
        }
    }
}
