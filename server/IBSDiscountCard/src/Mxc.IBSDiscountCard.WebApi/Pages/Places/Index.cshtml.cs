using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.IBSDiscountCard.Application.Image.Queries;
using Mxc.IBSDiscountCard.Application.Place.Queries;

namespace Mxc.IBSDiscountCard.WebApi.Pages.Places
{
    public class Index : PageModelBase
    {
        private readonly IPlaceQueries _placeQueries;
        private readonly IImageQueries _imageQueries;

        public List<AdminPlaceViewModel> PlaceHeaders { get; set; }
        
        public Index(IPlaceQueries placeQueries, IImageQueries imageQueries)
        {
            _placeQueries = placeQueries;
            _imageQueries = imageQueries;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            PlaceHeaders = await _placeQueries.GetAdminPlacesAsync();
            return Page();
        }
        
        public async Task<IActionResult> OnGetImageAsync(string imageName)
        {
            var fileSource = await _imageQueries.GetImageAsync(imageName);

            return File(fileSource.Content, fileSource.ContentType);
        }
    }
}