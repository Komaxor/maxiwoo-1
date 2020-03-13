import Constants from 'expo-constants';

export const assetsImages = {

  discount_card: Constants.isDetached ? { uri: 'discount_card' } : require('../assets/discount_card.png'),
  // icon_512x512: Constants.isDetached ? { uri: 'icon_512x512' } : require('../assets/icon_512x512.png'),
  icon: Constants.isDetached ? { uri: 'icon' } : require('../assets/icon.png'),
  no_image: Constants.isDetached ? { uri: 'no_image' } : require('../assets/no_image.jpg'),
  play_store_feature_background: Constants.isDetached
    ? { uri: 'play_store_feature_background' }
    : require('../assets/play_store_feature_background.jpeg'),
  profile_placeholder: Constants.isDetached ? { uri: 'profile_placeholder' } : require('../assets/profile_placeholder.jpg'),
  splash: Constants.isDetached ? { uri: 'splash' } : require('../assets/splash.png'),
};
