using iText.IO.Image;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.IO;
using PaySlipBackend.Context;
using PaySlipBackend.Models;
using PaySlipBackend.Utilities;
using System;
using System.IO;

namespace PaySlipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfGeneratorController : ControllerBase
    {

        private static byte[] pdfBytes;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _PdfContext;

        public PdfGeneratorController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _PdfContext = appDbContext;
            _env = env;
        }


        [HttpGet("CreatePdf")]
        public IActionResult CreatePdf(string empname)
        {
            Console.WriteLine(empname);
           
            var emp = _PdfContext.Employees
                                   .Where(s => s.EmployeeName == empname)
                                   .ToList();
            Console.WriteLine(emp[0].EmpId);
            Employee employee = emp[0];

            var _paydetails = _PdfContext.PayDetails
                                   .Where(s => s.EmpId == emp[0].EmpId)
                                   .ToList();
            PayDetails payDetails = _paydetails[0];
           
            Console.WriteLine(_paydetails[0].MAdeduction);

            double total_amt = payDetails.BasicPay + payDetails.HouseRentAllowance + payDetails.MealAllowance;
            Console.WriteLine(total_amt);

            double total_deduction = payDetails.BPdeduction + payDetails.HAdeduction + payDetails.MAdeduction;
            Console.WriteLine(total_deduction);

            int netpay = (int)(total_amt - total_deduction);
            Console.WriteLine(netpay);

            var obj = new Formatting();

            string netpaywords = obj.ConvertNumbertoWords(netpay);
            Console.WriteLine(netpaywords);

            MemoryStream memStream = new MemoryStream();

            iText.Kernel.Pdf.PdfDocument pdfDoc = new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfWriter(memStream));
            iText.Layout.Document document = new iText.Layout.Document(pdfDoc);

            var imagePath1 = _env.WebRootPath + "/Images/IMheader.png";
            //  String imageFile1 = Path.Combine(_env.WebRootPath, "images", "logo.png");
            ImageData data1 = ImageDataFactory.Create(imagePath1);

            iText.Layout.Element.Image img1 = new iText.Layout.Element.Image(data1);
            img1.SetFixedPosition(15, 730).ScaleAbsolute(565f, 100f);
            document.Add(img1);

            Paragraph para1 = new Paragraph("Pay Slip").SetFontSize(20).SetUnderline()
            .SetFixedPosition(250, 670, 100);
            document.Add((IBlockElement)para1);

            float[] colwidth = { 250f, 250f };
            Table emptable = new Table(colwidth).SetFixedPosition(100, 590, 500).SetBorder(Border.NO_BORDER);

            /*   emptable.AddCell("Date of Joining: "+employee.DOJ).SetFontSize(10).SetBorder(Border.NO_BORDER);
               emptable.AddCell("Employee Name: " + employee.EmployeeName).SetFontSize(10).SetBorder(Border.NO_BORDER);
            */
            Cell cell11 = new Cell()
             .SetBorder(Border.NO_BORDER)
             .SetTextAlignment(TextAlignment.LEFT)
             .SetFontSize(12)
         .Add(new iText.Layout.Element.Paragraph("Date of Joining: " + employee.DOJ));

            Cell cell12 = new Cell()
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(12)
            .Add(new iText.Layout.Element.Paragraph("Employee Name: " + employee.EmployeeName));


            Cell cell21 = new Cell()
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(12)
            .Add(new iText.Layout.Element.Paragraph("Pay Period: " + payDetails.PayPeriod));

            Cell cell22 = new Cell()
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(12)
            .Add(new iText.Layout.Element.Paragraph("Designation: " + employee.Designation));

            Cell cell31 = new Cell()
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(12)
            .Add(new iText.Layout.Element.Paragraph("PAN: " + employee.PAN));

            Cell cell32 = new Cell()
                .SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(12)
            .Add(new iText.Layout.Element.Paragraph("Department: " + employee.Department));

            emptable.AddCell(cell11);
            emptable.AddCell(cell12);
            emptable.AddCell(cell21);
            emptable.AddCell(cell22);
            emptable.AddCell(cell31);
            emptable.AddCell(cell32);

            float[] calccolwidth = { 120f, 100f, 140f, 100f };
            Table calctbl = new Table(calccolwidth).SetFixedPosition(50, 400, 480);
            calctbl.SetBorder(new SolidBorder(1));

            calctbl.AddCell("Earnings").SetFontSize(12).SetTextAlignment(TextAlignment.CENTER);
            calctbl.AddCell("Amount").SetFontSize(12);
            calctbl.AddCell("Deductions").SetFontSize(12);
            calctbl.AddCell("Amount").SetFontSize(12);

            Cell cellc11 = new Cell()
               .SetBorder(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph("Basic Pay"));
            calctbl.AddCell(cellc11).SetBorderBottom(Border.NO_BORDER);

            Cell cellc12 = new Cell()
               .SetBorderBottom(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph(payDetails.BasicPay.ToString()));
            calctbl.AddCell(cellc12);

            Cell cellc13 = new Cell()
               .SetBorderBottom(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph("Basic Pay Deduction"));
            calctbl.AddCell(cellc13);

            Cell cellc14 = new Cell()
                 .SetBorderBottom(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph(payDetails.BPdeduction.ToString()));
            calctbl.AddCell(cellc14);

            Cell cellc21 = new Cell()
               .SetBorderBottom(Border.NO_BORDER)
               .SetBorderTop(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph("House Rent Allowance"));
            calctbl.AddCell(cellc21);

            Cell cellc22 = new Cell()
              .SetBorderBottom(Border.NO_BORDER)
              .SetBorderTop(Border.NO_BORDER)
              .SetTextAlignment(TextAlignment.RIGHT)
              .SetFontSize(10)
          .Add(new iText.Layout.Element.Paragraph(payDetails.HouseRentAllowance.ToString()));
            calctbl.AddCell(cellc22);

            Cell cellc23 = new Cell()
               .SetBorderBottom(Border.NO_BORDER)
               .SetBorderTop(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph("House Rent Deduction"));
            calctbl.AddCell(cellc23);

            Cell cellc24 = new Cell()
          .SetBorderBottom(Border.NO_BORDER)
          .SetBorderTop(Border.NO_BORDER)
          .SetTextAlignment(TextAlignment.RIGHT)
          .SetFontSize(10)
      .Add(new iText.Layout.Element.Paragraph(payDetails.HAdeduction.ToString()));
            calctbl.AddCell(cellc24);


            Cell cellc31 = new Cell()
            .SetBorderBottom(Border.NO_BORDER)
            .SetBorderTop(Border.NO_BORDER)
            .SetTextAlignment(TextAlignment.LEFT)
            .SetFontSize(10)
        .Add(new iText.Layout.Element.Paragraph("Meal Allowance"));
            calctbl.AddCell(cellc31);

            Cell cellc32 = new Cell()
               .SetBorderBottom(Border.NO_BORDER)
            .SetBorderTop(Border.NO_BORDER)
              .SetTextAlignment(TextAlignment.RIGHT)
              .SetFontSize(10)
          .Add(new iText.Layout.Element.Paragraph(payDetails.MealAllowance.ToString()));
            calctbl.AddCell(cellc32);

            Cell cellc33 = new Cell()
                .SetBorderBottom(Border.NO_BORDER)
            .SetBorderTop(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph("Meal Allowance Reduction"));
            calctbl.AddCell(cellc33);

            Cell cellc34 = new Cell()
                .SetBorderBottom(Border.NO_BORDER)
            .SetBorderTop(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph(payDetails.MAdeduction.ToString()));
            calctbl.AddCell(cellc34);

            Cell cellc41 = new Cell()
          // .SetBorder(Border.NO_BORDER)
          // .SetBorderLeft(new SolidBorder(1f))
          .SetBorderBottom(Border.NO_BORDER)
               .SetBorderTop(Border.NO_BORDER)
         .SetTextAlignment(TextAlignment.RIGHT)
         .SetFontSize(10)
     .Add(new iText.Layout.Element.Paragraph("Total Earnings"));
            calctbl.AddCell(cellc41);

            Cell cellc42 = new Cell()
               //   .SetBorder(Border.NO_BORDER)
              .SetBorderBottom(Border.NO_BORDER)
               .SetBorderTop(Border.NO_BORDER)
              .SetTextAlignment(TextAlignment.RIGHT)
              .SetFontSize(10)
          .Add(new iText.Layout.Element.Paragraph(total_amt.ToString()));
            calctbl.AddCell(cellc42);

            Cell cellc43 = new Cell()
            // .SetBorder(Border.NO_BORDER)
          .SetBorderBottom(Border.NO_BORDER)
               .SetBorderTop(Border.NO_BORDER)
            .SetTextAlignment(TextAlignment.RIGHT)
              .SetFontSize(10)
            .Add(new iText.Layout.Element.Paragraph("Total Deductions"));
            calctbl.AddCell(cellc43);

            Cell cellc44 = new Cell()
                //.SetBorder(Border.NO_BORDER)
               .SetBorderBottom(Border.NO_BORDER)
             .SetBorderTop(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph(total_deduction.ToString()));
            calctbl.AddCell(cellc44);

            Cell cellc51 = new Cell()
                  .SetBorderTop(Border.NO_BORDER)
          .SetBorderBottom(new SolidBorder(1f))
           .SetTextAlignment(TextAlignment.RIGHT)
             .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph(" "));
            calctbl.AddCell(cellc51);

            Cell cellc52 = new Cell()
           .SetBorderBottom(new SolidBorder(1f))
            .SetBorderTop(Border.NO_BORDER)
           .SetTextAlignment(TextAlignment.RIGHT)
             .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph(" "));
            calctbl.AddCell(cellc52);



            Cell cellc53 = new Cell()
           .SetBorderBottom(new SolidBorder(1f))
            .SetBorderTop(Border.NO_BORDER)
           .SetTextAlignment(TextAlignment.RIGHT)
             .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph("Net Pay"));
            calctbl.AddCell(cellc53);

            Cell cellc54 = new Cell()
               .SetBorderBottom(new SolidBorder(1f))
                .SetBorderTop(Border.NO_BORDER)
               .SetTextAlignment(TextAlignment.RIGHT)
               .SetFontSize(10)
           .Add(new iText.Layout.Element.Paragraph(netpay.ToString()));
            calctbl.AddCell(cellc54);

            Paragraph para2 = new Paragraph(("Net Pay in words:" + netpaywords + " ONLY"))
           .SetFontSize(10)
           .SetBold()
          .SetFixedPosition(60, 300, 400);
            document.Add((IBlockElement)para2);


            var imagePath2 = _env.WebRootPath + "/Images/IMfooter.png";
            //  String imageFile1 = Path.Combine(_env.WebRootPath, "images", "logo.png");
            ImageData data2 = ImageDataFactory.Create(imagePath2);

            iText.Layout.Element.Image img2 = new iText.Layout.Element.Image(data2);
            img2.SetFixedPosition(0, 10).ScaleAbsolute(600f, 70f);
            document.Add(img2);



            document.Add(calctbl);
            document.Add(emptable);


            document.Close();

            pdfBytes = memStream.ToArray();

            memStream.Dispose();

            return File(pdfBytes, "application/pdf", "Payslip.pdf");

            //return Ok(200);


        }
    }
}
