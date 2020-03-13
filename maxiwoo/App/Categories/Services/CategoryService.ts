import { ApiResponse, toError } from '../../ApiHandler';
import Environment from '../../Environment';
import { logger } from '../../Logger';
import { HttpCategoryApi } from '../Api/HttpCategoryApi';
import { ICategoryApi } from '../Api/ICategoryApi';
import { MockedCategoryApi } from '../Api/MockedCategoryApi';
import { ICategory } from '../Models';

export class CategoryService {
  private _createApi(methodName: string): ICategoryApi {
    if (Environment.useHttpApi(CategoryService.name, methodName)) {
      return new HttpCategoryApi();
    }

    return new MockedCategoryApi();
  }

  public async getCategoriesAsync(): Promise<ApiResponse<ICategory[]>> {
    const result = await this._createApi(this.getCategoriesAsync.name).getCategoriesAsync();

    if (result.isSuccess === true) {
      return {
        isSuccess: true,
        content: result.response,
      };
    } else {
      return toError(result);
    }
  }

  public async getCategoryAsync(id: string): Promise<ApiResponse<ICategory>> {
    const result = await this._createApi(this.getCategoryAsync.name).getCategoryAsync(id);

    if (result.isSuccess === true) {
      return {
        isSuccess: true,
        content: result.response,
      };
    } else {
      return toError(result);
    }
  }
}
