using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSolution.Model.Entity
{
    public class PaymentDetail
    {

        
        public int Id { get; set; }
       
        [Required]
        public string CardOwnerName { get; set; }
    
            [Required]
        public string CardNumber { get; set; }
        
[Required]
        public string ExpirationDate { get; set; }
        
        [Required]
        public string CVV { get; set; }
    }
}

