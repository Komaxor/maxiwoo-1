import { IAjaxResponse } from '../../ApiHandler';
import { IPlaceHeader } from '../Models/IPlaceHeader';
import { IGetPlaceByIdResponse } from './IGetPlaceByIdResponse';
import { IGetPlaceListRequest } from './IGetPlaceListRequest';
import { IGetPlaceListResponse } from './IGetPlaceListResponse';

export interface IPlaceApi {
  getPlacesAsync(request: IGetPlaceListRequest): Promise<IAjaxResponse<IGetPlaceListResponse>>;
  getPlaceAsync(id: string): Promise<IAjaxResponse<IGetPlaceByIdResponse>>;
  getPlacesByCategoryAsync(categoryId: string): Promise<IAjaxResponse<IPlaceHeader[]>>;
  getFavouriteAsync(request: string[]): Promise<IAjaxResponse<IPlaceHeader[]>>;
}
