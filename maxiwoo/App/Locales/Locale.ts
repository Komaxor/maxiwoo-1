import * as Localization from 'expo-localization';
import I18n from 'i18n-js';
// import moment from 'moment/min/moment-with-locales';
import { en } from './en';
import { hu } from './hu';
// tslint:disable-next-line: no-var-requires
const moment = require('moment/min/moment-with-locales');

export const localeSet = () => {
  I18n.fallbacks = true;

  I18n.translations = {
    en,
    hu,
  };
  // const { locale } = await Localization.getLocalizationAsync();
  // I18n.locale = locale;
  // setLocale(getLocaleInISO2());
  moment.locale(getLocaleInISO2());
  // I18n.locale = Localization.locale;
};

export const localeSetOnApp = () => {
  I18n.fallbacks = true;

  I18n.translations = {
    en,
    hu,
  };
  
  moment.locale(getLocaleInISO2());
};

export const getLocale = (): string => {
  return I18n.locale;
};

export const setLocale = (langTag: string) => {
  I18n.locale = langTag;
  moment.locale(getLocaleInISO2());
};

export function getLocaleInISO2(): string {
  const locale = I18n.currentLocale();

  if (locale.startsWith('hu')) {
    return 'hu';
  } else if (locale.startsWith('en')) {
    return 'en';
  } else if (locale.startsWith('bg')) {
    return 'bg';
  } else {
    return 'en';
  }
}

export const PLACEEMPLOYEELANG = 'hu';
