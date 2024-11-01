import { create } from 'zustand';
import { DishId } from 'entities/dishes/lib';
import { ServiceId } from 'entities/services/lib';
import { getItemById, setElementWithId } from 'shared/helpers';
import { BookingAction, BookingId, BookingState, IBooking, IBookingData } from '../lib';
import { addBooking, loadBookings, loadFreeTime, updateBooking } from '../api';
import { BookingStatus } from '../lib/constants';

export const useBookingStoreBase = create<BookingState & BookingAction>()((set, get) => ({
  bookingsLoading: false,
  bookingsLoaded: false,
  bookings: [],
  freeTimeLoaded: false,
  freeTimeLoading: false,
  freeTime: [],
  newBooking: {},
  load: async () => {
    const { bookingsLoaded, bookingsLoading } = get();
    if (bookingsLoaded || bookingsLoading) {
      return;
    }
    set(() => ({ bookingsLoaded: false, bookingsLoading: true }));
    const result = await loadBookings();
    set(() => ({ bookingsLoaded: true, bookingsLoading: false, bookings: result.items }));
  },
  loadFreeTime: async () => {
    const { freeTimeLoaded, freeTimeLoading } = get();
    if (freeTimeLoaded || freeTimeLoading) {
      return;
    }
    set(() => ({ freeTimeLoaded: false, freeTimeLoading: true }));
    const freeTime = await loadFreeTime();
    set(() => ({ freeTimeLoaded: true, freeTimeLoading: false, freeTime }));
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
  clearNewBooking: () => {
    set(() => ({
      newBooking: {},
    }));
  },
  setDish: (dishId: DishId, count: number) => {
    set((state) => {
      const dishes = state.newBooking?.dishes || {};
      if (count > 0) {
        dishes[dishId] = count;
      } else {
        delete dishes[dishId];
      }
      return {
        newBooking: {
          ...state.newBooking,
          dishes,
        },
      };
    });
  },
  setService: (serviceId: ServiceId, count: number) => {
    set((state) => {
      const services = state.newBooking?.services || {};
      if (count > 0) {
        services[serviceId] = count;
      } else {
        delete services[serviceId];
      }
      return {
        newBooking: {
          ...state.newBooking,
          services,
        },
      };
    });
  },
}));
