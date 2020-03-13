import I18n from 'i18n-js';
import React from 'react';
import { Alert, FlatList, Linking, Platform, Share, StyleSheet, View } from 'react-native';
import { Image, ListItem } from 'react-native-elements';
import { connect } from 'react-redux';
import Environment from '../../Environment';
import { TabIcon } from '../../Primitives';
import { Colors, Typography, Views } from '../../Styles';
import { mapDispatchToProps, mapStateToProps } from './Settings.container';
import { ISettingsItem, Props, State } from './Settings.interfaces';

class Settings extends React.Component<Props, State> {
  public static navigationOptions = () => ({
    title: I18n.t('settings_title'),
    tabBarIcon: ({ tintColor }) => {
      return <TabIcon name="settings" type="simple-line-icon" color={tintColor} />;
    },
    headerTitleStyle: {
      ...Typography.titleText,
    },
    headerStyle: {
      textAlign: 'center',
      backgroundColor: Colors.headerBackground,
    },
    headerTintColor: Colors.headerText,
  });

  private _styles = StyleSheet.create({
    container: { ...Views.base },
  });

  public render() {
    return (
      <View style={this._styles.container}>
        <FlatList
          data={this.settingsItems.data}
          keyExtractor={item => item.title}
          renderItem={i => this._renderItem(i)}
          scrollEnabled={false}
          inverted={true}
        />
      </View>
    );
  }

  private _renderItem(row: { item: any }) {
    if (Platform.OS === 'android' && !row.item.visibleOnAndroid) return null;
    if (Platform.OS === 'ios' && !row.item.visibleOnIOS) return null;
    switch (row.item.type) {
      default:
        switch (row.item.icon.name) {
          case 'share':
            return (
              <ListItem
                leftIcon={<Image source={require('../../assets/share.png')} style={{width: 24, height: 25}}  />}
                title={row.item.title}
                titleStyle={Typography.normalButton}
                disabled={row.item.disabled}
                onPress={() => row.item.onPress(true)}
              />
            );
          case 'worldwide':
            return (
              <ListItem
                leftIcon={<Image source={require('../../assets/worldwide.png')} style={{width: 24, height: 24}}  />}
                title={row.item.title}
                titleStyle={Typography.normalButton}
                disabled={row.item.disabled}
                onPress={() => row.item.onPress(true)}
              />
            );
          case 'love-letter':
            return (
              <ListItem
                leftIcon={<Image source={require('../../assets/love-letter.png')} style={{width: 24, height: 24}}  />}
                title={row.item.title}
                titleStyle={Typography.normalButton}
                disabled={row.item.disabled}
                onPress={() => row.item.onPress(true)}
              />
            );
          case 'scroll':
            return (
              <ListItem
                leftIcon={<Image source={require('../../assets/scroll.png')} style={{width: 24, height: 24}}  />}
                title={row.item.title}
                titleStyle={Typography.normalButton}
                disabled={row.item.disabled}
                onPress={() => row.item.onPress(true)}
              />
            ); 
        }
        
    }
  }

  private _onShare = async () => {
    await Share.share({
      message: I18n.t('share_text') + Environment.appstoreUrl,
    });
  };

  public settingsItems: ISettingsItem = {
    data: [
      {
        title: I18n.t('policies_title'),
        onPress: () => this.props.navigation.navigate('TermsOfUse'),
        disabled: false,
        icon: { name: 'scroll', type: 'entypo' },
        type: 'simple',
        visibleOnIOS: true,
        visibleOnAndroid: true,
      },
      {
        title: I18n.t('settings_item_languages'),
        onPress: () => this.props.navigation.navigate('SelectLanguage'),
        disabled: false,
        icon: { name: 'worldwide', type: 'entypo' },
        type: 'simple',
        visibleOnIOS: true,
        visibleOnAndroid: true,
      },
      {
        title: I18n.t('settings_item_emailus'),
        onPress: async () =>
          Linking.openURL(`mailto:${Environment.emailusAddress}?subject=${I18n.t('email_us_subject')}&body=${I18n.t('email_us_body')}`),
        disabled: false,
        icon: { name: 'love-letter', type: 'entypo' },
        type: 'simple',
        visibleOnIOS: true,
        visibleOnAndroid: true,
      },
      {
        title: I18n.t('settings_item_share'),
        onPress: async () => this._onShare(),
        disabled: false,
        icon: { name: 'share', type: 'entypo' },
        type: 'simple',
        visibleOnIOS: true,
        visibleOnAndroid: true,
      },
    ],
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(Settings);
