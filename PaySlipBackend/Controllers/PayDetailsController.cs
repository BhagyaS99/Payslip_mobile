using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PaySlipBackend.Context;
using PaySlipBackend.Models;

namespace PaySlipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayDetailsController : ControllerBase
    {
        private readonly AppDbContext _PayDetailsContext;
        public PayDetailsController(AppDbContext appDbContext)
        {
            _PayDetailsContext = appDbContext;
        }

        [HttpPost("PostPayDetails")]
        public async Task<IActionResult> PostPayDetails([FromBody] PayDetailsDTO paydetails /* , string empname */ )
        {
              if (Request.Headers.TryGetValue("empname-header", out var empname))
              {
                  // Use the header value
                  Console.WriteLine("post pay detaiils");
                  Console.WriteLine(empname);
              }
          

            Console.WriteLine(empname);

            var EmployeeName=empname.ToString();

            if (paydetails != null)
            {
                var _paydetails=new PayDetails();
                var emp = _PayDetailsContext.Employees
                                    .Where(s => s.EmployeeName == EmployeeName)
                                    .ToList();
                Console.WriteLine("emp[0].EmpId");
                Console.WriteLine(emp[0].EmpId);
             _paydetails.PayPeriod = paydetails.PayPeriod;
                _paydetails.BasicPay= paydetails.BasicPay;
                _paydetails.MealAllowance = paydetails.MealAllowance;
                _paydetails.HouseRentAllowance = paydetails.HouseRentAllowance;
                _paydetails.BPdeduction= paydetails.BPdeduction;
                _paydetails.MAdeduction = paydetails.MAdeduction;
                _paydetails.HAdeduction= paydetails.HAdeduction;
                _paydetails.EmpId= emp[0].EmpId;

                Console.WriteLine(" _paydetails.HAdeduction");
                Console.WriteLine(_paydetails.HAdeduction);

                Console.WriteLine(" paydetails.HAdeduction");
                Console.WriteLine(paydetails.HAdeduction);

                await _PayDetailsContext.PayDetails.AddAsync(_paydetails);
                await _PayDetailsContext.SaveChangesAsync();
                return Ok(new
                {
                    Message = "PayDetails Added"
                });
            }
            else
            {
                return BadRequest("Bad request");
            }

           // return Ok(200);
        }



       //[HttpPut("UpdatePayDetails")]
       // public IActionResult UpdateInvoiceDetails( /*[FromBody] InvoiceDetails invDetailsint id, */[FromBody] PayDetailsDTO detailsDTO)
       // {
       //     if (Request.Headers.TryGetValue("transID-header", out var id))
       //     {
       //         // Use the header value
       //         Console.WriteLine("in if");
       //         Console.WriteLine(id);
       //     }
       //     // int.TryParse(id, out var a);
       //     var invoiceDetails = _.InvoicesDetails.FirstOrDefault(s => s.TransactionID == Int32.Parse(id));
       //     if (invoiceDetails != null)


       //     {
       //         Console.WriteLine("old amt");

       //         var old_total = invoiceDetails.Total;
       //         Console.WriteLine(old_total);
       //         invoiceDetails.From_Date = detailsDTO.From_Date;
       //         invoiceDetails.To_Date = detailsDTO.To_Date;
       //         invoiceDetails.Details = detailsDTO.Details;
       //         // invoice.Task= detailsDTO.Task;
       //         Console.WriteLine("nnnnnnnnnnnnnn");
       //         Console.WriteLine(invoiceDetails.Task);
       //         invoiceDetails.Hours_Worked = detailsDTO.Hours_Worked;
       //         invoiceDetails.Hourly_Rate = detailsDTO.Hourly_Rate;

       //         invoiceDetails.Total = detailsDTO.Hours_Worked * detailsDTO.Hourly_Rate;
       //         Console.WriteLine(invoiceDetails.Total);
       //         //  _InvoiceContext.Update(invoiceDetails);
       //         //  _InvoiceContext.SaveChanges();

       //         var invoice = _InvoiceContext.Invoices.FirstOrDefault(s => s.InvoiceId == invoiceDetails.InvoiceId);
       //         var amt = invoice.InvoiceAmt;
       //         Console.WriteLine(amt);
       //         Console.WriteLine(amt - old_total);
       //         amt = (amt - old_total) + (detailsDTO.Hours_Worked * detailsDTO.Hourly_Rate);
       //         Console.WriteLine(amt);
       //         invoice.InvoiceAmt = amt;

       //         _InvoiceContext.UpdateRange(invoice, invoiceDetails);
       //         _InvoiceContext.SaveChanges();

       //         return Ok(new
       //         {
       //             Message = "Invoice Updated"
       //         });
       //     }


       //     /* var invoice = _InvoiceContext.InvoicesDetails.FirstOrDefault(s => s.TransactionID == id);

       // if (invoice != null)
       // {
       //     invoice.Task = "John";

       //     _InvoiceContext.Update(invoice);
       //     _InvoiceContext.SaveChanges();

       //     return Ok(new
       //     {
       //         Message = "Invoice Added"
       //     });
       // }*/

       //     return BadRequest();
       // }
    }
}
