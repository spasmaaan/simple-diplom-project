import { DishId } from 'entities/dishes/lib';
import { ServiceId } from 'entities/services/lib';
import { IBooking, IBookingData, IBookingFreeTime } from './types';

export type BookingState = {
  bookingsLoading: boolean;
  bookingsLoaded: boolean;
  bookings: IBooking[];
  freeTimeLoading: boolean;
  freeTimeLoaded: boolean;
  freeTime: IBookingFreeTime[];
  newBooking: Partial<IBooking>;
};

export type BookingAction = {
  load: () => Promise<void>;
  loadFreeTime: () => Promise<void>;
  add: (bookingData: IBookingData) => Promise<void>;
  edit: (booking: Partial<IBooking>) => Promise<void>;
  clearNewBooking: () => void;
  setDish: (dishId: DishId, count: number) => void;
  setService: (serviceId: ServiceId, count: number) => void;
};
