import { of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { IAjaxResponse } from '../../ApiHandler';
import { IPlaceHeader } from '../../Places';
import { IGetFavouriteApi } from './IGetFavouriteApi';

export class MockedGetFavouriteApi implements IGetFavouriteApi {
  public async getFavouriteAsync(request: string[]): Promise<IAjaxResponse<IPlaceHeader[]>> {
    await of('')
      .pipe(delay(500))
      .toPromise();

    

    return {
      isSuccess: true,
      status: 200,
      responseType: '',
      response: this._places,
    };
  }
  private _places = [
    {
      id: '1',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '2',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '13',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '14',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '15',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '16',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '17',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '18',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '19',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '20',
      name: 'Görög kancsó',
      type: 'Restaurant',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
  ];
}
