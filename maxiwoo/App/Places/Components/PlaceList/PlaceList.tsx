import { Ionicons } from '@expo/vector-icons';
import I18n from 'i18n-js';
import React from 'react';
import { Alert, Animated, FlatList, Keyboard, KeyboardAvoidingView, Platform, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { ListItem, SearchBar } from 'react-native-elements';
import { HeaderBackButton } from 'react-navigation'
import { connect } from 'react-redux';
import { Subject, Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import Environment from '../../../Environment';
import { usingAsync } from '../../../Helpers';
import { RightHeaderIcon, TabIcon } from '../../../Primitives';
import { assetsImages } from '../../../Primitives/Images';
import { Colors, Typography, Views } from '../../../Styles';
import { IPlaceHeader } from '../../Models';
import { PlaceService } from '../../Services/PlaceService';
import { mapStateToProps } from './PlaceList.container';
import { Props, State } from './PlaceList.interfaces';

const AnimatedFlatList = Animated.createAnimatedComponent(FlatList);

class PlaceList extends React.Component<Props, State> {
  public static navigationOptions = ({ navigation }) => ({
    title: `${navigation.state.params.categoryTitle}`,
    headerTitle: ({ style, children : title }) => {
      return (
        <Text style={style} numberOfLines={2}>{title}</Text>
      )
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

  public state: State = {
    places: [],
    filteredPlaces: [],
    searchText: '',
    searchBarVisible: false,
    refreshing: false,
    scrollY: new Animated.Value(0),
    error: null,
  };

  private _placeService = new PlaceService();
  private _listReference = React.createRef<FlatList<IPlaceHeader>>();
  private _searchBarReference = React.createRef<SearchBar>();

  private _subscription: Subscription;
  private readonly _onSearch: Subject<string> = new Subject<string>();

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

    if (this.state.error) {
      Alert.alert(I18n.t('server_error'), I18n.t('server_error_message'));
    }
    return (
      <View style={this._styles.container}>
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
              this._onSearch.next();
            }}
            value={this.state.searchText}
          />
        </Animated.View>
        <FlatList
          ref={this._listReference}
          refreshing={this.state.refreshing}
          scrollIndicatorInsets={{ right: 1 }}
          // shouldItemUpdate={(props: { item: any; },nextProps: { item: any; })=>
          //   { 
          //     return props.item!==nextProps.item
          // }}
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
                <Text>{I18n.t('no_service_provider')}</Text>
              </View>
            </KeyboardAvoidingView>
          }
          data={this.state.filteredPlaces}
          keyExtractor={place => place.id}
          renderItem={({ item }) => (
            <ListItem
              onPress={() => this.props.navigation.navigate('PlaceDetails', { placeId: item.id, category: `${this.props.navigation.state.params.categoryTitle}`})}
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
      </View>
    );
  }

  public async componentWillMount() {
    this._setHeaderRightIcon();

    this._subscription = this._onSearch.pipe(debounceTime(500)).subscribe(async () => {
      try {
        this.setState({ refreshing: true });
        await this._refreshPlaces();
        this.searchFilterFunction();
      } finally {
        this.setState({ refreshing: false });
      }
    });
  }

  public componentWillUnmount() {
    this._subscription.unsubscribe();
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
    await usingAsync(this.props.loading.show(I18n.t('place_list_downloading')), () => this._refreshPlaces());
    this.props.navigation.addListener('willFocus', async () => usingAsync(this.props.loading.show(I18n.t('loading')), () => this._refreshPlaces()))
  }

  private async _refreshPlaces() {
    const response = await this._placeService.getPlacesByCategoryAsync(this.props.navigation.state.params.categoryId);

    if (response.isSuccess) {
      this.setState({ places: response.content});
      this.searchFilterFunction();
    }
  }

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

export default connect(mapStateToProps)(PlaceList);
