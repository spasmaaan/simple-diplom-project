import { backendGet, backendPost, backendPut } from 'shared/api';
import { BookingBackendUrl } from 'shared/config';
import { BookingId, IBooking, IBookingData } from '../lib';
import { MOCK_BOOKINGS } from './mock';

const getBookingsPath = (url: string = '') => `${BookingBackendUrl}${url}`;

export const loadBookings = async (): Promise<IBooking[]> => {
  return MOCK_BOOKINGS; // (await backendGet(null, getBookingsPath())).json();
};

export const addBooking = async (bookingData: IBookingData): Promise<IBooking> => {
  return (await backendPost(null, getBookingsPath(), bookingData)).json();
};

export const updateBooking = async (booking: Partial<IBooking>): Promise<IBooking> => {
  return (await backendPut(null, getBookingsPath(), booking)).json();
};
