import { HttpHandler, IAjaxResponse } from '../../ApiHandler';
import { default as Env } from '../../Environment';
import { ICategory } from '../Models';
import { ICategoryApi } from './ICategoryApi';
import { IGetCategoryByIdResponse } from './IGetCategoryByIdResponse';

export class HttpCategoryApi implements ICategoryApi {
  public getCategoryAsync(id: string): Promise<IAjaxResponse<IGetCategoryByIdResponse>> {
    return HttpHandler.get<IGetCategoryByIdResponse>(Env.apiUrl, `v1/place/${id}`);
  }
  // public getCategoriesAsync(request: IGetCategoryListRequest): Promise<IAjaxResponse<IGetCategoryListResponse>> {
  //   return HttpHandler.get<IGetCategoryListResponse>(Env.apiUrl, `v1/place?searchText=${encodeURIComponent(request.searchText)}`);
  // }
  public getCategoriesAsync(): Promise<IAjaxResponse<ICategory[]>> {
    return HttpHandler.get<ICategory[]>(Env.apiUrl, `v1/category`);
  }
}
