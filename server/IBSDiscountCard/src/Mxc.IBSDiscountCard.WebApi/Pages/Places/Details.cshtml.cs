using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Mxc.IBSDiscountCard.Application.Image.Commands;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using Mxc.IBSDiscountCard.Application.Place.Queries;

namespace Mxc.IBSDiscountCard.WebApi.Pages.Places
{
    public class Details : PageModelBase
    {
        private readonly IPlaceQueries _placeQueries;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)] public string PlaceId { get; set; }

        [BindProperty] public AdminPlaceViewModel Place { get; set; }

        public Details(IPlaceQueries placeQueries, IMediator mediator, IMapper mapper)
        {
            _placeQueries = placeQueries;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Place = await _placeQueries.GetAdminPlaceAsync(Guid.Parse(PlaceId));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var command = _mapper.Map<UpdatePlace>(Place);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ExecutionError.FunctionCode.ToString());
            }

            var image = Request.Form.Files.FirstOrDefault();
            if (image != null)
            {
                await _mediator.Send(new UploadPlaceImage(Guid.Parse(Place.Id), image));
            }

            return RedirectToPage("Details", new {PlaceId});
        }
    }
}