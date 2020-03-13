import { ThunkDispatch } from 'redux-thunk';
import { Action } from 'typescript-fsa';
import { IAppState } from '../../../Redux/Store';
import { ILoadingAwareState } from '../../../Redux/Store/Loading';
import { IPreferencesStoreState } from '../../../Redux/Store/Preferences';
import * as preferenceActions from '../../../Redux/Store/Preferences/Actions';

export const mapStateToProps: (state: IAppState) => ILoadingAwareState & IPreferencesStoreState = (state: IAppState) => ({
  loading: state.loading,
  ...state.preferences,
});

export const mapDispatchToProps = (dispatch: ThunkDispatch<undefined, undefined, Action<{}>>) => ({
  setLanguage: (langCode: string) => dispatch(preferenceActions.languageChangedAction({ langCode })),
});
