import { ServiceId } from 'entities/services/lib';
import { DishId } from 'entities/dishes/lib';
import { BookingStatus } from '../constants';

export type BookingId = number;

export interface IBooking {
  id: BookingId;
  userId?: string;
  creationDate: Date;
  startDate: Date;
  endDate: Date;
  statusId: BookingStatus;
  dishes: Record<DishId, number>;
  services: Record<ServiceId, number>;
}
