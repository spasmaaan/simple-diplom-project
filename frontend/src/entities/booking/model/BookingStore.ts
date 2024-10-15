import { create } from 'zustand';
import { DishId } from 'entities/dishes/lib';
import { ServiceId } from 'entities/services/lib';
import { getItemById, setElementWithId } from 'shared/helpers';
import { BookingAction, BookingId, BookingState, IBooking, IBookingData } from '../lib';
import { addBooking, loadBookings, updateBooking } from '../api';
import { BookingStatus } from '../lib/constants';

export const useBookingStoreBase = create<BookingState & BookingAction>()((set, get) => ({
  bookingsLoading: false,
  bookingsLoaded: false,
  bookings: [],
  load: async () => {
    const { bookingsLoaded, bookingsLoading } = get();
    if (bookingsLoaded || bookingsLoading) {
      return;
    }
    set(() => ({ bookingsLoaded: false, bookingsLoading: true }));
    const bookings = await loadBookings();
    set(() => ({ bookingsLoaded: true, bookingsLoading: false, bookings }));
  },
  add: async (bookingData: IBookingData) => {
    if (!get().bookingsLoaded) {
      await get().load();
    }
    const { bookingsLoading } = get();
    if (bookingsLoading) {
      return;
    }
    set(() => ({ bookingsLoading: true }));
    const createdBooking = await addBooking(bookingData);
    set((state) => ({
      bookingsLoading: false,
      bookings: [...state.bookings, createdBooking],
    }));
  },
  edit: async (booking: Partial<IBooking>) => {
    if (!get().bookingsLoaded) {
      await get().load();
    }
    const { bookingsLoading, bookings } = get();
    if (bookingsLoading || getItemById(bookings, booking.id)) {
      return;
    }
    set(() => ({ bookingsLoading: true }));
    const editedBooking = await updateBooking(booking);
    set((state) => ({
      bookingsLoading: false,
      bookings: setElementWithId(state.bookings, editedBooking),
    }));
  },
  approve: async (bookingId: BookingId) => {
    if (!get().bookingsLoaded) {
      await get().load();
    }
    const booking = getItemById(get().bookings, bookingId);
    if (!booking || booking.item.statusId === BookingStatus.Approved) {
      return;
    }
    get().edit({
      id: bookingId,
      statusId: BookingStatus.Approved,
    });
  },
  reject: async (bookingId: BookingId) => {
    if (!get().bookingsLoaded) {
      await get().load();
    }
    const booking = getItemById(get().bookings, bookingId);
    if (!booking || booking.item.statusId === BookingStatus.Rejected) {
      return;
    }
    get().edit({
      id: bookingId,
      statusId: BookingStatus.Rejected,
    });
  },
  setDish: async (bookingId: BookingId, dishId: DishId, count: number) => {
    if (!get().bookingsLoaded) {
      await get().load();
    }
    const booking = getItemById(get().bookings, bookingId);
    if (!booking) {
      return;
    }
    get().edit({
      id: bookingId,
      dishes: {
        ...booking.item.dishes,
        [dishId]: count,
      },
    });
  },
  setService: async (bookingId: BookingId, serviceId: ServiceId, count: number) => {
    if (!get().bookingsLoaded) {
      await get().load();
    }
    const booking = getItemById(get().bookings, bookingId);
    if (!booking) {
      return;
    }
    get().edit({
      id: bookingId,
      services: {
        ...booking.item.services,
        [serviceId]: count,
      },
    });
  },
}));
