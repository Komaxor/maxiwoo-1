using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mxc.IBSDiscountCard.Application.Image.Commands;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using Mxc.IBSDiscountCard.Application.Place.Queries;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;

namespace Mxc.IBSDiscountCard.WebApi.Pages.Places
{
    public class NewPlace : PageModelBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPlaceQueries _queries;
        private readonly ILoggedInUserAccessor _userAccessor;

        [BindProperty] public AdminPlaceViewModel Place { get; set; }

        public NewPlace(IMapper mapper, IMediator mediator, IPlaceQueries queries, ILoggedInUserAccessor userAccessor)
        {
            _mapper = mapper;
            _mediator = mediator;
            _queries = queries;
            _userAccessor = userAccessor;
        }

        public IActionResult OnGet()
        {
            Place = new AdminPlaceViewModel
            {
                Address = new AddressViewModel(),
                OpeningHours = new OpeningHoursViewModel(),
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var command = _mapper.Map<AddPlace>(Place);

            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ExecutionError.FunctionCode.ToString());
                return Page();
            }

            var image = Request.Form.Files.FirstOrDefault();
            if (image != null)
            {
                await _mediator.Send(new UploadPlaceImage(result.Result.PlaceId, image));
            }

            return RedirectToPage("Details", new {result.Result.PlaceId});
        }
    }
}