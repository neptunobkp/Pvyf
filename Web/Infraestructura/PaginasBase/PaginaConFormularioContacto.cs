using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;
using Web.Model;

namespace Web.Infraestructura.PaginasBase
{
    public class PaginaConFormularioContacto : PageModel
    {
        protected readonly IHostingEnvironment _hostingEnvironment;
        protected readonly ApplicationDbContext _db;

        public PaginaConFormularioContacto(IHostingEnvironment hostingEnvironment, ApplicationDbContext db)
        {
            _hostingEnvironment = hostingEnvironment;
            _db = db;
        }
        protected async Task GuardarContacto(SolicitudContacto vm)
        {
            await _db.SolicitudesContacto.AddAsync(vm);
            await _db.SaveChangesAsync();
            try
            {
                EnviarEmail(vm);
            }
            catch (Exception)
            {
            }
        }


        public void EnviarEmail(SolicitudContacto vm)
        {
            using (MailMessage mm = new MailMessage("josezeta17@gmail.com", vm.Correo))
            {
                mm.Subject = "contacto carnovum ";
                mm.Body = "Muchas gracias por su solicitud de contacto";
                mm.IsBodyHtml = false;
                mm.CC.Add("jose@pulse-it.cl");

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("josezeta17@gmail.com", "dydlebumokpurezt");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
        }
    }
}
