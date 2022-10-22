namespace Web.Model.Nuevos
{
    public class FotoPublicacionNuevo
    {
        public int Id { get; set; }
        public int PublicacionNuevoId { get; set; }
        public PublicacionNuevo PublicacionNuevo { get; set; }
        public int ArchivoId { get; set; }
        public Archivo Archivo { get; set; }
    }
}