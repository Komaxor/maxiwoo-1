import I18n from 'i18n-js';
import React from 'react';
import { SafeAreaView, StyleSheet, View } from 'react-native';
import { WebView } from 'react-native-webview';
import { connect } from 'react-redux';
import Environment from '../../../Environment';
import {  Views } from '../../../Styles';
import { mapStateToProps } from './PrivacyPolicy.container';
import { Props, State } from './PrivacyPolicy.interface';

class PrivacyPolicy extends React.Component<Props, State> {
  public static navigationOptions = () => ({
    title: I18n.t('privacy_policy_title'),
  });

  private _styles = StyleSheet.create({
    container: { ...Views.base },
  });
  public render() {
    return (
      <SafeAreaView style={{ flex: 1 }}>
        <View style={this._styles.container}>
          <WebView bounces={false} scrollEnabled={true} source={{ uri: Environment.privacyPolicyUrl } } />
        </View>
      </SafeAreaView>
    );
  }
}

export default connect(mapStateToProps)(PrivacyPolicy);
