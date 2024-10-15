import { DishId } from 'entities/dishes/lib';
import { ServiceId } from 'entities/services/lib';
import { BookingId, IBooking, IBookingData } from './types';

export type BookingState = {
  bookingsLoading: boolean;
  bookingsLoaded: boolean;
  bookings: IBooking[];
};

export type BookingAction = {
  load: () => Promise<void>;
  add: (bookingData: IBookingData) => Promise<void>;
  edit: (booking: Partial<IBooking>) => Promise<void>;
  setDish: (bookingId: BookingId, dishId: DishId, count: number) => Promise<void>;
  setService: (bookingId: BookingId, serviceId: ServiceId, count: number) => Promise<void>;
};
