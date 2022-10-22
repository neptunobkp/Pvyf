using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Model.CompraVende;
using Web.Model.Nuevos;
using Web.Model.Tipos;

namespace Web.Model
{
    public class Versiona
    {
        public bool Eliminado { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Modelo Modelo { get; set; }
        public int ModeloId { get; set; }
        public string TipoVehiculo { get; set; }
        public string Combustible { get; set; }
        public string MotorTfs { get; set; }
        public int? Hp { get; set; }
        public string TorqueNm { get; set; }
        public string VFinalKmH { get; set; }
        public string Traccion { get; set; }
        public string Transmision { get; set; }
        public string DiametroLlantas { get; set; }
        public string PaisFabricacion { get; set; }
        public bool SensorEstacionamiento { get; set; }
        public decimal? RCiudadKmLtr { get; set; }
        public decimal? RCarreteraKmLtr { get; set; }
        public int? Airbags { get; set; }
        public int Asientos { get; set; }
        public decimal? RMixto { get; set; }
        public bool FrenosAsistidos { get; set; }
        public bool FarosLed { get; set; }
        public bool LlantasAleacion { get; set; }
        public bool FijacionAsientosNinos { get; set; }
        public bool AirbagPasajero { get; set; }
        public bool AirbagLaterales { get; set; }
        public bool Alarma { get; set; }
        public bool CierreCentralizado { get; set; }
        public bool CamaraRetroceso { get; set; }
        public bool RuedaRepuesto { get; set; }

        public bool ControlCrucero { get; set; }

        public bool Climatizador { get; set; }
        public bool AireAcondicionado { get; set; }
        public bool Bluetooth { get; set; }
        public bool StartStop { get; set; }
        public bool EspejosElectrico { get; set; }
        public bool Aleron { get; set; }
        public bool AperturaElectricaMaletero { get; set; }
        public int? Cilindrada { get; set; }
        public int? Puertas { get; set; }
        public int? Marchas { get; set; }
        public string EquipamientoDetallado { get; set; }
        public ICollection<PublicacionNuevo> PublicacionesNuevo { get; set; }
        public ICollection<Vehiculo> Vehiculos { get; set; }
    }

}
