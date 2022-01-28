using CookieIdentity.AppCode;
using CookieIdentity.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieIdentity.Pages
{
    [Authorize(Policy = "HRManager")]
    public class HRManagermentModel : PageEndPointModel
    {
        [BindProperty]
        public IList<WeatherForecastDto> WeatherForecastDtos { get; set; }
        public HRManagermentModel(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this.WeatherForecastDtos = new List<WeatherForecastDto>();
        }
        public async Task OnGet()
        {
            WeatherForecastDtos = await InvokeGetEndPointAsync<List<WeatherForecastDto>>("WebAPI", "WeatherForecast");
        }
    }
}
