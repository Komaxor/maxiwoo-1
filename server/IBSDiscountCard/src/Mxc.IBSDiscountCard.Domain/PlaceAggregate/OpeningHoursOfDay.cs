using Mxc.Domain.Abstractions.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate
{
    /// <summary>
    /// Opening hours data
    /// </summary>
    public class OpeningHoursOfDay : ValueObject
    {
        public string Monday { get; }
        public string Tuesday { get; }
        public string Wednesday { get; }
        public string Thursday { get; }
        public string Friday { get; }
        public string Saturday { get; }
        public string Sunday { get; }

        public OpeningHoursOfDay(string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, string sunday)
        {
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
            Saturday = saturday;
            Sunday = sunday;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Monday;
            yield return Tuesday;
            yield return Wednesday;
            yield return Thursday;
            yield return Friday;
            yield return Saturday;
            yield return Sunday;
        }
    }
}
