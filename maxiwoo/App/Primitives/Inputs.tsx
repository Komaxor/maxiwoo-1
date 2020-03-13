import I18n from 'i18n-js';
import React, { Ref } from 'react';
import { Icon, Input, InputProps } from 'react-native-elements';
import { Colors, Spacing, Typography } from '../Styles';

export const NormalInputWithLeftIcon = React.forwardRef((props: InputProps, ref: Ref<Input>) => (
  <Input
    ref={ref}
    leftIcon={<Icon name="user-o" type="font-awesome" color={Colors.darkBlue} size={Spacing.larger} />}
    leftIconContainerStyle={{ marginLeft: 5 }}
    inputStyle={{ marginLeft: 10, color: Colors.darkBlue }}
    inputContainerStyle={{
      paddingRight: 5,
      paddingLeft: 5,
      marginVertical: 10,
      borderColor: Colors.darkBlue,
      borderRadius: 10,
      borderWidth: Spacing.hairline,
    }}
    placeholderTextColor={Colors.lightGray}
    errorStyle={Typography.errorText}
    {...props}
  />
));

export const NormalInput = React.forwardRef((props: InputProps, ref: Ref<Input>) => (
  <Input
    ref={ref}
    inputStyle={{ color: Colors.darkBlue, marginHorizontal: 5 }}
    inputContainerStyle={{
      paddingRight: 5,
      paddingLeft: 5,
      marginVertical: 10,
      borderColor: Colors.darkBlue,
      borderRadius: 10,
      borderWidth: Spacing.hairline,
    }}
    placeholderTextColor={Colors.lightGray}
    errorStyle={Typography.errorText}
    {...props}
  />
));

export class PasswordInput extends React.Component<
  InputProps & { range: number; onValidated?: (isValid: boolean) => void },
  { isSecure: boolean; icon: string; errorMessage: string }
> {
  public state: { isSecure: boolean; icon: string; errorMessage: string } = { isSecure: true, icon: 'eye', errorMessage: '' };
  private _input = React.createRef<Input>();

  public render() {
    return (
      <Input
        ref={this._input}
        secureTextEntry={this.state.isSecure}
        rightIcon={
          <Icon
            name={this.state.icon}
            type="font-awesome"
            color={Colors.darkBlue}
            size={Spacing.larger}
            onPress={() => this.setState({ isSecure: !this.state.isSecure, icon: this.state.isSecure ? 'eye-slash' : 'eye' })}
          />
        }
        leftIconContainerStyle={{ marginLeft: 5 }}
        inputStyle={{ marginLeft: 10, color: Colors.darkBlue }}
        inputContainerStyle={{
          paddingRight: 5,
          paddingLeft: 5,
          marginVertical: 10,
          borderColor: Colors.darkBlue,
          borderRadius: 10,
          borderWidth: Spacing.hairline,
        }}
        placeholderTextColor={Colors.lightGray}
        errorStyle={Typography.errorText}
        onChangeText={e => {
          if (this.props.onValidated) {
            this.props.onValidated(this.validatePassword(e));
          }
        }}
        errorMessage={this.state.errorMessage}
        {...this.props}
      />
    );
  }

  public validatePassword = (text: string) => {
    if (text != null && text.match('(/^ *$/)') === null && text.length >= this.props.range) {
      this.setState({ errorMessage: null });
      return true;
    } else {
      this.setState({ errorMessage: I18n.t('register_password_errormessage', { number: this.props.range }) });
      this._input.current.shake();
      return false;
    }
  };

  // public onValidate = (isValid: boolean) => {

  // }

  public focus() {
    if (this._input.current) {
      this._input.current.focus();
    }
  }

  public shake() {
    if (this._input.current) {
      this._input.current.shake();
    }
  }
}

// tslint:disable-next-line: max-classes-per-file
export class EmailInput extends React.Component<InputProps, { email: string; emailErrorMessage: string }> {
  public state: { email: string; emailErrorMessage: string } = { email: '', emailErrorMessage: '' };
  private _input = React.createRef<Input>();

  public render() {
    return (
      <Input
        ref={this._input}
        inputStyle={{ marginLeft: 10, color: Colors.darkBlue }}
        inputContainerStyle={{
          paddingRight: 5,
          paddingLeft: 5,
          marginVertical: 10,
          borderColor: Colors.darkBlue,
          borderRadius: 10,
          borderWidth: Spacing.hairline,
        }}
        keyboardType="email-address"
        placeholderTextColor={Colors.lightGray}
        errorStyle={Typography.errorText}
        errorMessage={this.state.emailErrorMessage}
        onChangeText={email => {
          this.setState({ email });
        }}
        onSubmitEditing={() => this.validateEmail()}
        {...this.props}
      />
    );
  }

  public validateEmail = () => {
    if (this.state.email != null && this.state.email.endsWith('@ibs-b.hu') && this.state.email.match('(/^ *$/)') === null) {
      this.setState({ emailErrorMessage: null });
      return true;
    } else {
      this.setState({ emailErrorMessage: I18n.t('register_email_errormessage') });
      this._input.current.shake();
      return false;
    }
  };
}
