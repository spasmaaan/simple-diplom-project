import { ServiceId } from 'entities/services/lib';
import { DishId } from 'entities/dishes/lib';
import { BookingStatus } from '../constants';

export type BookingId = number;

export interface IBookingData {
  userId?: string;
  creationDate: string;
  startDate: string;
  endDate: string;
  statusId: BookingStatus;
  dishes: Record<DishId, number>;
  services: Record<ServiceId, number>;
}

export interface IBooking extends IBookingData {
  id: BookingId;
}
