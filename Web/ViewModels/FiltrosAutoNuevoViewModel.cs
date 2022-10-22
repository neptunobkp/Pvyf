using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class FiltrosAutoNuevoViewModel
    {
        public string Tipo { get; set; }
        public int? MarcaId { get; set; }
        public int? ModeloId { get; set; }
        public int? RegionId { get; set; }
        public int? VersionId { get; set; }
        public string TipoVehiculo { get; set; }
        public int? PrecioMin { get; set; }
        public int? PrecioMax { get; set; }

        public int? CantidadAsientos { get; set; }
        public  string TipoCombustible { get; set; }

        public string TipoTransmision { get; set; }

        public string ConsumoPromedio { get; set; }

        public int? AnioMin { get; set; }
        public int? AnioMax { get; set; }

        public string PalabraClave { get; set; }

        public string ValorCombustible { get; set; }
        public string ValorMotorTfs { get; set; }

        public string ValorHp { get; set; }
        public string ValorTorqueNm { get; set; }
        public string ValorVFinalKmH { get; set; }

        public string ValorTraccion { get; set; }
        public string ValorTransmision { get; set; }
        public string ValorSensorEstacionamiento { get; set; }

        public string ValorPaisFabricacion { get; set; }


        public bool ValorFrenosAsistidos { get; set; }
        public bool ValorFarosLed { get; set; }
        public bool ValorLlantasAleacion { get; set; }


        public bool ValorAlarma { get; set; }
        public bool ValorCierreCentralizado { get; set; }

        public bool ValorCamaraRetroceso { get; set; }
        public bool ValorRuedaRepuesto { get; set; }

        public bool ValorControlCrucero { get; set; }

        public bool ValorClimatizador { get; set; }
        public bool ValorAireAcondicionado { get; set; }
        public bool ValorBluetooth { get; set; }

        public bool ValorStartStop { get; set; }

        public bool ValorEspejosElectrico { get; set; }
        public bool ValorAleron { get; set; }
        public bool ValorAperturaElectricaMaletero { get; set; }
    }
}
