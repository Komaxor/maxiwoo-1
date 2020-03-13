import { ImageStyle, StyleSheet, TextStyle, ViewStyle } from 'react-native';
import * as Colors from './Colors';
export const extraLargeFontSize = 32;
export const largeFontSize = 24;
export const titleFontSize = 20;
export const buttonFontSize = 18;
export const baseFontSize = 16;
export const smallFontSize = 14;
export const smallerFontSize = 12;
export const smallestFontSize = 10;
export const largeHeaderFontSize = 20;
export const headerFontSize = 18;
export const subHeaderFontSize = 16;
export const menuFontSize = buttonFontSize;

const base: ViewStyle | ImageStyle | TextStyle = {
  alignItems: 'center',
  display: 'flex',
  flexDirection: 'row',
  justifyContent: 'center',
};

const link: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.blue,
};

const bodyText: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.baseText,
  fontSize: smallFontSize,
  lineHeight: 19,
};

const titleText: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.white,
  fontSize: titleFontSize,
};

const headerText: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.blue,
  fontSize: headerFontSize,
  fontWeight: 'bold',
};
const listTitleText: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.darkBrown,
  fontSize: headerFontSize,
};
const subHeaderText: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.blue,
  fontSize: subHeaderFontSize,
};

const descriptionText: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.baseText,
  fontSize: smallFontSize,
};

const errorText: ViewStyle | ImageStyle | TextStyle = {
  color: Colors.errorText,
  fontSize: smallFontSize,
};

const screenHeader: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  color: Colors.baseText,
  fontSize: largeFontSize,
  fontWeight: 'bold',
};

const screenFooter: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  ...descriptionText,
};

const menuItem: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  fontSize: menuFontSize,
  color: Colors.darkBlue,
};

const normalButton: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  color: Colors.normalButtonText,
};

const highlightedButton: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  color: Colors.highlightedButtonText,
  fontWeight: 'bold',
};

export const Typography = StyleSheet.create({
  base,
  normalButton,
  menuItem,
  screenHeader,
  screenFooter,
  link,
  bodyText,
  errorText,
  titleText,
  listTitleText,
  headerText,
  subHeaderText,
  highlightedButton,
});
