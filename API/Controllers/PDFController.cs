//using System;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using BusinessObject.Model;
//using DataAccess;
//using DinkToPdf;
//using DinkToPdf.Contracts;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PdfController : ControllerBase
//    {
//        private readonly IConverter _converter;
//        private readonly DBContext _context;
//        public UserExam user { get; set; }
//        public Exam e { get; set; }
//        public User u { get; set; }


//        public PdfController(IConverter converter, DBContext context)
//        {
//            _converter = converter;
//            _context = context;
//        }
//        [HttpGet("generate")]
//        public async Task<IActionResult> GeneratePdf(int userExamId)
//        {
//            user = _context.UserExams
//                    .Where(d => d.UserExamId == userExamId)
//                    .SingleOrDefault();
//            if (user != null)
//            {
//                u = await _context.Users
//                    .Where(d => d.UserId == user.UserId)
//                    .SingleOrDefaultAsync();
//                e = await _context.Exams.Where(e => e.ExamId == user.ExamId).FirstOrDefaultAsync();
//                string fileName = "Certificate.pdf";
//                var globalSettings = new GlobalSettings
//                {
//                    ColorMode = ColorMode.Color,
//                    Orientation = Orientation.Landscape,
//                    PaperSize = PaperKind.A4,
//                    Margins = new MarginSettings
//                    {
//                        Bottom = 20,
//                        Left = 20,
//                        Right = 20,
//                        Top = 30
//                    },
//                    DocumentTitle = "Certificate"
//                };

//                var objectSettings = new ObjectSettings
//                {
//                    PagesCount = true,
//                    HtmlContent = @"
//<!DOCTYPE html>
//<html lang='vi'>
//<head>
//    <meta charset='UTF-8'>
//    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
//    <title>Chứng Chỉ</title>
//    <style>
//    body {
//            font-family: 'Arial', sans-serif;
//            display: block; /* Thay đổi từ flex thành block */
//            margin: 0;
//            background-color: #f0f0f0;
//        }
//        .container {
//            width: 100%;
//            display: flex;
//            justify-content: flex-start; /* Thay đổi từ center thành flex-start */
//            align-items: flex-start; /* Thay đổi từ center thành flex-start */
//        }
//        .chungchi {
//            width: 80%;
//            max-width: 800px;
//            padding: 2em;
//            border: 1em solid #8c8c8c;
//            background: white;
//            box-shadow: 0 0 20px rgba(0, 0, 0, 0.25);
//            position: relative;
//            margin: 0 auto; /* Thêm margin: 0 auto để căn giữa .chungchi trong .container */
//        }
//        .chungchi::before,
//        .chungchi::after {
//            content: '';
//            position: absolute;
//            border: 0.5em solid #d4af37;
//            border-radius: 1.25em;
//            top: -1.25em;
//            left: -1.25em;
//            right: -1.25em;
//            bottom: -1.25em;
//            z-index: -1;
//        }
//        .chungchi::before {
//            border-color: #d4af37 transparent transparent transparent;
//            border-width: 1.25em;
//            top: -1.25em;
//            left: -1.25em;
//            right: -1.25em;
//            bottom: -1.25em;
//            border-radius: 2.5em;
//        }
//        .chungchi h1 {
//            font-size: 1.5em;
//            margin-bottom: 1em;
//            text-align: center;
//        }
//        .chungchi h2 {
//            font-size: 1.25em;
//            margin-bottom: 0.5em;
//            text-align: center;
//        }
//        .chungchi p {
//            font-size: 1em;
//            text-align: center;
//            margin: 0.5em 0;
//        }
//        .chungchi .dau {
//            display: flex;
//            justify-content: center;
//            margin: 1.5em 0;
//        }
//        .chungchi .dau img {
//            width: 6.25em;
//            height: 6.25em;
//        }
//        .chungchi .chuky {
//            display: flex;
//            justify-content: space-between;
//            margin-top: 1.5em;
//        }
//        .chungchi .chuky div {
//            text-align: center;
//        }
//        .chungchi .chuky div p {
//            margin: 0.25em 0;
//        }

//        #ten-truong {
//            font-size: 1.5em;
//            margin-bottom: 1em;
//            text-align: center;
//        }

//        #chung-nhan {
//            font-size: 1.25em;
//            margin-bottom: 0.5em;
//            text-align: center;
//        }

//        #ten-chung-chi {
//            font-size: 1.5em;
//            margin-bottom: 1em;
//            text-align: center;
//        }

//        #duoc-trao-cho {
//            font-size: 1.25em;
//            margin-bottom: 0.5em;
//            text-align: center;
//        }

//        #ten-hoc-vien {
//            font-size: 1.5em;
//            margin-bottom: 1em;
//            text-align: center;
//        }

//        #noi-dung {
//            font-size: 1em;
//            text-align: center;
//            margin: 0.5em 0;
//        }

//        #chuyen-nganh {
//            font-size: 1.25em;
//            margin-bottom: 0.5em;
//            text-align: center;
//        }

//        #ngay-cap {
//            font-size: 1em;
//            text-align: center;
//            margin: 0.5em 0;
//        }

