import { NavigationScreenProps } from 'react-navigation';
import { mapStateToProps } from './TermsOfUse.container';

export interface ITermsOfUseProps extends NavigationScreenProps {}

export type ITermsOfUseStore = ReturnType<typeof mapStateToProps>;

export interface ITermsOfUseState {}

export type Props = ITermsOfUseProps & ITermsOfUseStore;
export type State = ITermsOfUseState;
