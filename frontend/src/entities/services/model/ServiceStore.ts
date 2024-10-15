import { create } from 'zustand';
import { deleteElementWithId, setElementWithId } from 'shared/helpers';
import { IService, IServiceData, ServiceAction, ServiceId, ServiceState } from '../lib';
import { addService, editService, loadServices, removeService } from '../api';

export const useServiceStoreBase = create<ServiceState & ServiceAction>()((set, get) => ({
  servicesLoading: false,
  servicesLoaded: false,
  services: [],
  load: async () => {
    const { servicesLoaded, servicesLoading } = get();
    if (servicesLoaded || servicesLoading) {
      return;
    }
    set(() => ({ servicesLoaded: false, servicesLoading: true }));
    const services = await loadServices();
    set(() => ({ servicesLoaded: true, servicesLoading: false, services }));
  },
  add: async (serviceData: IServiceData) => {
    if (!get().servicesLoaded) {
      await get().load();
    }
    const { servicesLoading } = get();
    if (servicesLoading) {
      return;
    }
    set(() => ({ servicesLoading: true }));
    const addedDish = await addService(serviceData);
    set((state) => ({ servicesLoading: false, services: [...state.services, addedDish] }));
  },
  edit: async (service: IService) => {
    if (!get().servicesLoaded) {
      await get().load();
    }
    const { servicesLoading, services } = get();
    if (servicesLoading || !services.find((currentDish) => currentDish.id === service.id)) {
      return;
    }
    set(() => ({ servicesLoading: true }));
    const editedDish = await editService(service);
    set((state) => ({
      servicesLoading: false,
      services: setElementWithId(state.services, editedDish),
    }));
  },
  remove: async (serviceId: ServiceId) => {
    if (!get().servicesLoaded) {
      await get().load();
    }
    const { servicesLoading, services } = get();
    if (servicesLoading || !services.find((currentDish) => currentDish.id === serviceId)) {
      return;
    }
    set(() => ({ servicesLoading: true }));
    const removedDish = await removeService(serviceId);
    set((state) => ({
      servicesLoading: false,
      services: deleteElementWithId(state.services, removedDish),
    }));
  },
}));
