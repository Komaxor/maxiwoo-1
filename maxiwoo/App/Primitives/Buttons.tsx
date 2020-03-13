import React from 'react';
import { Button, ButtonProps, Icon, Input } from 'react-native-elements';
import { Buttons, Colors, Typography } from '../Styles';

export const NormalButton = props => <Button buttonStyle={Buttons.normalButton} titleStyle={Typography.normalButton} type="outline" {...props} />;

export const HighlightedButton = (props: ButtonProps) => (
  <Button buttonStyle={Buttons.highlightedButton} titleStyle={Typography.highlightedButton} type="solid" {...props} />
);

export const LinkButton = props => <Button buttonStyle={Buttons.linkButton} titleStyle={Typography.bodyText} type="clear" {...props} />;
