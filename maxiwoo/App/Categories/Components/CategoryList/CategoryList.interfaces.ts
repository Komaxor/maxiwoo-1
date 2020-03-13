import { Animated } from 'react-native';
import { NavigationScreenProps } from 'react-navigation';
import { ICategory } from '../../Models';
import { mapStateToProps } from './CategoryList.container';

export interface ICategoryListProps extends NavigationScreenProps {
  headerRight: JSX.Element;
}

export type ICategoryListStore = ReturnType<typeof mapStateToProps>;

export interface ICategoryListState {
  categories: ICategory[];
  filtered: ICategory[];
  searchText: string;
  searchBarVisible: boolean;
  refreshing: boolean;
  scrollY: Animated.Value;
}

export type Props = ICategoryListProps & ICategoryListStore;
export type State = ICategoryListState;