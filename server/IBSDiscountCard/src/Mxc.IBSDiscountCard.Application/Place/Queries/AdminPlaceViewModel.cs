using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mxc.IBSDiscountCard.Application.Place.Queries
{
    public class AdminPlaceViewModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Type { get; set; }

        public Category CategoryId { get; set; }

        [Display(Name = "Phone Number (Start with +, then 10-14 digits)")]
        [RegularExpression(@"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|
2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|
4[987654310]|3[9643210]|2[70]|7|1)\d{1,12}$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Not a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Site")]
        [DataType(DataType.Url)]
        [RegularExpression(@"^(http|https|ftp|)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&%\$#\=~])*[^\.\,\)\(\s]$")]
        public string Web { get; set; }

        [Display(Name = "Facebook")]
        [DataType(DataType.Url)]
        [RegularExpression(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", ErrorMessage = "Not a valid Facebook url")]
        public string Facebook { get; set; }

        [Display(Name = "Instagram")]
        [DataType(DataType.Url)]
        [RegularExpression(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", ErrorMessage = "Not a valid Instagram url")]
        public string Instagram { get; set; }

        public string PreviewImage { get; set; }

        public string About { get; set; }

        public AddressViewModel Address { get; set; }

        public string Tags { get; set; }

        public OpeningHoursViewModel OpeningHours { get; set; }

        public bool IsHidden { get; set; }
    }

    public enum Category: int
    {
        Photographer,
        Videographer,
        Venue,

        [Display(Name = "Wedding Sweets and Confectionery")]
        Wedding_Sweets_and_Confectionery,

        [Display(Name = "Wedding Designer")]
        wedding_designer,

        [Display(Name = "Master / of Ceremony")]
        master_of_ceremony,

        Transporation,

        [Display(Name = "Wedding Band/Dj")]
        wedding_band_dj,

        [Display(Name = "Make-up")]
        make_up,

        Hairdresser,

        [Display(Name = "Wedding Dress")]
        wedding_dress,

        [Display(Name = "Wedding Ceremony Leader")]
        wedding_ceremony_leader,

        Honeymoon,

        [Display(Name = "Kids\' Corner")]
        kids_corner,

        [Display(Name = "Guest Gift")]
        guest_gift,

        [Display(Name = "Wedding Invitation")]
        wedding_Invitation,

        Florist,

        [Display(Name = "Dance Lessons")]
        dance_lessons,

        Extras
    }
}