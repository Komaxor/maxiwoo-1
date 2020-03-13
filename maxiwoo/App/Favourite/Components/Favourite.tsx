import I18n from 'i18n-js';
import React from 'react';
import { Animated, AsyncStorage, FlatList, Keyboard, KeyboardAvoidingView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Image, ListItem, SearchBar } from 'react-native-elements';
import { SafeAreaView } from 'react-navigation';
import { connect } from 'react-redux';
import Environment from '../../Environment';
import { usingAsync } from '../../Helpers';
import { logger } from '../../Logger';
import { IPlaceHeader } from '../../Places/Models/IPlaceHeader';
import { PlaceService } from '../../Places/Services/PlaceService';
import { RightHeaderIcon, TabIcon } from '../../Primitives';
import { assetsImages } from '../../Primitives/Images';
import { Colors, Typography, Views } from '../../Styles';
import { mapStateToProps } from './Favourite.container';
import { Props, State } from './Favourite.interfaces';

class Favourites extends React.Component<Props, State> {
  public static navigationOptions = ({ navigation }) => ({
    title: I18n.t('favourite_title'),
    tabBarIcon: ({ focused }) =>  {
      if (focused===true) {
        return <Image source={require('../../assets/Heart-active.png')} style={{width: 24, height: 20}}  />;
      } else {
        return <Image source={require('../../assets/Heart.png')} style={{width: 24, height: 20}}  />;
      }
    },
    headerTitleStyle: {
      ...Typography.headerText,
      color: Colors.white,
    },
    headerStyle: {
      backgroundColor: Colors.headerBackground,
      numberOfLines: 2
    },
    headerTintColor: Colors.headerText,
    headerRight: !navigation.state.params || !navigation.state.params.headerRight ? null : navigation.state.params.headerRight,
  });

  public state: State = {
    places: [],
    filteredPlaces: [],
    searchText: '',
    searchBarVisible: false,
    refreshing: false,
    scrollY: new Animated.Value(0),
    favourites: []
  };

  private _placeService = new PlaceService();
  private _listReference = React.createRef<FlatList<IPlaceHeader>>();
  private _searchBarReference = React.createRef<SearchBar>();


  public render() {
    const searchBarHeight = this.state.scrollY.interpolate({
      inputRange: [0, 50],
      outputRange: [0, 60],
      extrapolate: 'clamp',
    });

    const searchBarOpacity = this.state.scrollY.interpolate({
      inputRange: [0, 50],
      outputRange: [0, 1],
      extrapolate: 'clamp',
    });

    return (
      <SafeAreaView style={this._styles.container} forceInset={{ bottom: 'always', top: 'never' }} >
        <Animated.View
          style={{
            opacity: searchBarOpacity,
            height: searchBarHeight,
          }}
        >
          <SearchBar
            ref={this._searchBarReference}
            placeholder={I18n.t('place_list_searchbar_placeholder')}
            onChangeText={searchText => {
              this.setState({ searchText });
            }}
            value={this.state.searchText}
          />
        </Animated.View>
        <FlatList
          ref={this._listReference}
          refreshing={this.state.refreshing}
          scrollIndicatorInsets={{ right: 1 }}
          onRefresh={async () => {
            try {
              this.setState({ refreshing: true });
              await this._refreshPlaces();
              
            } finally {
              // this.searchFilterFunction(this.state.searchText)
              this.setState({ refreshing: false });
            }
          }}
          contentContainerStyle={{ flexGrow: 1 }}
          ListEmptyComponent={
            <KeyboardAvoidingView behavior="padding">
              <View style={this._styles.emptyContainer}>
                <Text>{I18n.t('no_favourte')}</Text>
              </View>
            </KeyboardAvoidingView>
          }
          data={this.state.filteredPlaces}
          keyExtractor={place => place.id}
          renderItem={({ item }) => (
            <ListItem
              onPress={() => this.props.navigation.navigate('PlaceDetails', { placeId: item.id })}
              leftAvatar={{
                rounded: false,
                source:
                  item.previewImage == null
                    ? assetsImages.no_image
                    : {
                      uri: `${Environment.apiUrl}v1/image/${item.previewImage}`,
                    },
              }}
              chevron={false}
              bottomDivider={true}
              title={item.name}
              subtitle={item.type}
              // rightTitle={I18n.t('place_list_up_to')}
              // rightSubtitle={`${Math.max(item.maxBasicDiscount, item.maxPremiumDiscount)}%`}
              titleStyle={ Typography.listTitleText }
              subtitleStyle={{ color: Colors.darkBrown, fontWeight: 'bold' }}
              // rightTitleStyle={Typography.subHeaderText}
              // rightSubtitleStyle={Typography.headerText}
              titleProps={{ numberOfLines: 10 }}
              subtitleProps={{ numberOfLines: 10 }}
            />
          )}
        />
      </SafeAreaView>
    );
  }

