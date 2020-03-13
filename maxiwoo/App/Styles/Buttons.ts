import { ImageStyle, StyleSheet, TextStyle, ViewStyle } from 'react-native';
import * as Colors from './Colors';
import * as Spacing from './Spacing';
import * as Typography from './Typography';

const base: ViewStyle | ImageStyle | TextStyle = {
  alignItems: 'center',
  marginRight: Spacing.smallest,
  marginVertical: Spacing.tiny,
};

const small: ViewStyle | ImageStyle | TextStyle = {
  paddingHorizontal: Spacing.small,
  paddingVertical: Spacing.small + 2,
  width: 75,
};

const large: ViewStyle | ImageStyle | TextStyle = {
  paddingHorizontal: Spacing.large,
  paddingVertical: Spacing.large + 4,
};

const normal: ViewStyle | ImageStyle | TextStyle = {
  paddingHorizontal: Spacing.base,
  paddingVertical: Spacing.base + 4,
};

const rounded: ViewStyle | ImageStyle | TextStyle = {
  borderRadius: 10,
};

const smallRounded: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  ...small,
  ...rounded,
};

const largeRounded: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  ...large,
  ...rounded,
};

const normalRounded: ViewStyle | ImageStyle | TextStyle = {
  ...base,
  ...rounded,
};

const normalButton: ViewStyle | ImageStyle | TextStyle = {
  ...normalRounded,
  backgroundColor: Colors.normalButtonBackground,
  borderColor: Colors.normalButtonBorder,
};

const highlightedButton: ViewStyle | ImageStyle | TextStyle = {
  ...normalRounded,
  backgroundColor: Colors.highlightedButtonBackground,
  borderColor: Colors.highlightedButtonBorder,
};

const linkButton: ViewStyle | ImageStyle | TextStyle = {
  backgroundColor: Colors.transparent,
  borderColor: Colors.transparent,
};

const cancelButton: ViewStyle | ImageStyle | TextStyle = {
  backgroundColor: Colors.transparent,
  borderColor: Colors.transparent,
};

export const Buttons = StyleSheet.create({
  normalButton,
  highlightedButton,
  linkButton,
  cancelButton,
});
