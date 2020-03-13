import { Ionicons } from '@expo/vector-icons';
import Constants from 'expo-constants';
import I18n from 'i18n-js';
import React from 'react';
import {
  ActivityIndicator,
  Alert,
  Animated,
  AsyncStorage,
  Dimensions,
  Linking,
  Platform,
  SafeAreaView,
  Share,
  StatusBar,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from 'react-native';
import { Image } from 'react-native-elements';
import MapView, { Marker } from 'react-native-maps';
import { connect } from 'react-redux';
import Environment from '../../../Environment';
import { usingAsync } from '../../../Helpers';
import { logger } from '../../../Logger';
import { CircleIcon, HighlightedButton, NormalButton } from '../../../Primitives';
import { assetsImages } from '../../../Primitives/Images';
import { Colors, Spacing, Typography, Views } from '../../../Styles';
import { PlaceService } from '../../Services/PlaceService';
import { mapStateToProps } from './PlaceDetails.container';
import { Props, State } from './PlaceDetails.interfaces';

class PlaceDetails extends React.Component<Props, State> {
  public static navigationOptions = ({ navigation }) => ({
    headerStyle: {
      backgroundColor: 'transparent',
      borderBottomWidth: 0,
    },
    headerTitleStyle: {
      fontWeight: undefined,
    },
    headerTransparent: true,
    headerTintColor: Colors.white,
    headerLeft: (
      <Ionicons
        name={Platform.OS === "ios" ? "ios-arrow-back" : "md-arrow-back"}
        size={Platform.OS === "ios" ? 35 : 24}
        color='white'
        style={
          Platform.OS === "ios"
            ? { marginBottom: -4, width: 25, marginLeft: 9 }
            : { marginBottom: -4, width: 25, marginLeft: 20 }
        }
        onPress={() => {
          navigation.goBack();
        }}
      />
    )
  });

  private readonly TITLE_MAX_HEIGHT = 60
  private readonly HEADER_MAX_HEIGHT = 300;
  private readonly MAP_MAX_HEIGHT = 300;
  private readonly HEADER_MIN_HEIGHT = Constants.statusBarHeight;
  private readonly HEADER_SCROLL_DISTANCE = this.HEADER_MAX_HEIGHT - this.HEADER_MIN_HEIGHT;
  private readonly HEADER_HEIGHT = Platform.OS === 'ios' ? 44 : 60;
  public state: State = {
    place: undefined,
    scrollY: new Animated.Value(
      // iOS has negative initial scroll value because content inset...
      Platform.OS === 'ios' ? -this.HEADER_MAX_HEIGHT : Constants.statusBarHeight,
    ),
    isFavorite: undefined,
    favourites: [],
  };

  private _placeService = new PlaceService();

  public render() {
    if (!this.state.place) {
      return (
        <View style={this._styles.emptyContainer}>
          <Text>{I18n.t('place_details_empty_list')}</Text>
        </View>
      );
    }
    const scrollY = Animated.add(
      this.state.scrollY,
      Platform.OS === 'ios' ? this.HEADER_MAX_HEIGHT : Constants.statusBarHeight,
    );
    logger.log(Constants.statusBarHeight);
    const headerTranslate = scrollY.interpolate({
      inputRange: [0, this.HEADER_SCROLL_DISTANCE],
      outputRange: [0, -this.HEADER_SCROLL_DISTANCE],
      extrapolate: 'clamp',
    });

    const imageOpacity = scrollY.interpolate({
      inputRange: [0, this.HEADER_SCROLL_DISTANCE / 2, this.HEADER_SCROLL_DISTANCE],
      outputRange: [1, 1, 0],
      extrapolate: 'clamp',
    });
    const imageTranslate = scrollY.interpolate({
      inputRange: [0, this.HEADER_SCROLL_DISTANCE],
      outputRange: [0, -this.HEADER_SCROLL_DISTANCE],
      extrapolate: 'clamp',
    });

    const titleScale = scrollY.interpolate({
      inputRange: [0, (this.HEADER_SCROLL_DISTANCE / 5) * 4, this.HEADER_SCROLL_DISTANCE],
      outputRange: [1, 1, this.HEADER_HEIGHT / this.TITLE_MAX_HEIGHT],
      extrapolate: 'clamp',
    });
    const titleTranslate = scrollY.interpolate({
      inputRange: [0, (this.HEADER_SCROLL_DISTANCE / 5) * 4, this.HEADER_SCROLL_DISTANCE],
      outputRange: [0, -1 * (this.HEADER_SCROLL_DISTANCE / 5) * 4, -this.HEADER_SCROLL_DISTANCE - ((this.TITLE_MAX_HEIGHT - this.HEADER_HEIGHT) / 2) ],
      extrapolate: 'clamp',
    });

    const titleOpacity = scrollY.interpolate({
      inputRange: [0, (this.HEADER_SCROLL_DISTANCE / 5) * 4, this.HEADER_SCROLL_DISTANCE],
      outputRange: [0, 0, 1],
      extrapolate: 'clamp',
    });

    const titleMaskOpacity = scrollY.interpolate({
      inputRange: [0, (this.HEADER_SCROLL_DISTANCE / 5) * 4, this.HEADER_SCROLL_DISTANCE],
      outputRange: [1, 1, 0],
      extrapolate: 'clamp',
    });

    const likeBtnOpacity = scrollY.interpolate({
      inputRange: [0, (this.HEADER_SCROLL_DISTANCE / 5) * 4, (this.HEADER_SCROLL_DISTANCE / 5) * 4 + 1, this.HEADER_SCROLL_DISTANCE],
      outputRange: [1, 1, 0, 0],
      extrapolate: 'clamp',
    });
    const titleColor = scrollY.interpolate({
      inputRange: [0, (this.HEADER_SCROLL_DISTANCE / 5) * 4, this.HEADER_SCROLL_DISTANCE],
      outputRange: [Colors.darkBrown, Colors.darkBrown, 'rgb(255,255,255)'],
      extrapolate: 'clamp',
    });
    const isMapAvailable = (this.state.place.address.displayAddress)&&(+this.state.place.address.latitude>=-90)
    &&(+this.state.place.address.latitude<=90)&&(+this.state.place.address.longitude>=-180)&&(+this.state.place.address.longitude<=180);

    const minimumHeight = isMapAvailable ? Dimensions.get('window').height - this.MAP_MAX_HEIGHT - this.HEADER_MIN_HEIGHT:Dimensions.get('window').height - this.HEADER_MIN_HEIGHT;
    
    logger.debug(I18n.locale);
    return (
      <View style={this._styles.fill}>
        <StatusBar
          translucent
          barStyle="light-content"
          backgroundColor="rgba(0, 0, 0, 0.251)"
        />
          <Animated.ScrollView
            style={{ flex: 1 }}
            scrollEventThrottle={100}
            onScroll={Animated.event(
              [{ nativeEvent: { contentOffset: { y: this.state.scrollY } } }],
              { useNativeDriver: true },
            )}
            contentInset={{
              top: this.HEADER_MAX_HEIGHT,
            }}
            contentOffset={{
              y: -this.HEADER_MAX_HEIGHT,
            }}
          >
            <View style={[this._styles.scrollViewContent/*, { minHeight: minimumHeight }*/]}>
              
              <Text style={this._styles.sectionTitle}>{ this.state.place.type }</Text>
              <View style={this._styles.container1}>
                <TouchableOpacity onPress={() => this._onShare()}>
                  <View style={this._styles.buttonContainer}>
                    <View style={this._styles.outerCircle}>
                        <View style={this._styles.innerCircle} >
                          <Image source={require('../img/share.png')} style={{width: 24, height: 25}}  />
                        </View>
                      </View>
                  </View>
                </TouchableOpacity>
                { !(this.state.place.phoneNumber === null || this.state.place.phoneNumber === '') ? (
                  <TouchableOpacity onPress={() => this.callNumber(this.state.place.phoneNumber)}>
                    <View style={this._styles.buttonContainer}>
                      <View style={this._styles.outerCircle}>
                        <View style={this._styles.innerCircle} >
                          <Image source={require('../img/phone.png')} style={{width: 24, height: 25}}  />
                        </View>
                      </View>
                    </View>
                  </TouchableOpacity>) : null
                }
                { !(this.state.place.email === null || this.state.place.email === '') ? (
                  <TouchableOpacity onPress={() => Linking.openURL(`mailto:${this.state.place.email}?body=Hi! I found you on MaxiWoo!`) }>
                    <View style={this._styles.buttonContainer}>
                      <View style={this._styles.outerCircle}>
                        <View style={this._styles.innerCircle}>
                          <Image source={require('../img/email.png')} style={{width: 24, height: 24}}  />
                        </View>
                      </View>
                    </View>
                  </TouchableOpacity>) : null
                }
                { !(this.state.place.web === null || this.state.place.web === '') ? (
                  <TouchableOpacity onPress={() => this._openWeb()}>
                    <View style={this._styles.buttonContainer}>
                      <View style={this._styles.outerCircle}>
                        <View style={this._styles.innerCircle}>
                          <Image source={require('../img/www.png')} style={{width: 24, height: 24}}  />
                        </View>
                      </View>
                    </View>
                  </TouchableOpacity>) : null
                }
                { !(this.state.place.facebook === null || this.state.place.facebook === '') ? (
                  <TouchableOpacity onPress={() => Linking.openURL(`${this.state.place.facebook}`) }>
                    <View style={this._styles.buttonContainer}>
                      <View style={this._styles.outerCircle}>
                        <View style={this._styles.innerCircle}>
                          <Image source={require('../img/facebook-logo.png')} style={{width: 24, height: 25}}  />
                        </View>
                      </View>
                    </View>
                  </TouchableOpacity>) : null
                }
                { !(this.state.place.instagram === null || this.state.place.instagram === '') ? (
                  <TouchableOpacity onPress={() => Linking.openURL(`${this.state.place.instagram}`) }>
                    <View style={this._styles.buttonContainer}>
                      <View style={this._styles.outerCircle}>
                        <View style={this._styles.innerCircle}>
                          <Image source={require('../img/Instagram_logo.png')} style={{width: 24, height: 25}}  />
                        </View>
                      </View>
                    </View>
                  </TouchableOpacity>) : null
                }
              </View>
              <Text style={this._styles.sectionBody}>{ this.state.place.about }</Text>
              { ((this.state.place.openingHours.monday)||(this.state.place.openingHours.tuesday)||(this.state.place.openingHours.wednesday)||(this.state.place.openingHours.thursday)||(this.state.place.openingHours.friday)||(this.state.place.openingHours.saturday)||(this.state.place.openingHours.sunday)) ? (
                <Text style={this._styles.sectionSubTitle}>{ I18n.t('opening_hours') }:</Text>) : null
              }
              {this._renderOpeningHours()}
            </View>
            { ((this.state.place.address.displayAddress)&&(+this.state.place.address.latitude>=-90)
                  &&(+this.state.place.address.latitude<=90)&&(+this.state.place.address.longitude>=-180)&&(+this.state.place.address.longitude<=180)) ? (
              <MapView
                style={{ height: this.MAP_MAX_HEIGHT, marginHorizontal: 0, marginTop: Spacing.base }}
                initialRegion={{
                  latitude: +this.state.place.address.latitude,
                  longitude: +this.state.place.address.longitude,
                  latitudeDelta: 0.0922,
                  longitudeDelta: 0.0421,
                }}
              >
                <Marker
                  coordinate={{
                    latitude: +this.state.place.address.latitude,
                    longitude: +this.state.place.address.longitude,
                  }}
                  title={this.state.place.address.displayAddress}
                />
              </MapView>): null
            }
            { ((this.state.place.address.displayAddress)&&(+this.state.place.address.latitude>=-90)
                  &&(+this.state.place.address.latitude<=90)&&(+this.state.place.address.longitude>=-180)&&(+this.state.place.address.longitude<=180)) ? (
              <TouchableOpacity style={this._styles.navigationButton} onPress={() => this._openNavigationApplication()}>
                <CircleIcon name="directions" reverseColor={Colors.white} />
              </TouchableOpacity>): null
            }
          </Animated.ScrollView>
          <Animated.View style={[this._styles.header, { transform: [{ translateY: headerTranslate }] }]}/>
          <Animated.View style={[this._styles.backgroundImage, { opacity: imageOpacity, transform: [{ translateY: imageTranslate }] }]}>
            <Image
              source={
                this.state.place.previewImage == null
                  ? assetsImages.no_image
                  : {
                    uri: `${Environment.apiUrl}v1/image/${this.state.place.previewImage}`,
                  }
              }
              style={this._styles.backgroundImage}
              PlaceholderContent={<ActivityIndicator />}
            />
          </Animated.View>
          <Animated.View style={[this._styles.bar, { flexDirection: 'row' }, { opacity: titleOpacity, transform: [{ translateY: titleTranslate }, { scaleY: titleScale }] }]}>
            <Animated.Text style={[this._styles.title, { flex: 1 }, { color:'white', flexWrap: 'wrap', transform: [{ scaleX: titleScale }] }]} numberOfLines={2} ellipsizeMode='tail'>{this.state.place.name}</Animated.Text>
          </Animated.View>
          <Animated.View style={[this._styles.maskBar, { flexDirection: 'row' }, { opacity: titleMaskOpacity, transform: [{ translateY: titleTranslate }, { scaleY: titleScale }] }]}>
            <Animated.Text style={[this._styles.title, { flex: 1 }, { flexWrap: 'wrap', transform: [{ scaleX: titleScale }] }]} numberOfLines={2} ellipsizeMode='tail'>{this.state.place.name}</Animated.Text>
          </Animated.View>
          <Animated.View style={[this._styles.heartButtonContainer, { opacity: likeBtnOpacity, transform: [{ translateY: titleTranslate }] }]} >
            <TouchableOpacity style={[this._styles.heartCircle]} activeOpacity = {1} onPress={() =>  this._toggleFavourite() } >
              <Image source={ this.state.isFavorite === true ?                  
                        require('../img/Like.png') : 
                        require('../img/Dislike.png')} style={{width: 30, height: 27}}  />
            </TouchableOpacity>
          </Animated.View>
      </View>
    );
  }

  public async componentDidMount() {
    await this._getFavourites();
    const response = await usingAsync(this.props.loading.show(I18n.t('place_details_downloading')), () =>
      this._placeService.getPlaceAsync(this.props.navigation.state.params.placeId)
    );

    if (response.isSuccess) {
      this.setState({ place: response.content });
    }
  }

  private _renderOpeningHours() {
    return (
      <View style={{ flex: 1/*, 
      maxHeight: 200,*/}}>
        { (this.state.place.openingHours.monday) ? (
          <View style={ this._styles.openingHoursView }>
            <Text style={ this._styles.openingHoursStartText }>{I18n.t('monday')}</Text>
            <Text style={ this._styles.openingHoursEndText }>{this.state.place.openingHours.monday}</Text>
          </View>) : null
        }
        { (this.state.place.openingHours.tuesday) ? (
          <View style={ this._styles.openingHoursView }>
            <Text style={ this._styles.openingHoursStartText }>{I18n.t('tuesday')}</Text>
            <Text style={ this._styles.openingHoursEndText }>{this.state.place.openingHours.tuesday}</Text>
          </View>) : null
        }
        { (this.state.place.openingHours.wednesday) ? (
          <View style={ this._styles.openingHoursView }>
            <Text style={ this._styles.openingHoursStartText }>{I18n.t('wednesday')}</Text>
            <Text style={ this._styles.openingHoursEndText }>{this.state.place.openingHours.wednesday}</Text>
          </View>) : null
        }
        { (this.state.place.openingHours.thursday) ? (
          <View style={ this._styles.openingHoursView }>
            <Text style={ this._styles.openingHoursStartText }>{I18n.t('thursday')}</Text>
            <Text style={ this._styles.openingHoursEndText }>{this.state.place.openingHours.thursday}</Text>
          </View>) : null
        }
        { (this.state.place.openingHours.friday) ? (
          <View style={ this._styles.openingHoursView }>
            <Text style={ this._styles.openingHoursStartText }>{I18n.t('friday')}</Text>
            <Text style={ this._styles.openingHoursEndText }>{this.state.place.openingHours.friday}</Text>
          </View>) : null
        }
        { (this.state.place.openingHours.saturday) ? (
          <View style={ this._styles.openingHoursView }>
            <Text style={ this._styles.openingHoursStartText }>{I18n.t('saturday')}</Text>
            <Text style={ this._styles.openingHoursEndText }>{this.state.place.openingHours.saturday}</Text>
          </View>) : null
        }
        { (this.state.place.openingHours.sunday) ? (
          <View style={ this._styles.openingHoursView }>
            <Text style={ this._styles.openingHoursStartText }>{I18n.t('sunday')}</Text>
            <Text style={ this._styles.openingHoursEndText }>{this.state.place.openingHours.sunday}</Text>
          </View>) : null
        }
      </View>
    );
  }

  private _openNavigationApplication = async () => {
    let url = `https://www.google.com/maps/dir/?api=1&destination=${this.state.place.address.displayAddress}`;

    if (Platform.OS === 'ios') {
      url = `http://maps.apple.com/?ll=${this.state.place.address.latitude},${this.state.place.address.longitude}&address=${this.state.place.address.displayAddress}`;
    }
    const result = await Linking.openURL(url);

    logger.debug(result);
  };

  private _openWeb = async () => {
    const url = `${this.state.place.web}`;

    const result = await Linking.openURL(url);

    logger.debug(result);
  };

  private _onShare = async () => {
    let shareLink = '';
    const isMapAvailable = (this.state.place.address.displayAddress)&&(+this.state.place.address.latitude>=-90)
    &&(+this.state.place.address.latitude<=90)&&(+this.state.place.address.longitude>=-180)&&(+this.state.place.address.longitude<=180);
    if (isMapAvailable) {
      shareLink = this.state.place.address.displayAddress + ' (' + this.state.place.address.latitude + ', ' + this.state.place.address.longitude + ')';
    }
    if (this.state.place.web) {
      shareLink = this.state.place.web;
    }
    if (this.state.place.phoneNumber) {
      shareLink = this.state.place.phoneNumber;
    }
    if (this.state.place.email) {
      shareLink = this.state.place.email;
    }
    if (this.state.place.instagram) {
      shareLink = this.state.place.instagram;
    }
    if (this.state.place.facebook) {
      shareLink = this.state.place.facebook;
    }
    await Share.share({
      message: I18n.t('share_professional') + this.props.navigation.state.params.category + I18n.t('share_profile_text') + shareLink,
    });
  };

  private callNumber = (phone: string) => {
    logger.log('callNumber ----> ', phone);
    let phoneNumber = phone;
    if (Platform.OS !== 'android') {
    phoneNumber = `telprompt:${phone}`;
    }
    else  {
    phoneNumber = `tel:${phone}`;
    }
    Linking.canOpenURL(phoneNumber)
    .then(supported => {
    if (!supported) {
        Alert.alert('Phone number is not available');
      } else {
        return Linking.openURL(phoneNumber);
    }
    })
    .catch(err => logger.log(err));
  };

  private async _getFavourites() {
    try {
      const myFavourites = await AsyncStorage.getItem('@MyFavourites:key');
      if (myFavourites !== null) {
        // We have data!!
        const placeId: string = this.props.navigation.state.params.placeId;
        logger.log('placeid: ' + placeId)
        this.setState({ favourites: JSON.parse(myFavourites) });
        if (myFavourites.includes(placeId)) {
          this.setState( {isFavorite: true });
          logger.log('true');
        }
        logger.log(JSON.parse(myFavourites));
      }
    } catch (error) {
      logger.log(error);
    }
  };

  private async _toggleFavourite() {
    logger.log('toggle');
    this.setState({ isFavorite: !this.state.isFavorite });
    const tempArray = this.state.favourites;
    const placeId: string = this.props.navigation.state.params.placeId;
    logger.log(placeId);
    const index = tempArray.indexOf(placeId, 0);
    if (index > -1) {
      tempArray.splice(index, 1);
    } else {
      tempArray.push(placeId);
    }
    try {
      await AsyncStorage.setItem('@MyFavourites:key', JSON.stringify(tempArray));
    } catch (error) {
      logger.log(error);
    } finally {
      this.setState({ favourites: tempArray })
      logger.log(tempArray.length)
    }
  };

  private _styles = StyleSheet.create({
    fill: {
      flex: 1,
    },
    content: {
      flex: 1,
    },
    header: {
      position: 'absolute',
      top: 0,
      left: 0,
      right: 0,
      backgroundColor: Colors.primary,
      overflow: 'hidden',
      height: this.HEADER_MAX_HEIGHT,
    },
    backgroundImage: {
      position: 'absolute',
      top: 0,
      left: 0,
      right: 0,
      width: null,
      height: this.HEADER_MAX_HEIGHT,
      resizeMode: 'cover',
    },
    bar: {
      backgroundColor: Colors.primary,
      marginTop: this.HEADER_MAX_HEIGHT,
      height: this.TITLE_MAX_HEIGHT,
      alignItems: 'center',
      justifyContent: 'center',
      position: 'absolute',
      top: 0,
      left: 0,
      right: 0,
    },
    maskBar: {
      backgroundColor: Colors.white,
      marginTop: this.HEADER_MAX_HEIGHT,
      height: this.TITLE_MAX_HEIGHT,
      alignItems: 'center',
      justifyContent: 'center',
      position: 'absolute',
      top: 0,
      left: 0,
      right: 0,
    },
    title: {
      ...Typography.titleText,
      textAlign: "center",
      color: Colors.darkBrown,
      paddingLeft: 80,
      paddingRight: 80
    },
    scrollViewContent: {
      // iOS uses content inset, which acts like padding.
      paddingTop: Platform.OS !== 'ios' ? this.HEADER_MAX_HEIGHT : 0,
      marginTop: this.TITLE_MAX_HEIGHT,
      padding: Spacing.base,
    },
    container: {
      ...Views.base,
      justifyContent: 'flex-start',
      backgroundColor: Colors.white,
      marginTop: Platform.OS === 'android' ? StatusBar.currentHeight : 0,
    },
    emptyContainer: {
      ...Views.base,
      flexDirection: 'column',
      justifyContent: 'flex-start',
      alignItems: 'center',
    },
    sectionTitle: { 
      ...Typography.headerText, 
      color: Colors.darkBrown, 
      fontWeight: 'bold', 
      marginVertical: Spacing.base 
    },
    sectionSubTitle: { 
      ...Typography.bodyText, 
      color: Colors.darkBrown, 
      marginVertical: Spacing.base 
    },
    sectionBody: {
      ...Typography.bodyText,
      marginVertical: Spacing.largest,
      textAlign: 'justify'
    },
    container1: {
      flex: 2,
      justifyContent: 'space-between',
      flexDirection: 'row',
      backgroundColor: 'white',
      alignItems: 'center',
      // maxHeight: 48
    },
    buttonContainer: {
      flexDirection: 'row',
      justifyContent: 'center',
      alignItems: 'center',
      // maxHeight: 48
    },
    outerCircle: {
      borderRadius: 20,
      width: 40,
      height: 40,
      backgroundColor: Colors.darkBrown,
      ...Platform.select({
        ios: {
          shadowColor: Colors.darkBrown,
          shadowOpacity: 0.25,
          shadowRadius: 4,
          shadowOffset: {
            height: 4,
            width: 0
          },
        },
        android: {
          elevation: 4
        },
      }),
    },
    innerCircle: {
      borderRadius: 19,
      width: 38,
      height: 38,
      margin: 1,
      backgroundColor: 'white',
      justifyContent: 'center',
      alignItems: 'center'
    },
    heartButtonContainer: {
      position: 'absolute',
      left: 0,
      right: 0,
      justifyContent: 'flex-end',
      alignItems: 'flex-end',
      color: 'red',
      width: '100%',
      height: 48,
      top: this.HEADER_MAX_HEIGHT - 24,
    },
    heartCircle: {
      zIndex: 2,
      borderRadius: 24,
      borderWidth: 1,
      borderColor: Colors.darkBrown,
      width: 48,
      height: 48,
      right: 34,
      backgroundColor: 'white',
      justifyContent: 'center',
      alignItems: 'center',
      ...Platform.select({
        ios: {
          shadowColor: Colors.darkBrown,
          shadowOpacity: 0.25,
          shadowRadius: 4,
          shadowOffset: {
            height: 4,
            width: 0
          },
        },
        android: {
          elevation: 4
        },
      }),
    },
    navigationButton: {
      flex: 1,
      flexDirection: 'row',
      position: 'absolute',
      bottom: this.MAP_MAX_HEIGHT - 65,
      alignSelf: 'flex-end',
      justifyContent: 'space-between',
      backgroundColor: 'transparent',
    },
    openingHoursView: {
      flex: 1, 
      flexDirection: 'row', 
      justifyContent: 'space-around'
    },
    openingHoursStartText: {
      flex: 1,
      alignContent: 'flex-start',
      color: Colors.darkBrown,
    },
    openingHoursEndText: {
      flex: 1,
      alignContent: 'flex-end',
      color: Colors.darkBrown,
    },
  });
}

export default connect(mapStateToProps)(PlaceDetails);
