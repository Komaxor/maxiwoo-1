using AutoMapper;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Institute;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place.Models;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;
using System;
using Mxc.IBSDiscountCard.Application.User.Responses;

namespace Mxc.IBSDiscountCard.Infrastructure
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile()
        {
            CreateMap<PlaceDb, Place>();
            CreateMap<Guid, PlaceId>().ConstructUsing(s => new PlaceId(s));
            CreateMap<Guid, InstituteId>().ConstructUsing(s => new InstituteId(s));
            CreateMap<Guid, UserId>().ConstructUsing(s => new UserId(s));
            CreateMap<Guid, SubscriptionId>().ConstructUsing(s => new SubscriptionId(s));

            CreateMap<Place, PlaceDb>()
                .ForMember(dst => dst.Id, o => o.MapFrom(src => src.Id.Id));

            CreateMap<Address, AddressDb>().ReverseMap();
            CreateMap<OpeningHoursOfDay, OpeningHoursOfDayDb>().ReverseMap();

            CreateMap<User, UserDb>()
                .ForMember(dst => dst.Id, o => o.MapFrom(src => src.Id.Id))
                .ForMember(dst => dst.InstitudeId, o => o.MapFrom(src => src.InstituteId.Id))
                .ForMember(dst => dst.UserName, o => o.MapFrom(src => src.Email));

            CreateMap<Subscription, SubscriptionDb>()
                .ForMember(dst => dst.Id, o => o.MapFrom(src => src.Id.Id))
                .ReverseMap();

            CreateMap<InstituteDb, Institute>();
            CreateMap<Braintree.Subscription, SubscriptionResponse>()
                .ForMember(s => s.TrialDurationUnit, cfg => cfg.MapFrom(s => s.TrialDurationUnit.ToString()));
        }
    }
}