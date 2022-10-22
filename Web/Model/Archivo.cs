using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Model
{
    public class Archivo
    {
        public int Id { get; set; }
        public byte[] ArchivoBytes { get; set; }
        public string NombreArchivo { get; set; }
        public string Extension { get; set; }
    }
}
