import { IPlaceHeader } from '.';

export interface IPlaceDetails extends IPlaceHeader {
  name: string;
  type: string;
  about: string;
  phoneNumber: string;
  email: string;
  web: string;
  facebook: string;
  instagram: string;
  previewImage: string;
  address: IAddressViewModel;
  discount: IDiscountViewModel;
  tags: string[];
  openingHours: IOpeningHoursViewModel;
}
export interface IAddressViewModel {
  displayAddress: string;
  latitude: string;
  longitude: string;
}

export interface IDiscountViewModel {
  institudeId: string;
  premiumDiscountDescription: { [lang: string]: string };
  basicDiscountDescription: { [lang: string]: string };
  maxPremiumDiscount?: number;
  maxBasicDiscount?: number;
}

export interface IOpeningHoursViewModel {
  monday: string;
  tuesday: string;
  wednesday: string;
  thursday: string;
  friday: string;
  saturday: string;
  sunday: string;
}
