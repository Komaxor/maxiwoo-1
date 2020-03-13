import { NavigationScreenProps } from 'react-navigation';
import { mapDispatchToProps, mapStateToProps } from './SelectLanguage.container';

export interface ISelectLanguageProps extends NavigationScreenProps {}

export type ISelectLanguageStore = ReturnType<typeof mapStateToProps>;
export type ISelectLanguageDispatch = ReturnType<typeof mapDispatchToProps>;

export type Props = ISelectLanguageProps & ISelectLanguageStore & ISelectLanguageDispatch;
// tslint:disable-next-line: interface-name
export interface State {}
