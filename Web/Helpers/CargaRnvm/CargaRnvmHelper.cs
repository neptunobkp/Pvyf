using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Model;

namespace Web.Helpers.CargaRnvm
{
    public class CargaRnvmHelper
    {
        public void Procesar()
        {
            ProcesarArchivo(@"C:\Users\nicof\Desktop\PPU_TU_2020_SISTEMAS (1)\PPU_TU_2020_SISTEMAS_000009.txt");
            ProcesarArchivo(@"C:\Users\nicof\Desktop\PPU_TU_2020_SISTEMAS (1)\PPU_TU_2020_SISTEMAS_000010.txt");
        }

        private void ProcesarArchivo(string cadaArchivo)
        {
            var lineas = LeerRegistrosCsv(cadaArchivo);

            using (var cn =
                new SqlConnection(
                    "Database=CarnovumDB;Server=tcp:carnovum.database.windows.net,1433;User=CarnovumDBA;Password=1Tennis1;"))
            {
                cn.Open();
                using (var bulkCopy = new SqlBulkCopy(cn))
                {
                    bulkCopy.DestinationTableName = "VehiculosPlanos";
                    bulkCopy.BulkCopyTimeout = 0;
                    var datatable = ToDataTable(lineas);
                    bulkCopy.WriteToServer(datatable);
                }
            }
        }

        public List<VehiculoPlano> LeerRegistrosCsv(string ruta)
        {
            List<VehiculoPlano> resultado = new List<VehiculoPlano>();
            Encoding iso = Encoding.GetEncoding("ISO-8859-1");
            using (var reader = new StreamReader(ruta, iso))
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

        public DataTable ToDataTable(List<VehiculoPlano> items)
        {
            DataTable dataTable = new DataTable("VehiculoPlano");
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("Patente");
            dataTable.Columns.Add("Marca");
            dataTable.Columns.Add("Modelo");
            dataTable.Columns.Add("TipoVehiculo");
            dataTable.Columns.Add("Color");
            dataTable.Columns.Add("Anio");
            dataTable.Columns.Add("Tasacion");
            dataTable.Columns.Add("NumeroChasis");
            dataTable.Columns.Add("NumeroMotor");
            foreach (VehiculoPlano item in items)
            {
                dataTable.Rows.Add(0, item.Patente, item.Marca, item.Modelo, item.TipoVehiculo, item.Color, item.Anio, item.Tasacion, item.NumeroChasis, item.NumeroMotor);
            }
            return dataTable;
        }

        public VehiculoPlano LeerLinea(string[] linea)
        {
            VehiculoPlano resultado = new VehiculoPlano();
            resultado.Patente = linea[(int)IndiceRnvmCsv.Patente]?.Trim();
            resultado.Marca = linea[(int)IndiceRnvmCsv.Marca]?.Trim();

            resultado.Modelo = linea[(int)IndiceRnvmCsv.Modelo]?.Trim();
            resultado.Color = linea[(int)IndiceRnvmCsv.Color]?.Trim();
            resultado.TipoVehiculo = linea[(int)IndiceRnvmCsv.TipoVehiculo]?.Trim();
            resultado.Anio = linea[(int)IndiceRnvmCsv.Anio]?.Trim();
            resultado.NumeroChasis = linea[(int)IndiceRnvmCsv.NumeroChasis]?.Trim();
            resultado.NumeroMotor = linea[(int)IndiceRnvmCsv.NumeroMotor]?.Trim();
            resultado.Tasacion = linea[(int)IndiceRnvmCsv.Tasacion]?.Trim();
            return resultado;
        }
    }
}
