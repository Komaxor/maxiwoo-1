import { IAjaxResponse } from '../../ApiHandler';
import { IPlaceHeader } from '../../Places';

export interface IGetFavouriteApi {
  getFavouriteAsync(request: string[]): Promise<IAjaxResponse<IPlaceHeader[]>>;
}
