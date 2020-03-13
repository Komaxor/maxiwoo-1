import { Animated } from 'react-native';
import { NavigationScreenProps } from 'react-navigation';
import { IPlaceHeader } from '../../Models';
import { mapStateToProps } from './PlaceList.container';

export interface IPlaceListProps extends NavigationScreenProps {
  headerRight: JSX.Element;
}

export type IPlaceListStore = ReturnType<typeof mapStateToProps>;

export interface IPlaceListState {
  places: IPlaceHeader[];
  filteredPlaces: IPlaceHeader[];
  searchText: string;
  searchBarVisible: boolean;
  refreshing: boolean;
  scrollY: Animated.Value;
  error: undefined;
}

export type Props = IPlaceListProps & IPlaceListStore;
export type State = IPlaceListState;
