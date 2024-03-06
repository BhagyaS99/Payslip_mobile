using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaySlipBackend.Models
{
    public class PayDetails
    {
            [Key]
            public int TransactionId { get; set; }

            public int EmpId { get; set; }

            [ForeignKey("EmpId")]
            public virtual Employee Employee { get; set; }

            public string PayPeriod { get; set; }

            public double BasicPay { get; set; }

            public double HouseRentAllowance { get; set; }

            public double MealAllowance { get; set; }

            public double BPdeduction { get; set; }
            public double HAdeduction { get; set; }

            public double MAdeduction { get; set; }
        
    }

    public class PayDetailsDTO
    {
        public string PayPeriod { get; set; }

        public double BasicPay { get; set; }

        public double HouseRentAllowance { get; set; }

        public double MealAllowance { get; set; }

        public double BPdeduction { get; set; }
        public double HAdeduction { get; set; }

        public double MAdeduction { get; set; }


    }
}