  public async componentWillMount() {
    this._setHeaderRightIcon();
  }



  public async componentDidMount() {
    this.props.navigation.setParams({
      openSearchBar: () => {
        if (this.state.searchBarVisible) {
          Animated.timing(this.state.scrollY, {
            toValue: 0,
            duration: 500,
          }).start();
          this.setState({ searchBarVisible: false }, () => this._setHeaderRightIcon());
        } else {
          this._listReference.current.scrollToOffset({ animated: true, offset: 0 });
          this._searchBarReference.current.focus();
          this.setState({ searchBarVisible: true }, () => this._setHeaderRightIcon());
          Animated.timing(this.state.scrollY, {
            toValue: 50,
            duration: 500,
          }).start();
        }
      },
    });
    await usingAsync(this.props.loading.show(I18n.t('loading')), () => this._refreshPlaces());
    this.props.navigation.addListener('willFocus', async () => usingAsync(this.props.loading.show(I18n.t('loading')), () => this._refreshPlaces()))
  }

  private async _refreshPlaces() {
    await this._getFavourites()
    const response = await this._placeService.getFavouriteAsync(this.state.favourites);

    if (response.isSuccess) {
      this.setState({ places: response.content});
      this.searchFilterFunction()
    }
  }

  private async _getFavourites() {
    logger.log('start');
    try {
      const myFavourites = await AsyncStorage.getItem('@MyFavourites:key');
      if (myFavourites !== null) {
        // We have data!!
        const placeId: string = this.props.navigation.state.params.placeId;
        this.setState({ favourites: JSON.parse(myFavourites) });
        logger.log('favourites: ' + JSON.parse(myFavourites));
      }
    } catch (error) {
      logger.log(error);
    } finally {
      logger.log('fetch');
    }
  };

  private searchFilterFunction = () => {    
    const text = this.state.searchText;
    const newData = this.state.places.filter(item => {      
      const itemData = `${item.name.toUpperCase()}`;
      
       const textData = text.toUpperCase();
        
       return itemData.indexOf(textData) > -1;    
    });
    
    this.setState({ filteredPlaces: newData });  
  };

  private _setHeaderRightIcon = () => {
    if (this.state.searchBarVisible === false) {
      this.props.navigation.setParams({
        headerRight: (
          <TouchableOpacity onPress={() => this.props.navigation.state.params.openSearchBar()}>
            <RightHeaderIcon name="md-search" type="ionicon" />
          </TouchableOpacity>
        ),
      });
    } else {
      this.props.navigation.setParams({
        headerRight: (
          <TouchableOpacity onPress={() => { this.props.navigation.state.params.openSearchBar(), Keyboard.dismiss() }}>
            <RightHeaderIcon name="md-close" type="ionicon" />
          </TouchableOpacity>
        ),
      });
    }
  };

  private _styles = StyleSheet.create({
    container: { ...Views.base },
    emptyContainer: {
      ...Views.base,
      flexDirection: 'row',
      justifyContent: 'space-evenly',
      alignItems: 'center',
    },
  });
}

export default connect(mapStateToProps)(Favourites);
