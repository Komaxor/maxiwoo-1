export interface IGetPlaceByIdResponse {
  id: string;
  name: string;
  type: string;
  previewImage: string;
  phoneNumber: string;
  email: string;
  web: string;
  facebook: string;
  instagram: string;
  about: string;
  address: {
    displayAddress: string;
    latitude: number;
    longitude: number;
  };
  tags: string[];
  openingHours: {
    monday: string;
    tuesday: string;
    wednesday: string;
    thursday: string;
    friday: string;
    saturday: string;
    sunday: string;
  };
}
