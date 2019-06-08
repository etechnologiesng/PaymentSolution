using PaymentSolution.Core.DTO;
using PaymentSolution.Core.Service;
using PaymentSolution.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSolution.Core.Application.Interface
{
   public interface IPayment
    {
        Task<PaymentResult> MakePayment(PaymentDetailsDTO paymentDetails);

    }
}
