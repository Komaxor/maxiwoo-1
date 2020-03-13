import { IAppState } from '../../Redux/Store';

export function mapStateToProps(state: IAppState) {
  return {
    loading: state.loading,
  };
}
