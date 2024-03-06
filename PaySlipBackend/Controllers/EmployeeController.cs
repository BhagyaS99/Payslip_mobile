using iText.IO.Image;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using PaySlipBackend.Context;
using PaySlipBackend.Models;
using PaySlipBackend.Utilities;
using System;

namespace PaySlipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //private static byte[] pdfBytes;
      //  private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _EmployeeContext;

        public EmployeeController(AppDbContext appDbContext)
        {
            _EmployeeContext = appDbContext;
        }

        //public EmployeeController(IWebHostEnvironment env)
        //{
        //    _env = env;
        //}



        [HttpPost("PostEmployee")]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
      {
            Console.WriteLine("in emp action method");
            Console.WriteLine(employee.DOJ);
            if (employee != null)
            {
                await _EmployeeContext.Employees.AddAsync(employee);
                await _EmployeeContext.SaveChangesAsync();
                return Ok(new
                {
                    Message = "Employee Added"
                });
            }
            else
            {
                return BadRequest("Bad request");
            }

            
      }

        [HttpGet("getEmployee")]
        public List<string> getEmployee()
        {

            var EmpNameList = _EmployeeContext.Employees
    .Select(s => s.EmployeeName).ToList();
            foreach (var item in EmpNameList)
            {
                Console.WriteLine(item);
            }
            //  return Ok(CustomerNameList);
            return EmpNameList;

        }


    }
}
