import Constants from 'expo-constants';
import I18n from 'i18n-js';
import { LogLevel } from './Logger/LogLevel';

export interface IConfig {
  privacyPolicyUrl: string;
  termsOfViewsUrl: string;
  apiUrl: string;
  logLevel: LogLevel;
  defaultUsername: string;
  defaultPassword: string;
  emailEndsValidation: boolean;
  appstoreUrl: string;
  emailusAddress: string;
  /**
   * Defines if http or mock api is expected
   * @returns true if http api is configured or false for mock
   */
  useHttpApi: (service: string, method: string) => boolean;
}

const ApiConfigs = {
  dev: {
    LoginService: {
      // you can define the api in service level
      default: true,
      // or in method level
      loginAsync: true,
    },
    CategoryService: {
      default: true,
      getCategoriesAsync: true,
      getCategoryAsync: true,
    },
    PlaceService: {
      default: true,
      getPlacesAsync: true,
      getPlaceAsync: true,
    },
    ProfileService: {
      default: true,
      getUserAsync: true,
    },
    ProfilePictureUpdateService: {
      default: true,
      updateImageAsync: true,
    },
    RegisterService: {
      default: true,
      registerAsync: true,
    },
    SetNewPasswordService: {
      default: true,
      setNewPasswordAsync: true,
    },
    ForgotPasswordService: {
      default: true,
      forgotPasswordAsync: true,
    },
    ChangePasswordService: {
      default: true,
      changePasswordAsync: true,
    },
    UnsubscribeService: {
      default: true,
      unsubscribeAsync: true,
    },
  },
  // define a bool to use that value for every api
  staging: true,
  prod: true,
};

function _useHttpApiWithEnv(apiConfig: any): (service: string, method: string) => boolean {
  return (service: string, method: string): boolean => {
    // if bool then we use the value, if not defined then fallback to true
    // if the service is and method is defined use that, if only the service defined use the default value
    // if nothing from the above, use true
    if (typeof apiConfig === 'boolean') {
      return apiConfig;
    } else if (apiConfig === undefined || apiConfig[service] === undefined) {
      return true;
    } else if (apiConfig[service][method] !== undefined) {
      return apiConfig[service][method];
    } else if (apiConfig[service].default !== undefined) {
      return apiConfig[service].default;
    }
  };
}

const ENV = {
  dev: {
    // apiUrl: 'http://dev.ibs.mxcsoftware.hu/api/',
    // apiUrl: 'http://localhost:61455/api/',
    apiUrl: 'http://updevfast-001-site1.atempurl.com/api/',
    logLevel: LogLevel.Debug,
    defaultUsername: 'test@ibs-b.hu',
    defaultPassword: 'Qwer1234',
    privacyPolicyUrl: 'http://dev.ibs.mxcsoftware.hu/privacypolicy',
    termsOfViewsUrl: 'http://dev.ibs.mxcsoftware.hu/termsofuse',
    appstoreUrl: 'https://apps.apple.com/us/app/slack/id618783545',
    emailusAddress: 'support@example.com',
    useHttpApi: _useHttpApiWithEnv(ApiConfigs.dev),
    emailEndsValidation: false,
  },
  staging: {
    apiUrl: 'http://staging.ibs.mxcsoftware.hu/api/',
    logLevel: LogLevel.Debug,
    defaultUsername: '',
    defaultPassword: '',
    privacyPolicyUrl: 'http://staging.ibs.mxcsoftware.hu/privacypolicy',
    termsOfViewsUrl: 'http://staging.ibs.mxcsoftware.hu/termsofuse',
    appstoreUrl: 'https://apps.apple.com/us/app/slack/id618783545',
    emailusAddress: 'support@example.com',
    useHttpApi: _useHttpApiWithEnv(ApiConfigs.staging),
    emailEndsValidation: false,
  },
  prod: {
    // apiUrl: 'http://localhost:61455/api/',
    // apiUrl: 'http://10.0.2.2:61455/api/',
    apiUrl: 'http://updevfast-001-site1.atempurl.com/api/',
    logLevel: LogLevel.Info,
    defaultUsername: '',
    defaultPassword: '',
    privacyPolicyUrl: 'http://updevfast-001-site1.atempurl.com/privacypolicy',
    termsOfViewsUrl: 'http://updevfast-001-site1.atempurl.com/termsofuse',
    appstoreUrl: 'https://apps.apple.com/us/app/slack/id618783545',
    emailusAddress: 'support@maxiwoo.com',
    useHttpApi: _useHttpApiWithEnv(ApiConfigs.prod),
    emailEndsValidation: true,
  },
};

function getEnvVars(env = ''): IConfig {
  if (env === null || env === undefined || env === '') return ENV.prod;
  if (env.indexOf('dev') !== -1) return ENV.dev;
  if (env.indexOf('staging') !== -1) return ENV.staging;
  if (env.indexOf('prod') !== -1) return ENV.prod;
}

export default getEnvVars(Constants.manifest.releaseChannel);
