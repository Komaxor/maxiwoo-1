import actionCreatorFactory from 'typescript-fsa';

const LANGUAGECHANGED = 'LANGUAGECHANGED';

const preferencesActionCreator = actionCreatorFactory('@@preferences');
export const languageChangedAction = preferencesActionCreator<{ langCode: string }>(LANGUAGECHANGED);
