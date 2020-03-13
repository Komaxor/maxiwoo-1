import { NavigationScreenProps } from 'react-navigation';
import { mapStateToProps } from './PrivacyPolicy.container';

export interface IPrivacyPolicyProps extends NavigationScreenProps {}

export type IPrivacyPolicyStore = ReturnType<typeof mapStateToProps>;

export interface IPrivacyPolicyState {}

export type Props = IPrivacyPolicyProps & IPrivacyPolicyStore;
export type State = IPrivacyPolicyState;
