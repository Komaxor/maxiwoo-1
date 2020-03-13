import { Ionicons } from '@expo/vector-icons';
import {
  createAppContainer,
  createBottomTabNavigator,
  createMaterialTopTabNavigator,
  createStackNavigator,
  createSwitchNavigator,
} from 'react-navigation';
import CategoryList from '../Categories/Components/CategoryList/CategoryList';
import Favourite from '../Favourite/Components/Favourite';
import PlaceDetails from '../Places/Components/PlaceDetails/PlaceDetails';
import PlaceList from '../Places/Components/PlaceList/PlaceList';
import PrivacyPolicy from '../Policies/Components/PrivacyPolicy/PrivacyPolicy';
import TermsOfUse from '../Policies/Components/TermsOfUse/TermsOfUse';
import SelectLanguage from '../Settings/Components/SelectLanguage/SelectLanguage';
import Settings from '../Settings/Components/Settings';
import { Colors } from '../Styles';

const categoriesStack = createStackNavigator(
  {
    CategoryList: {
      screen: CategoryList,
    },
  },
  { headerLayoutPreset: 'center' }
);

const policiesNavigator = createMaterialTopTabNavigator(
  {
    TermsOfUse: {
      screen: TermsOfUse,
      navigationOptions: () => ({
        ...TermsOfUse.navigationOptions(),
      }),
    },
    PrivacyPolicy: {
      screen: PrivacyPolicy,
      navigationOptions: () => ({
        ...PrivacyPolicy.navigationOptions(),
      }),
    },
  },
  {
    tabBarOptions: {
      activeTintColor: Colors.darkBrown,
      inactiveTintColor: Colors.unselected,
      showLabel: true,
      upperCaseLabel: false,
      style: {
        backgroundColor: Colors.background,
      },
      indicatorStyle: {
        backgroundColor: Colors.background,
      },
    },
    initialRouteName: 'TermsOfUse',
    swipeEnabled: false,
  }
);

const settingsStack = createStackNavigator(
  {
    Settings: {
      screen: Settings,
      navigationOptions: () => ({
        headerBackTitle: null
      }),
    },
    Policies: {
      screen: policiesNavigator,
      navigationOptions: () => ({
        ...TermsOfUse.policiesHeaderStyle(),
      }),
    }
  },
  { headerLayoutPreset: 'center' }
);

const favouritesStack = createStackNavigator(
  {
    Favourite: {
      screen: Favourite,
      navigationOptions: () => ({
        headerBackTitle: null
      }),
    },
  },
  { headerLayoutPreset: 'center' }
);

// placesStack.navigationOptions = navigation => ({
//   ...PlaceList.navigationOptions(navigation),
// });

categoriesStack.navigationOptions = (navigation: { navigation: any; }) => ({
  ...CategoryList.navigationOptions(navigation),
});

favouritesStack.navigationOptions = (navigation: { navigation: any; }) => ({
  ...Favourite.navigationOptions(navigation),
});

const tabNavigator = createBottomTabNavigator(
  {
    Settings: {
      screen: settingsStack,
      navigationOptions: () => ({
        ...Settings.navigationOptions(),
      }),
    },
    Categories: {
      screen: categoriesStack,
    },
    Favourite: {
      screen: favouritesStack,
    },
  },
  {
    tabBarOptions: {
      activeTintColor: Colors.darkBrown,
      inactiveTintColor: Colors.white,
      activeBackgroundColor: Colors.primary,
      inactiveBackgroundColor: Colors.primary,
      showLabel: false,
      style: {
        backgroundColor: Colors.primary,
      },
      indicatorStyle: {
        backgroundColor: Colors.primary,
      },
    },
    initialRouteName: 'Categories',
  }
);

// we have to hide the tab navigation, the solution is to put every child
// on the same level as a tab navigator inside a stack navigator
// https://reactnavigation.org/docs/en/navigation-options-resolution.html#a-tab-navigator-contains-a-stack-and-you-want-to-hide-the-tab-bar-on-specific-screens
// second option from the previous link
const appStackNavigator = createStackNavigator(
  {
    Tabs: {
      screen: tabNavigator,
      navigationOptions: () => ({
        header: null,
      }),
    },
    CategoryList: {
      screen: CategoryList,
    },
    PlaceList: {
      screen: PlaceList,
      navigationOptions: () => ({
        headerBackTitle: null
      }),
    },
    Favourite: {
      screen: Favourite,
      navigationOptions: () => ({
        headerBackTitle: null
      }),
    },
    PlaceDetails: {
      screen: PlaceDetails,
      navigationOptions: () => ({
        headerBackTitle: null
      }),
    },
    Policies: {
      screen: policiesNavigator,
      navigationOptions: () => ({
        ...TermsOfUse.policiesHeaderStyle(),
      }),
    },
    SelectLanguage: {
      screen: SelectLanguage,
    }
  },
  { headerLayoutPreset: 'center', initialRouteName: 'Tabs' }
);

const RootNavigator = createSwitchNavigator(
  {
    App: appStackNavigator
  },
  {
    initialRouteName: 'App',
  }
);

export const AppContainer = createAppContainer(RootNavigator);