//        @media (max-width: 768px) {
//            .chungchi {
//                width: 90%;
//                padding: 1.5em;
//            }
//            .chungchi h1, .chungchi h2, .chungchi p {
//                font-size: 0.875em;
//            }
//            .chungchi .dau img {
//                width: 4.375em;
//                height: 4.375em;
//            }
//            .chungchi .chuky {
//                flex-direction: column;
//                margin-top: 1em;
//            }
//            .chungchi .chuky div {
//                margin-bottom: 1em;
//            }
//        }
//    </style>
//</head>
//<body>
//    <div class='container'>
//        <div class='chungchi'>
//            <h1 id='ten-truong'>Hệ Thống Trắc Nghiệm Tự Động</h1>
//            <h2 id='chung-nhan'>Chứng nhận rằng</h2>
//            <h1 id='ten-chung-chi'>Chứng Chỉ {{chungchi}}</h1>
//            <h2 id='duoc-trao-cho'>Được Trao Cho</h2>
//            <h1 id='ten-hoc-vien'>{{fullname}}</h1>
//            <p id='noi-dung'>Vì đã hoàn thành xuất sắc chương trình chuyên sâu</p>
//            <h2 id='chuyen-nganh'>{{content}}</h2>
//            <p id='ngay-cap'>Được cấp tại Cần Thơ, Việt Nam</p>
//            <div class='dau'>
//                <img src='https://testrazorpage.000webhostapp.com/2.png' alt=''>
//            </div>
//            <div class='chuky'>
//                <div>
//                    <p>__________________________</p>
//                    <p>Hữu Kiến</p>
//                    <p>Giám đốc</p>
//                </div>
//                <div>
//                    <p>__________________________</p>
//                    <p>Hữu Kiến</p>
//                    <p>Người Tư Vấn Học Thuật</p>
//                </div>
//            </div>
//        </div>
//    </div>
//</body>
//</html>
//",
//                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
//                };
//                string htmlContent = objectSettings.HtmlContent
//                    .Replace("{{chungchi}}", e.Title)
//                    .Replace("{{fullname}}", u.FullName)
//                    .Replace("{{content}}", e.Description);
//                objectSettings = new ObjectSettings
//                {
//                    PagesCount = true,
//                    HtmlContent = htmlContent,
//                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = null }
//                };
//                var pdfDocument = new HtmlToPdfDocument
//                {
//                    GlobalSettings = globalSettings,
//                    Objects = { objectSettings }
//                };

//                byte[] pdfBytes = _converter.Convert(pdfDocument);
//                return File(pdfBytes, "application/pdf", fileName);
//            }
//            else
//            {
//                return BadRequest();
//            }

//        }


//    }
//}

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Model;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Xml.Linq;
using Repository.IRepository;
using System.Collections.Generic;
using System.Net.Mail;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly ISendMailService _sendMailService;
        public UserExam user { get; set; }
        public Exam e { get; set; }
        public User u { get; set; }

        public PdfController(DBContext context, ISendMailService sendMailService)
        {
            _context = context;
            _sendMailService = sendMailService;
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GeneratePdf(int userExamId)
        {
            var userExam = _context.UserExams
                    .Where(d => d.UserExamId == userExamId)
                    .SingleOrDefault();

            if (userExam != null)
            {
                var user = await _context.Users
                    .Where(d => d.UserId == userExam.UserId)
                    .SingleOrDefaultAsync();

                var exam = await _context.Exams
                    .Where(e => e.ExamId == userExam.ExamId)
                    .FirstOrDefaultAsync();

                string fileName = "Certificate.pdf";

                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Certificate";

                // Add a page to the document
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12, XFontStyle.Regular);

                // Draw the certificate content
                gfx.DrawString("Hệ Thống Trắc Nghiệm Tự Động", font, XBrushes.Black,
                    new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString("Chứng nhận rằng", font, XBrushes.Black,
                    new XRect(0, 40, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString($"Chứng Chỉ {exam.Title}", font, XBrushes.Black,
                    new XRect(0, 80, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString($"Được Trao Cho {user.FullName}", font, XBrushes.Black,
                    new XRect(0, 120, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString("Vì đã hoàn thành xuất sắc chương trình chuyên sâu", font, XBrushes.Black,
                    new XRect(0, 160, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString(exam.Description, font, XBrushes.Black,
                    new XRect(0, 200, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString("Được cấp tại Cần Thơ, Việt Nam", font, XBrushes.Black,
                    new XRect(0, 240, page.Width, page.Height), XStringFormats.TopCenter);

                gfx.DrawString("__________________________\nHữu Kiến\nGiám đốc", font, XBrushes.Black,
                    new XRect(100, 550, page.Width, page.Height), XStringFormats.TopLeft);

                gfx.DrawString("__________________________\nHữu Kiến\nNgười Tư Vấn Học Thuật", font, XBrushes.Black,
                    new XRect(400, 550, page.Width, page.Height), XStringFormats.TopLeft);

                // Save the document to a memory stream
                using (MemoryStream stream = new MemoryStream())
                {
                    document.Save(stream, false);
                    byte[] pdfBytes = stream.ToArray();

                    var email = user.Email;
                    var subject = "Thông tin chứng chỉ của bạn";
                    var body = $"Xin chào {user.FullName},\n\nDưới đây là chứng chỉ của bạn.\n\nTên chứng chỉ: {exam.Title}\n\nXin cảm ơn.";
                    await _sendMailService.SendEmailWithAttachmentAsync(email, subject, body, pdfBytes, fileName, "application/pdf");

                    return Ok("PDF đã được gửi qua email.");
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}

