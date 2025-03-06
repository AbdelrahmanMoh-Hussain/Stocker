using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Web.ViewModels
{
    public class BuyStockViewModel
    {

        [Required]
        public string Stock { get; set; }

        [Range(0, int.MaxValue)]
        public int SharesCount { get; set; }

        [ValidateNever]
        public IEnumerable<string> StocksList { get; set; }
    }
}
