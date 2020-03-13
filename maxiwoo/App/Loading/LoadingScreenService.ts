import { Loading } from './Loading';

export class LoadingScreenService {
  private _listeners: Array<(ischanged: boolean) => void> = [];

  private _activeLoadings: Loading[] = [];

  public get isLoading(): boolean {
    return this._activeLoadings.length > 0;
  }

  public addLoadingChangeListener(listener?: (ischanged: boolean) => void) {
    if (listener !== undefined) {
      this._listeners.push(listener);
    }
  }

  public get loadingLabel(): string {
    return this._activeLoadings.map(l => l.label).join(', ');
  }

  private _notifyListeners() {
    this._listeners.forEach(listener => {
      listener(this.isLoading);
    });
  }

  public show(label: string = ''): Loading {
    const loading = new Loading(this, label);

    this._activeLoadings.push(loading);

    this._notifyListeners();

    return loading;
  }

  public hide(loading: Loading) {
    if (this._activeLoadings.length === 1) {
      this._activeLoadings = [];
    } else if (this._activeLoadings.length > 1) {
      const index = this._activeLoadings.indexOf(loading);
      this._activeLoadings.splice(index, 1);
    }

    this._notifyListeners();
  }
}
