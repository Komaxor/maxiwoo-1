import { FontAwesome } from '@expo/vector-icons';
import I18n from 'i18n-js';
import React from 'react';
import { ActivityIndicator, Platform, StatusBar, Text, View } from 'react-native';
import { NavigationActions, NavigationContainerComponent } from 'react-navigation';
import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import { IApplicationProps, IApplicationState } from './Application.interfaces';
import { localeSet } from './Locales/Locale';
import { AppContainer } from './Navigation';
import { persistor, store } from './Redux/Store';

export default class Application extends React.Component<IApplicationProps, IApplicationState> {
  public state = {
    isLoading: false,
    loadingLabel: '',
  };

  public async componentWillMount() {
    if (Platform.OS === 'ios') {
      StatusBar.setBarStyle('light-content');
    }
    localeSet();
    store.getState().loading.addLoadingChangeListener(this._handleLoadingChanged);
  }

  

  public componentDidMount() {
    store.subscribe(() => {
      if (!store.getState().preferences.selectedLanguage) {
        this._navigationref.dispatch(
          NavigationActions.navigate({
            routeName: 'App',
          })
        );
      }
      this._navigationref.dispatch(
        NavigationActions.navigate({
          routeName: 'App',
        })
      );
    });
  }

  private _navigationref: NavigationContainerComponent;

  public render() {
    let loadingSpinner = null;
    if (this.state.isLoading) {
      loadingSpinner = (
        <View
          style={{
            width: '100%',
            height: '100%',
            alignItems: 'center',
            justifyContent: 'center',
            backgroundColor: 'rgba(0, 0, 0, 0.8)',
          }}
        >
          <ActivityIndicator size="large" color="#fff" />
          <Text style={ { color: '#fff' } }>{this.state.loadingLabel}</Text>
        </View>
      );
    }

    return (
      <Provider store={store}>
        <PersistGate loading={null} persistor={persistor}>
          <AppContainer ref={ref => (this._navigationref = ref)} />
        </PersistGate>
        {loadingSpinner}
      </Provider>
    );
  }

  private _handleLoadingChanged = (_: boolean) => {
    const loadingScreenService = store.getState().loading;
    this.setState({ isLoading: loadingScreenService.isLoading, loadingLabel: loadingScreenService.loadingLabel });
  };
}
