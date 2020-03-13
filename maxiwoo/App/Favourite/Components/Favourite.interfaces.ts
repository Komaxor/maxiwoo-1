import { Animated } from 'react-native';
import { NavigationScreenProps } from 'react-navigation';
import { mapStateToProps } from '../../Categories/Components/CategoryList/CategoryList.container';
import { IPlaceHeader } from '../../Places/Models/IPlaceHeader';

export interface IFavouriteProps extends NavigationScreenProps {
  headerRight: JSX.Element;
}

export type IFavouriteStore = ReturnType<typeof mapStateToProps>;

export interface IFavouriteState {
  places: IPlaceHeader[];
  filteredPlaces: IPlaceHeader[];
  searchText: string;
  searchBarVisible: boolean;
  refreshing: boolean;
  scrollY: Animated.Value;
  favourites: string[];
}

export type Props = IFavouriteProps & IFavouriteStore;
export type State = IFavouriteState;
