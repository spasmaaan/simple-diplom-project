import { backendGet, backendPost, backendPut } from 'shared/api';
import { BookingBackendUrl } from 'shared/config';
import { IPaginationResult } from 'shared/types';
import { IBooking, IBookingData, IBookingFreeTime } from '../lib';
import { MOCK_BOOKINGS } from './mock';

const getBookingsPath = (url: string = '') => `${BookingBackendUrl}${url}`;

export const loadBookings = async (): Promise<IPaginationResult<IBooking>> => {
  return MOCK_BOOKINGS; // (await backendGet(null, getBookingsPath())).json();
};

export const loadFreeTime = async (): Promise<IBookingFreeTime[]> => {
  return (await backendGet(null, getBookingsPath('/freeTime'))).json();
};

export const addBooking = async (bookingData: IBookingData): Promise<IBooking> => {
  return (await backendPost(null, getBookingsPath(), bookingData)).json();
};

export const updateBooking = async (booking: Partial<IBooking>): Promise<IBooking> => {
  return (await backendPut(null, getBookingsPath(), booking)).json();
};
