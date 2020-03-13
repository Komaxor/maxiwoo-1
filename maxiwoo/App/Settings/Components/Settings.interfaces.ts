import { NavigationScreenProps } from 'react-navigation';
import { mapDispatchToProps, mapStateToProps } from './Settings.container';

export interface ISettingsProps extends NavigationScreenProps {}

export type ISettingsStore = ReturnType<typeof mapStateToProps>;
export type ISettingsDispatch = ReturnType<typeof mapDispatchToProps>;

export interface ISettingsState {}

export interface ISettingsItem {
  data: Array<{
    type: 'simple' | 'toggle';
    title: string;
    disabled: boolean;
    icon: { name: string; type: string };
    onPress: (v: null | any) => any;
    visibleOnIOS: boolean;
    visibleOnAndroid: boolean;
  }>;
}

export type Props = ISettingsProps & ISettingsStore & ISettingsDispatch;
export type State = ISettingsState;
