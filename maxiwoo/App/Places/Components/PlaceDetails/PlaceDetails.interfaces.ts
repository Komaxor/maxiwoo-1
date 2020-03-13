import { Animated } from 'react-native';
import { NavigationScreenProps } from 'react-navigation';
import { IPlaceDetails } from '../../Models';
import { mapStateToProps } from './PlaceDetails.container';

export interface IPlaceDetailsProps extends NavigationScreenProps<{ placeId: string, category: string}> {}

export type IPlaceDetailsStore = ReturnType<typeof mapStateToProps>;

export interface IPlaceDetailsState {
  place: IPlaceDetails;
  scrollY: Animated.Value;
  isFavorite: boolean;
  favourites: string[];
}

export type Props = IPlaceDetailsProps & IPlaceDetailsStore;
export type State = IPlaceDetailsState;
