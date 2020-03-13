import { Asset } from 'expo-asset';
import I18n from 'i18n-js';
import React from 'react';
import { Animated, FlatList, Keyboard, KeyboardAvoidingView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import { Image, ListItem, SearchBar } from 'react-native-elements';
import { connect } from 'react-redux';
import { Subject, Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import Environment from '../../../Environment';
import { usingAsync } from '../../../Helpers';
import { RightHeaderIcon, TabIcon } from '../../../Primitives';
import { assetsImages } from '../../../Primitives/Images';
import { Colors, Typography, Views } from '../../../Styles';
import { ICategory } from '../../Models';
import { CategoryService } from '../../Services/CategoryService';
import { mapStateToProps } from './CategoryList.container';
import { Props, State } from './CategoryList.interfaces';

const AnimatedFlatList = Animated.createAnimatedComponent(FlatList);

class CategoryList extends React.Component<Props, State> {
  public static navigationOptions = ({ navigation }) => ({
    title: I18n.t('categories'),
    tabBarIcon: ({ focused }) =>  {
      if (focused===true) {
        return <Image source={require('../../../assets/List-active.png')} style={{width: 24, height: 24}}  />;
      } else {
        return <Image source={require('../../../assets/List.png')} style={{width: 24, height: 24}}  />;
      }
    },
    headerTitleStyle: {
      ...Typography.headerText,
      color: Colors.white
    },
    
    headerStyle: {
      backgroundColor: Colors.headerBackground,
    },
    headerTintColor: Colors.headerText,
    headerRight: !navigation.state.params || !navigation.state.params.headerRight ? null : navigation.state.params.headerRight,
  });

  public state: State = {
    categories: [],
    filtered: [],
    searchText: '',
    searchBarVisible: false,
    refreshing: false,
    scrollY: new Animated.Value(0),
  };

  private _categoryService = new CategoryService();
  private _listReference = React.createRef<FlatList<ICategory>>();
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
            placeholder={ 'Type category name...' }
            onChangeText={searchText => {
              this.setState({ searchText });
              this._onSearch.next(searchText);
            }}
            value={this.state.searchText}
          />
        </Animated.View>
        <FlatList
          ref={this._listReference}
          refreshing={this.state.refreshing}
          // onScroll={Animated.event(
          //   [{ nativeEvent: { contentOffset: { y: this.state.scrollY } } }],
          //   { useNativeDriver: true },
          // )}
          // scrollEventThrottle={16}
          // shouldItemUpdate={(props: { item: any; },nextProps: { item: any; })=>
          //   { 
          //     return props.item!==nextProps.item
          // }}
          onRefresh={async () => {
            try {
              this.setState({ refreshing: true });
              await this._refreshCategories();
            } finally {
              this.setState({ refreshing: false });
            }
          }}
          contentContainerStyle={{ flexGrow: 1 }}
          ListEmptyComponent={
            <KeyboardAvoidingView behavior="padding">
              <View style={this._styles.emptyContainer}>
                <Text>{I18n.t('place_list_empty_list')}</Text>
              </View>
            </KeyboardAvoidingView>
          }
          data={this.state.filtered}
          // keyExtractor={category => category.id.toString()}
          renderItem={({ item }) => (
            <ListItem
              // onPress={() => this.props.navigation.navigate('CategoryDetails', { categoryId: item.id })}
              onPress={() => this.props.navigation.navigate('PlaceList', { categoryId: item.id, categoryTitle: I18n.t(`${item.lang_Key}`) })}
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
              title={I18n.t(`${item.lang_Key}`)}
              titleStyle={{ color: Colors.darkBrown }}
              titleProps={{ numberOfLines: 10 }}
            />
          )}
          keyExtractor={(item: any, index: { toString: () => any; }) => index.toString()}
        />
      </View>
    );
  }
  public async componentWillMount() {
    this._setHeaderRightIcon();

    this._subscription = this._onSearch.pipe(debounceTime(500)).subscribe(async () => {
      try {
        this.setState({ refreshing: true });
        await this._refreshCategories();
      } finally {
        this.setState({ refreshing: false });
      }
    });
  }

  public componentWillUnmount() {
    this._subscription.unsubscribe();
  }

  public cacheImages(images: any[]) {
    return images.map(image => {
      if (typeof image === 'string') {
        return null;
      } else {
        return Asset.fromModule(image).downloadAsync();
      }
    });
  }
  

  public async _loadAssetsAsync() {
    const imageAssets = this.cacheImages([
      require('../../../assets/Heart.png'),
      require('../../../assets/Heart-active.png'),
      require('../../../assets/email.png'),
      require('../../../assets/facebook-logo.png'),
      require('../../../assets/Instagram_logo.png'),
      require('../../../assets/List-active.png'),
      require('../../../assets/List.png'),
      require('../../../assets/love-letter.png'),
      require('../../../assets/no_image.jpg'),
      require('../../../assets/phone.png'),
      require('../../../assets/scroll.png'),
      require('../../../assets/settings.png'),
      require('../../../assets/share.png'),
      require('../../../assets/worldwide.png'),
      require('../../../assets/www.png'),
    ]);

    await Promise.all([...imageAssets]);
  }

  public async componentDidMount() {
    // await localeSet();

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
    await usingAsync(this.props.loading.show(I18n.t('loading')), () => this._refreshCategories());
    this.props.navigation.addListener('willFocus', async () => usingAsync(this.props.loading.show(I18n.t('loading')), () => this._refreshCategories()))
  }

  private async _refreshCategories() {
    // await this._loadAssetsAsync().then(
    //   );
    const response = await this._categoryService.getCategoriesAsync();

    if (response.isSuccess) {
      this.setState({ categories: response.content });
      this.searchFilterFunction()
    }
  }

  private searchFilterFunction = () => {
    const text = this.state.searchText;    
    const newData = this.state.categories.filter(item => {
      const name = I18n.t(`${item.lang_Key}`);      
      const itemData = `${name.toUpperCase()}`;
      const textData = text.toUpperCase();
      return itemData.indexOf(textData) > -1;    
    });
    
    this.setState({ filtered: newData });  
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

export default connect(mapStateToProps)(CategoryList);
