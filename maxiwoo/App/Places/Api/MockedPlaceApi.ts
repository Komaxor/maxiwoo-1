import { of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { IAjaxResponse } from '../../ApiHandler';
import { IPlaceHeader } from '../Models/IPlaceHeader';
import { IGetPlaceByIdResponse } from './IGetPlaceByIdResponse';
import { IGetPlaceListRequest } from './IGetPlaceListRequest';
import { IGetPlaceListResponse } from './IGetPlaceListResponse';
import { IPlaceApi } from './IPlaceApi';

export class MockedPlaceApi implements IPlaceApi {
  public async getPlacesByCategoryAsync(categoryId: string): Promise<IAjaxResponse<IPlaceHeader[]>> {
    await of('')
      .pipe(delay(500))
      .toPromise();
    return this._places
  }
  private _places = [
    {
      id: '1',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '2',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '13',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '14',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '15',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '16',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '17',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '18',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '19',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
    {
      id: '20',
      name: 'Görög kancsó',
      type: 'Restaurant',
      maxPremiumDiscount: 20,
      maxBasicDiscount: 10,
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
      about: 'string',
      address: {
        displayAddress: 'Szentendre, Görög u. 6, 2000',
        latitude: 47.668055,
        longitude: 19.077452,
      },
      discount: {
        institudeId: 'string',
        premiumDiscountDescription: { ['en']: 'string' },
        basicDiscountDescription: { ['en']: 'string' },
        maxPremiumDiscount: 20,
        maxBasicDiscount: 10,
      },
      tags: ['ad'],
      openingHours: {
        monday: '7-17',
        tuesday: '7-17',
        wednesday: '7-17',
        thursday: '7-17',
        friday: '7-17',
        saturday: '7-17',
        sunday: '7-17',
      },
    },
  ];

  public async getPlacesAsync(request: IGetPlaceListRequest): Promise<IAjaxResponse<IGetPlaceListResponse>> {
    await of('')
      .pipe(delay(500))
      .toPromise();

    let list = this._places;

    if (request.searchText) {
      list = list.filter(p => p.name.toLowerCase().indexOf(request.searchText.toLowerCase()));
    }

    const res = {
      results: list,
      resultsLength: 10,
    };

    return {
      isSuccess: true,
      status: 200,
      responseType: '',
      response: res,
    };
  }

  public async getPlaceAsync(id: string): Promise<IAjaxResponse<IGetPlaceByIdResponse>> {
    await of('')
      .pipe(delay(500))
      .toPromise();

    const res = this._places.find(p => p.id === id);

    return {
      isSuccess: true,
      status: 200,
      responseType: '',
      response: res,
    };
  }
}
