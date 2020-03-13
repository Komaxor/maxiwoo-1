import { HttpHandler, IAjaxResponse } from '../../ApiHandler';
import Environment from '../../Environment';
import { IPlaceHeader } from '../../Places';
import { IGetFavouriteApi } from './IGetFavouriteApi';

export class HttpGetFavouriteApi implements IGetFavouriteApi {
  public getFavouriteAsync(request: string[]): Promise<IAjaxResponse<IPlaceHeader[]>> {
    return HttpHandler.get<IPlaceHeader[]>(Environment.apiUrl, 'v1/user', request);
  }
}
