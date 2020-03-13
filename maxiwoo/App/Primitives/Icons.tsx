import React, { Ref } from 'react';
import { Icon, IconProps, SocialIcon } from 'react-native-elements';
import { Colors, Spacing, Typography } from '../Styles';

export const TabIcon = React.forwardRef((props: IconProps, ref: Ref<Icon>) => <Icon ref={ref} type="material" {...props} />);

export const RightHeaderIcon = React.forwardRef((props: IconProps, ref: Ref<Icon>) => (
  <Icon ref={ref} color={Colors.white} iconStyle={{ padding: Spacing.small }} type="material" {...props} />
));

export const CircleIcon = React.forwardRef((props: IconProps, ref: Ref<Icon>) => (
  <Icon ref={ref} color={Colors.position} iconStyle={{ padding: Spacing.small }} type="material" raised reverse {...props} />
));

export const LinkBtn = React.forwardRef((props: IconProps, ref: Ref<Icon>) => (
  <SocialIcon button light type='instagram' />
));
