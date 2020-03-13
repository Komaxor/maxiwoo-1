import { AsyncStorage } from 'react-native';
import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import { createLogger } from 'redux-logger';
import { persistReducer, persistStore } from 'redux-persist';
import * as thunkMiddleware from 'redux-thunk';
import { LoadingScreenService } from '../../Loading/LoadingScreenService';
import { loadingnReducer } from './Loading/Reducers';
import { preferencesReducer } from './Preferences/Reducers';
import { IPreferencesStoreState } from './Preferences/Types';

export interface IAppState {
  loading: LoadingScreenService;
  preferences: IPreferencesStoreState;
}

const rootReducer = combineReducers<IAppState>({
  loading: loadingnReducer,
  preferences: preferencesReducer,
});

const persistConfig = {
  key: 'root',
  timeout: 10000, // for Android
  storage: AsyncStorage,
  debug: true,
  whitelist: ['login', 'preferences'],
};
const persistedReducer = persistReducer(persistConfig, rootReducer);

let composeEnhancers = compose;
let middlewares = [thunkMiddleware.default];

if (__DEV__) {
  const loggerMiddleware = createLogger();
  middlewares = [...middlewares, loggerMiddleware];

  // HACK: determine whether remote js debug enabled or not
  if (typeof atob !== 'undefined') {
    composeEnhancers = (window as any).__REDUX_DEVTOOLS_EXTENSION__ && (window as any).__REDUX_DEVTOOLS_EXTENSION__();
  }
}

export const store = createStore(
  persistedReducer,
  compose(
    applyMiddleware(...middlewares),
    composeEnhancers
  )
);

export const persistor = persistStore(store);
