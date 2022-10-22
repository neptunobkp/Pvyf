using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Web.Services.Prismic.Model;
using Web.Services.Prismic.Model.Vehiculo;
using Web.ViewModels;

namespace Web.Services.Prismic
{
    public interface IPrismicStub
    {
        Task<LandingPageViewModel> GetLanding();
        Task<List<VehiculoViewModel>> GetVehiculos();
    }

    public class PrismicStub : IPrismicStub
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public string CurrentRef { get; set; }
        private string AccessToken => _configuration.GetValue<string>("Prismic:Token");
        public PrismicStub(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<LandingPageViewModel> GetLanding()
        {
            var _ref = await GetRef();

            var response = await _client.GetAsync($"/api/v2/documents/search?access_token={HttpUtility.UrlEncode(AccessToken)}&ref={_ref}&q={HttpUtility.UrlEncode("[[at(document.type,\"landing\")]]")}");
            var contentResult = await response.Content.ReadAsAsync<Response>();

            var result = new LandingPageViewModel
            {
                Titulo = contentResult.results.First().data.titulo.First().text,
                Hero = contentResult.results.First().data.texto_hero.First().text,
                Footer = contentResult.results.First().data.footer.First().text
            };

            result.Vehiculos = await GetVehiculos();

            return result;
        }

        private async Task<string> GetRef()
        {
            if (!string.IsNullOrEmpty(CurrentRef))
                return CurrentRef;

            var apiget = await _client.GetAsync($"/api/v2?access_token={HttpUtility.UrlEncode(AccessToken)}");
            var apigetResponse = await apiget.Content.ReadAsAsync<Get>();
            CurrentRef = apigetResponse.refs.First().@ref;
            return CurrentRef;
        }

        public async Task<List<VehiculoViewModel>> GetVehiculos()
        {
            var _ref = await GetRef();

            var response = await _client.GetAsync($"/api/v2/documents/search?access_token={HttpUtility.UrlEncode(AccessToken)}&ref={_ref}&q={HttpUtility.UrlEncode("[[at(document.type,\"vehiculo\")]]")}");
            var contentResult = await response.Content.ReadAsAsync<VehiculoResponse>();

            var resultados = new List<VehiculoViewModel>();

            foreach (var cadaResult in contentResult.results)
            {
                resultados.Add(new VehiculoViewModel
                {
                    Titulo = cadaResult.data.titulo.First().text,
                    FechaPublicacion = cadaResult.data.fecha_de_publicacion,
                    Url = cadaResult.data.foto_principal.url,
                    Descripcion = cadaResult.data.descripcion.First().text,
                    Precio = cadaResult.data.precio,
                    Patente = cadaResult.uid
                });
            }

            return resultados;
        }

    }
}
