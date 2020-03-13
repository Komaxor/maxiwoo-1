import I18n from 'i18n-js';
import { Alert } from 'react-native';
import { of } from 'rxjs';
import { ajax, AjaxResponse } from 'rxjs/ajax';
import { catchError, map } from 'rxjs/operators';
import { logger } from '../Logger';
import { store } from '../Redux/Store';
import { ajaxResponse, IAjaxResponse } from './AjaxResponse';

declare global {
  interface IFormDataValue {
    uri: string;
    name: string;
  }
  // tslint:disable-next-line:interface-name
  interface FormData {
    append(name: string, value: string | Blob | IFormDataValue, fileName?: string): void;
  }
}

class HttpApiHandler {
  private _getDefaultHeaders() {
    const state = store.getState();

    const baseHeaders = { 'Content-Type': 'application/json' };

    return baseHeaders;
  }
  public get<R>(baseUrl: string, slug: string, headers?: object): Promise<IAjaxResponse<R>> {
    const url = `${baseUrl}${slug}`;
    logger.log('GET REQUEST START: ' + url);
    return ajax({
      method: 'GET',
      url,
      crossDomain: true,
      withCredentials: false,
      headers: {
        ...this._getDefaultHeaders(),
        ...headers,
      },
    })
      .pipe(
        map((r: any) => ajaxResponse<R>(r)),
        catchError(this._errorHandling)
      )
      .toPromise() as Promise<IAjaxResponse<R>>;
  }

  public async post<R>(baseUrl: string, slug: string, body?: any, headers?: object): Promise<IAjaxResponse<R>> {
    const url = `${baseUrl}${slug}`;
    logger.log('POST REQUEST START: ' + url);
    logger.log('BODY: ');
    logger.log(body);
    return ajax({
      method: 'POST',
      url,
      withCredentials: false,
      headers: {
        ...this._getDefaultHeaders(),
        ...headers,
      },
      body: JSON.stringify(body),
    })
      .pipe(
        map((r: any) => ajaxResponse<R>(r)),
        catchError(this._errorHandling)
      )
      .toPromise() as Promise<IAjaxResponse<R>>;
  }

  public async postImage<R>(baseUrl: string, slug: string, fileUri: string, headers?: object): Promise<IAjaxResponse<R>> {
    return new Promise<IAjaxResponse<R>>((resolve, reject) => {
      const url = `${baseUrl}${slug}`;

      const formData = new FormData();

      const fileName = fileUri.split('/').pop();

      const f = { uri: fileUri, name: fileName, type: `image/${fileName.split('.').pop()}` };

      formData.append('File', f);

      logger.log('POST REQUEST START: ' + url);
      logger.log('BODY: ');
      logger.log(formData);

      const xhr = new XMLHttpRequest();

      xhr.onreadystatechange = e => {
        if (xhr.readyState !== 4) {
          return;
        }

        if (xhr.status !== 200) {
          logger.warn(e);
        }

        resolve(ajaxResponse<R>(new AjaxResponse(e, xhr, xhr)));
      };
      xhr.open('POST', url);
      xhr.responseType = 'json';
      xhr.setRequestHeader('content-type', 'multipart/form-data');
      xhr.send(formData);
    });
  }

  public put<R>(baseUrl: string, slug: string, body?: any, headers?: object): Promise<IAjaxResponse<R>> {
    const url = `${baseUrl}${slug}`;
    logger.log('PUT REQUEST START: ' + url);
    logger.log('BODY: ');
    logger.log(body);
    return ajax({
      method: 'PUT',
      url,
      withCredentials: true,
      crossDomain: true,
      headers: {
        ...this._getDefaultHeaders(),
        ...headers,
      },
      body,
    })
      .pipe(
        map((r: any) => ajaxResponse<R>(r)),
        catchError(this._errorHandling)
      )
      .toPromise() as Promise<IAjaxResponse<R>>;
  }

  public delete<R>(baseUrl: string, slug: string, headers?: object) {
    const url = `${baseUrl}${slug}`;
    logger.log('DELETE REQUEST START: ' + url);
    return ajax({
      method: 'DELETE',
      url,
      withCredentials: true,
      headers: {
        ...this._getDefaultHeaders(),
        ...headers,
      },
    }).pipe(
      map((r: any) => ajaxResponse<R>(r)),
      catchError(this._errorHandling)
    );
  }

  private _errorHandling = error => {
    logger.info('Handling error from server call', JSON.stringify(error));

    const ajaxResp = ajaxResponse(error.xhr);

    if(ajaxResp.isSuccess === false && ajaxResp.status <= 500){
      Alert.alert(I18n.t('server_error'), I18n.t('server_error_message'))
    }

    return of(ajaxResp);
  };
}

export const HttpHandler = new HttpApiHandler();
