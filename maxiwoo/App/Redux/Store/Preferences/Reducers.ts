import { reducerWithInitialState } from 'typescript-fsa-reducers';
import { languageChangedAction } from './Actions';
import { IPreferencesStoreState } from './Types';

const initialState: IPreferencesStoreState = {
  selectedLanguage: '',
  // isLanguageChanged: false,
};

export const preferencesReducer = reducerWithInitialState(initialState)
  .case(languageChangedAction, (state, payload) => ({
    ...state,
    selectedLanguage: payload.langCode,
  }));
