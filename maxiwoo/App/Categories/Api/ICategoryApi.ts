import { IAjaxResponse } from '../../ApiHandler';
import { ICategory } from '../Models';
import { IGetCategoryByIdResponse } from './IGetCategoryByIdResponse';
import { IGetCategoryListRequest } from './IGetCategoryListRequest';
import { IGetCategoryListResponse } from './IGetCategoryListResponse';

export interface ICategoryApi {
  // getCategoriesAsync(request: IGetCategoryListRequest): Promise<IAjaxResponse<IGetCategoryListResponse>>;
  getCategoriesAsync(): Promise<IAjaxResponse<ICategory[]>>;
  getCategoryAsync(id: string): Promise<IAjaxResponse<IGetCategoryByIdResponse>>;
}
