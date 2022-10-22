namespace Web.Services.Prismic.Model.Vehiculo
{
    public class VehiculoResponse
    {
        public ResponseResult[] results { get; set; }
    }
    public class ResponseResult
    {
        public string uid { get; set; }
        public ResponseData data { get; set; }
    }

    public class ResponseData
    {
        public ResponseDataItem[] titulo { get; set; }
        public string fecha_de_publicacion { get; set; }
        public ResponseDataItemUrl foto_principal { get; set; }
        public ResponseDataItem[] descripcion { get; set; }
        public string precio { get; set; }
    }


    public class ResponseDataItem
    {
        public string text { get; set; }
    }

    public class ResponseDataItemUrl
    {
        public string url { get; set; }
    }
}
