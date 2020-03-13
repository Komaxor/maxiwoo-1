import { Guid } from 'guid-typescript';
import { IDisposable } from '../Helpers';
import { LoadingScreenService } from './LoadingScreenService';

export class Loading implements IDisposable {
  public id: string = Guid.create().toString();

  private _loadingScreenService: LoadingScreenService;
  public label: string = '';

  constructor(loadingScreenService: LoadingScreenService, label: string = '') {
    this._loadingScreenService = loadingScreenService;
    this.label = label;
  }

  public dispose(): void {
    this._loadingScreenService.hide(this);
  }
}
