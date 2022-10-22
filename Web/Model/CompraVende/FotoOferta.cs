namespace Web.Model.CompraVende
{
    public class FotoPublicacionUsado
    {
        public int Id { get; set; }
        public int PublicacionUsadoId { get; set; }
        public PublicacionUsado PublicacionUsado { get; set; }
        public int ArchivoId { get; set; }
        public Archivo Archivo { get; set; }
    }
}