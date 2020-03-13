import { reducerWithInitialState } from 'typescript-fsa-reducers';
import { LoadingScreenService } from '../../../Loading/LoadingScreenService';

const initialState: LoadingScreenService = new LoadingScreenService();

export const loadingnReducer = reducerWithInitialState(initialState);
