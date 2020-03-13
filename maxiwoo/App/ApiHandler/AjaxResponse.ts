import { AjaxResponse as RxjsAjaxResponse } from 'rxjs/ajax';
import { logger } from '../Logger';
import { ApiError } from './ApiError';
import { ApiResponse } from './ApiResponse';

export type IAjaxResponse<T> =
  | {
      isSuccess: true;
      response: T;
      status: number;
      responseType: string;
    }
  | {
      isSuccess: false;
      response: ApiError;
      status: number;
      responseType: string;
    };

export const ajaxResponse = <T>(ajaxResp: RxjsAjaxResponse): IAjaxResponse<T> => {
  logger.log('STATUS: ' + ajaxResp.status);
  logger.log(JSON.stringify(ajaxResp.response));

  return {
    isSuccess: ajaxResp.status === 200,
    response: ajaxResp.response,
    status: ajaxResp.status,
    responseType: ajaxResp.responseType,
  };
};

export const toError = <TModel>(resp: { isSuccess: false; response: ApiError; status: number; responseType: string }): ApiResponse<TModel> => {
  logger.debug('toerror');
  if (resp.isSuccess === false) {
    return { isSuccess: false, content: { statusCode: resp.status, ...resp.response } };
  }

  throw new Error('Can not call toError is resp is success');
};
