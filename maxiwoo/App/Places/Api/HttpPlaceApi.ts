import { HttpHandler, IAjaxResponse } from '../../ApiHandler';
import { default as Env } from '../../Environment';
import { IPlaceHeader } from '../Models';
import { IGetPlaceByIdResponse } from './IGetPlaceByIdResponse';
import { IGetPlaceListRequest } from './IGetPlaceListRequest';
import { IGetPlaceListResponse } from './IGetPlaceListResponse';
import { IPlaceApi } from './IPlaceApi';

export class HttpPlaceApi implements IPlaceApi {
  public getPlaceAsync(id: string): Promise<IAjaxResponse<IGetPlaceByIdResponse>> {
    return HttpHandler.get<IGetPlaceByIdResponse>(Env.apiUrl, `v1/place/${id}`);
  }
  public getPlacesAsync(request: IGetPlaceListRequest): Promise<IAjaxResponse<IGetPlaceListResponse>> {
    return HttpHandler.get<IGetPlaceListResponse>(Env.apiUrl, `v1/place?searchText=${encodeURIComponent(request.searchText)}`);
  }
  public getPlacesByCategoryAsync(categoryId: string): Promise<IAjaxResponse<IPlaceHeader[]>> {
    return HttpHandler.get<IPlaceHeader[]>(Env.apiUrl, `v1/place/category/${encodeURIComponent(categoryId)}`);
  }
  public getFavouriteAsync(request: string[]): Promise<IAjaxResponse<IPlaceHeader[]>> {
    return HttpHandler.post<IPlaceHeader[]>(Env.apiUrl, `v1/place/favourite`, request);
  }
}
