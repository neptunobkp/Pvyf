using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Web.Data;
using Web.Helpers.CargaRnvm;
using Web.Model;

namespace Web.Helpers.CargaSii
{
    public class CargaSiiHelper
    {
        public void Procesar(ApplicationDbContext db)
        {
            var lineas = LeerRegistrosCsv();
            var marcasDistintas = lineas.Select(t => t.Marca).Distinct();
            foreach (var cadaMarca in marcasDistintas)
            {
                try
                {
                    CargarMarca(db, cadaMarca, lineas);
                }
                catch (Exception ex)
                {
                }
            }
        }


        private void CargarMarca(ApplicationDbContext db, string cadaMarca, List<LineaSiiCsv> lineas)
        {
            var marcaCreada = db.Marcas.Add(new Marca {Nombre = cadaMarca});
            var lineasMarcas = lineas.Where(t => t.Marca == cadaMarca);
            var modelosDistintos = lineasMarcas.Select(t => t.Modelo).Distinct();
            foreach (var cadaModelo in modelosDistintos)
            {
                var modeloCreado = db.Modelos.Add(new Modelo {Nombre = cadaModelo, MarcaId = marcaCreada.Entity.Id});
                var lineasModelos = lineasMarcas.Where(t => t.Modelo == cadaModelo);
                var versionesDistintas = lineasModelos.Select(t => t.Version).Distinct();
                foreach (var cadaVersion in versionesDistintas)
                {
                    var lineasVersion = lineasModelos.Where(t => t.Version == cadaVersion);
                    var versionMuestra = lineasVersion.Any(t => t.Anio == 2018)
                        ? lineasVersion.First(t => t.Anio == 2018)
                        : lineasVersion.First();
                    var versionMapeada = ContruirVersion(versionMuestra, modeloCreado.Entity.Id);
                    var versionCreada = db.Versiones.Add(versionMapeada);
                    foreach (var cadaLinea in lineasVersion)
                    {
                        if (cadaLinea.Tasacion.GetValueOrDefault() > 0)
                        {
                            var nuevaTasacion = new Tasacion();
                            nuevaTasacion.VersionId = versionCreada.Entity.Id;
                            nuevaTasacion.Anio = cadaLinea.Anio.GetValueOrDefault();
                            nuevaTasacion.Monto = cadaLinea.Tasacion.GetValueOrDefault();
                            db.Tasaciones.Add(nuevaTasacion);
                        }
                    }
                }
            }

            db.SaveChanges();
        }

        private Versiona ContruirVersion(LineaSiiCsv version2018, int modeloId)
        {
            var resultado = new Versiona();
            resultado.ModeloId = modeloId;
            resultado.Nombre = version2018.Version;
            resultado.TipoVehiculo = version2018.Tipo;
            resultado.Combustible = version2018.Combustible;

            resultado.Cilindrada = version2018.Cilindrada;
            resultado.Puertas = version2018.Puertas;
            resultado.Hp = version2018.Hp;
            resultado.Transmision = version2018.Transmision;
            resultado.Marchas = version2018.Marchas;
            resultado.Traccion = version2018.Traccion;
            resultado.PaisFabricacion = version2018.Pais;
            resultado.EquipamientoDetallado = version2018.Equipamiento;
            if (!string.IsNullOrEmpty(resultado.EquipamientoDetallado))
            {
                var equipamientos = resultado.EquipamientoDetallado.Split(',');
                resultado.CierreCentralizado = equipamientos.Any(t => t == "CC");
                resultado.EspejosElectrico = equipamientos.Any(t => t == "CAP" || t == "AET");
                resultado.AireAcondicionado = equipamientos.Any(t => t == "AA");
                resultado.Climatizador = equipamientos.Any(t => t == "CLZ");
                resultado.CamaraRetroceso = equipamientos.Any(t => t == "CR");
                resultado.SensorEstacionamiento = equipamientos.Any(t => t == "SDP");
                resultado.FrenosAsistidos = equipamientos.Any(t => t == "ABS" || t == "AAF");
                resultado.AirbagPasajero = equipamientos.Any(t => t == "ABP");
                resultado.AirbagLaterales = equipamientos.Any(t => t == "ALC");
                resultado.LlantasAleacion = equipamientos.Any(t => t == "LAL");
                resultado.FijacionAsientosNinos = equipamientos.Any(t => t == "AIN");
                resultado.Aleron = equipamientos.Any(t => t == "STR");
            }

            return resultado;
        }

        public List<LineaSiiCsv> LeerRegistrosCsv()
        {
            List<LineaSiiCsv> resultado = new List<LineaSiiCsv>();
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            using (var reader = new StreamReader(@"C:\Dessarrollo\pvyf\misc\data_sii_2019_master.csv", iso))
            {
                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine().Split(';');
                    if (linea.Length > 1)
                    {
                        var item = LeerLinea(linea);
                        resultado.Add(item);
                    }
                }
            }
            return resultado;
        }

        public LineaSiiCsv LeerLinea(string[] linea)
        {
            LineaSiiCsv resultado = new LineaSiiCsv();
            int parser;
            resultado.Anio = int.TryParse(linea[(int)IndiceColumnasSii.Anio], out parser) ? parser : (int?)null;

            resultado.Tipo = linea[(int)IndiceColumnasSii.Tipo];
            resultado.Marca = linea[(int)IndiceColumnasSii.Marca];
            resultado.Marca = resultado.Marca.Trim();

            resultado.Modelo = linea[(int)IndiceColumnasSii.Modelo];
            resultado.Modelo = resultado.Modelo.Trim();

            resultado.Version = linea[(int)IndiceColumnasSii.Version];
            resultado.Version = resultado.Version.Trim();

            resultado.Puertas = int.TryParse(linea[(int)IndiceColumnasSii.Puertas], out parser) ? parser : (int?)null;
            resultado.Cilindrada = int.TryParse(linea[(int)IndiceColumnasSii.Cilindrada], out parser) ? parser : (int?)null;
            resultado.Hp = int.TryParse(linea[(int)IndiceColumnasSii.Hp], out parser) ? parser : (int?)null;

            resultado.Combustible = linea[(int)IndiceColumnasSii.Combustible];
            resultado.Transmision = linea[(int)IndiceColumnasSii.Transmision];
            resultado.Transmision = resultado.Transmision.Trim();

            if (string.Compare(resultado.Transmision, "Automatica", StringComparison.InvariantCultureIgnoreCase) == 0
                || string.Compare(resultado.Transmision, "Automatizada", StringComparison.InvariantCultureIgnoreCase) == 0)
                resultado.Transmision = "Automatica";


            resultado.Marchas = int.TryParse(linea[(int)IndiceColumnasSii.Marchas], out parser) ? parser : (int?)null;
            resultado.Traccion = linea[(int)IndiceColumnasSii.Traccion];

            resultado.Pais = linea[(int)IndiceColumnasSii.Pais];
            resultado.Equipamiento = linea[(int)IndiceColumnasSii.Equipamiento];

            var tasacionLiteral = linea[(int) IndiceColumnasSii.Tasacion];
            tasacionLiteral = tasacionLiteral.Replace(".", "");
            tasacionLiteral = tasacionLiteral.Replace(",", "");
            resultado.Tasacion = int.TryParse(tasacionLiteral, out parser) ? parser : (int?)null;

            return resultado;
        }
    }
}
