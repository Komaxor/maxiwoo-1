import { Ionicons } from '@expo/vector-icons';
import { Updates } from 'expo';
import I18n from 'i18n-js';
import React from 'react';
import { Alert, FlatList, ListRenderItemInfo, Platform, StyleSheet, View } from 'react-native';
import { ListItem } from 'react-native-elements';
import { connect } from 'react-redux';
import { getLocale, setLocale } from '../../../Locales';
import { Colors, Typography, Views } from '../../../Styles';
import { mapDispatchToProps, mapStateToProps } from './SelectLanguage.container';
import { Props, State } from './SelectLanguage.interfaces';

class SelectLanguage extends React.Component<Props, State> {
  public static navigationOptions = ({ navigation }) => ({
    title: I18n.t('settings_item_languages'),
    headerStyle: {
      textAlign: 'center',
      backgroundColor: Colors.headerBackground,
    },
    headerTintColor: Colors.headerText,
    headerLeft: (
      <Ionicons
        name={Platform.OS === "ios" ? "ios-arrow-back" : "md-arrow-back"}
        size={Platform.OS === "ios" ? 35 : 24}
        color='white'
        style={
          Platform.OS === "ios"
            ? { marginBottom: -4, width: 25, marginLeft: 9 }
            : { marginBottom: -4, width: 25, marginLeft: 20 }
        }
        onPress={() => {
          navigation.goBack();
        }}
      />
    )
  });

  private _styles = StyleSheet.create({
    container: { ...Views.base },
  });

  public render() {
    return (
      <View style={this._styles.container}>
        <FlatList
          data={[{ label: 'English', value: 'en' }, 
                  { label: 'Hungarian', value: 'hu' }
                ]}
          keyExtractor={item => item.value}
          renderItem={i => this._renderItem(i)}
          scrollEnabled={false}
        />
      </View>
    );
  }

  private _renderItem(row: ListRenderItemInfo<{ label: string; value: string }>) {
    return <ListItem title={row.item.label} titleStyle={Typography.normalButton} onPress={() => this._selectLanguage(row.item.value)} />;
  }

  private async _selectLanguage(langcode: string) {
    if (getLocale() === langcode) {
      this.props.navigation.goBack();
      return;
    }
    Alert.alert(I18n.t('settings_alert_title'), I18n.t('settings_restart_message'), [
      { text: I18n.t('settings_cancel'), onPress: () => null },
      {
        text: I18n.t('settings_restart_later'),
        onPress: async () => {
          this.props.setLanguage(langcode);
        },
      },
      {
        text: I18n.t('settings_restart_now'),
        onPress: async () => {
          this.props.setLanguage(langcode);
          await Updates.reload();
        },
        style: 'default',
      },
    ]);
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(SelectLanguage);
