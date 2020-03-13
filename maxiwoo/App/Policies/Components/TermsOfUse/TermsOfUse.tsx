import I18n from 'i18n-js';
import React from 'react';
import { SafeAreaView, StyleSheet, View } from 'react-native';
import { WebView } from 'react-native-webview';
import { connect } from 'react-redux';
import Environment from '../../../Environment';
import { Colors, Views } from '../../../Styles';
import { mapStateToProps } from './TermsOfUse.container';
import { Props, State } from './TermsOfUse.interface';

export interface ITermsOfUseProps {}

class TermsOfUse extends React.Component<Props, State> {
  public static navigationOptions = () => ({
    title: I18n.t('terms_of_use_title'),
  });

  public static policiesHeaderStyle = () => ({
    title: I18n.t('policies_title'),
    headerStyle: {
      backgroundColor: Colors.headerBackground,
    },
    headerTintColor: Colors.headerText,
  });

  private _styles = StyleSheet.create({
    container: { ...Views.base },
  });

  public render() {
    return (
      <SafeAreaView style={{ flex: 1 }}>
        <View style={this._styles.container}>
          <WebView bounces={false} scrollEnabled={true} source={{ uri: Environment.termsOfViewsUrl }} />
        </View>
      </SafeAreaView>
    );
  }
}

export default connect(mapStateToProps)(TermsOfUse);
