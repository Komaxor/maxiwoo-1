import moment from 'moment/min/moment-with-locales';

export class DateHelper {
  public get now(): moment.Moment {
    return moment();
  }
}
