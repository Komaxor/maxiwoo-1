import { IAppState } from '../../../Redux/Store';
import { ILoadingAwareState } from '../../../Redux/Store/Loading';

export const mapStateToProps: (state: IAppState) => ILoadingAwareState = (state: IAppState) => ({
  loading: state.loading,
});
