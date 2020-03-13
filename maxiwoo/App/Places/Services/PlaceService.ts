import { ApiResponse, toError } from '../../ApiHandler';
import Environment from '../../Environment';
import { HttpPlaceApi } from '../Api/HttpPlaceApi';
import { IPlaceApi } from '../Api/IPlaceApi';
import { MockedPlaceApi } from '../Api/MockedPlaceApi';
import { IPlaceDetails, IPlaceHeader } from '../Models';

export class PlaceService {
  private _createApi(methodName: string): IPlaceApi {
    if (Environment.useHttpApi(PlaceService.name, methodName)) {
      return new HttpPlaceApi();
    }
    return new MockedPlaceApi();
  }

  public async getPlacesAsync(searchKey?: string): Promise<ApiResponse<IPlaceHeader[]>> {
    const result = await this._createApi(this.getPlacesAsync.name).getPlacesAsync({
      searchText: searchKey,
    });

    if (result.isSuccess === true) {
      return {
        isSuccess: true,
        content: result.response.results,
      };
    } else {
      return toError(result);
    }
  }

  public async getPlacesByCategoryAsync(categoryId: string): Promise<ApiResponse<IPlaceHeader[]>> {
    const result = await this._createApi(this.getPlacesByCategoryAsync.name).getPlacesByCategoryAsync(categoryId);

    if (result.isSuccess === true) {
      return {
        isSuccess: true,
        content: result.response,
      };
    } else {
      return toError(result);
    }
  }

  public async getPlaceAsync(id: string): Promise<ApiResponse<IPlaceDetails>> {
    const result = await this._createApi(this.getPlaceAsync.name).getPlaceAsync(id);

    if (result.isSuccess === true) {
      return {
        isSuccess: true,
        content: result.response,
      };
    } else {
      return toError(result);
    }
  }

  public async getFavouriteAsync(ids: string[]): Promise<ApiResponse<IPlaceHeader[]>> {
    const result = await this._createApi(this.getFavouriteAsync.name).getFavouriteAsync(ids);

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
