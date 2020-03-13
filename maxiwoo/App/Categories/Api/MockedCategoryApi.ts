import I18n from 'i18n-js';
import { of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { IAjaxResponse } from '../../ApiHandler';
import { ICategory } from '../Models';
import { ICategoryApi } from './ICategoryApi';
import { IGetCategoryByIdResponse } from './IGetCategoryByIdResponse';

export class MockedCategoryApi implements ICategoryApi {
  private _categories = [
    {
      id: '1',
      title: I18n.t('photographer'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '2',
      title: I18n.t('videographer'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '3',
      title: I18n.t('venue'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '4',
      title: I18n.t('wedding_sweets_and_confectionery'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '5',
      title: I18n.t('wedding_designer'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '6',
      title: I18n.t('master_of_ceremony'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '7',
      title: I18n.t('transporation'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '8',
      title: I18n.t('wedding_band_dj'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
    {
      id: '9',
      title: I18n.t('make_up'),
      lang_Key: 'photographer',
      previewImage: 'https://www.ahstatic.com/photos/5555_rsr001_01_p_1024x768.jpg',
    },
  ];

  public async getCategoriesAsync(): Promise<IAjaxResponse<ICategory[]>> {
    await of('')
      .pipe(delay(500))
      .toPromise();

    const list = this._categories;

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

  public async getCategoryAsync(id: string): Promise<IAjaxResponse<IGetCategoryByIdResponse>> {
    await of('')
      .pipe(delay(500))
      .toPromise();

    const res = this._categories.find(p => p.id === id);

    return {
      isSuccess: true,
      status: 200,
      responseType: '',
      response: res,
    };
  }
}
