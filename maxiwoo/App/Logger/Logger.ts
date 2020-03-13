import Sentry from 'sentry-expo';
import { default as Env } from '../Environment';
import { LogLevel } from './LogLevel';

// tslint:disable:no-console
export class Logger {
  public error(message?: any, ...optionalParams: any[]): void {
    if (Env.logLevel > LogLevel.Error) return;
    console.error(message, this.printIfNotEmpty(optionalParams));
    Sentry.captureException(message, optionalParams);
  }

  public info(message?: any, ...optionalParams: any[]): void {
    if (Env.logLevel > LogLevel.Info) return;

    console.info(message, this.printIfNotEmpty(optionalParams));
  }

  public log(message?: any, ...optionalParams: any[]): void {
    if (Env.logLevel > LogLevel.Info) return;

    console.log(message, this.printIfNotEmpty(optionalParams));
  }

  public warn(message?: any, ...optionalParams: any[]): void {
    if (Env.logLevel > LogLevel.Warn) return;

    console.warn(message, this.printIfNotEmpty(optionalParams));
    Sentry.captureException(message, optionalParams);
  }

  public debug(message?: any, ...optionalParams: any[]): void {
    if (Env.logLevel > LogLevel.Debug) return;

    console.debug(message, this.printIfNotEmpty(optionalParams));
  }

  private printIfNotEmpty(params: any[]): any {
    if (params.length < 1) {
      return '';
    }
    return params;
  }
}
