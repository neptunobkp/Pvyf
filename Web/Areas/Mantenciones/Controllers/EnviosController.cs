using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.ViewModels;

namespace Web.Areas.Mantenciones.Controllers
{
    [Area("Mantenciones")]
    [Route("[area]/api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private ApplicationDbContext _db;

        public EnviosController(IHostingEnvironment hostingEnvironment, ApplicationDbContext db)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }


        [HttpPost]
        public void Post([FromForm] EnvioVoucher payload)
        {
            var lugarServicioId = Convert.ToInt32(payload.IdServicio);
            var lugarServicio = _db.LugaresServicios.Find(lugarServicioId);
            var nombrePdf = $"V{lugarServicio.Servicios.Split(',').First()}.pdf";

            using (MailMessage mm = new MailMessage("auxilia@auxilia.cl", payload.Destinatario))
            {
                mm.Subject = "carnovum voucher";
                mm.Body = "adjunto encontraras el voucher";
                mm.Attachments.Add(new Attachment(Path.Combine(_hostingEnvironment.WebRootPath, nombrePdf)));
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("auxilia@auxilia.cl", "Auxilia2014");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
        }
    }
}