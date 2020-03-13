using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using Mxc.IBSDiscountCard.Application.Place.Queries;
using Mxc.IBSDiscountCard.Application.User.Queries;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;

namespace Mxc.IBSDiscountCard.Application
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Domain.PlaceAggregate.Place, PlaceHeaderViewModel>();
            
            CreateMap<Domain.PlaceAggregate.Place, AdminPlaceViewModel>()
                .ForMember(d => d.Tags, cfg =>
                {
                    cfg.MapFrom(place => place.Tags);
                    cfg.ConvertUsing(new TagToStringConverter());
                });

            CreateMap<AdminPlaceViewModel, UpdatePlace>()
                .ForMember(place => place.Tags, cfg =>
                {
                    cfg.MapFrom(vm => vm.Tags);
                    cfg.ConvertUsing(new StringToTagConverter());
                })
                .ForMember(place => place.CategoryId, cfg =>
                {
                    cfg.MapFrom(vm => (int)vm.CategoryId);
                });

            CreateMap<AdminPlaceViewModel, AddPlace>()
                .ForMember(place => place.Tags, cfg =>
                {
                    cfg.MapFrom(vm => vm.Tags);
                    cfg.ConvertUsing(new StringToTagConverter());
                });

            CreateMap<AddressViewModel, AddressDto>();
            CreateMap<OpeningHoursViewModel, OpeningHoursOfDayDto>();

            CreateMap<Domain.PlaceAggregate.Place, PlaceDetailsViewModel>();

            CreateMap<Address, AddressViewModel>();
            CreateMap<OpeningHoursOfDay, OpeningHoursViewModel>();

            CreateMap<Domain.UserAggregate.User, UserDetailsViewModel>();
            CreateMap<Domain.UserAggregate.Subscription, SubscriptionViewModel>();
        }
        
        public class TagToStringConverter : IValueConverter<IEnumerable<string>, string>
        {
            public string Convert(IEnumerable<string> sourceMember, ResolutionContext context)
            {
                return string.Join(", ", sourceMember);
            }
        }
        
        public class StringToTagConverter : IValueConverter<string, List<string>>
        {
            public List<string> Convert(string sourceMember, ResolutionContext context)
            {
                return sourceMember?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
            }
        }
    }
}