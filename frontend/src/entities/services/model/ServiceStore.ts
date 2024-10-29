import { create } from 'zustand';
import { deleteElementWithId, setElementWithId } from 'shared/helpers';
import {
  IService,
  IServiceData,
  IServiceState,
  ServiceAction,
  ServiceId,
  ServiceState,
} from '../lib';
import { addService, editService, loadServiceImage, loadServices, removeService } from '../api';

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
    const result = await loadServices();
    set(() => ({
      servicesLoaded: true,
      servicesLoading: false,
      services: result.items.map((item) => ({ ...item, loading: false, url: null, data: null })),
    }));
  },
  loadImage: async (id: ServiceId) => {
    const { servicesLoaded, servicesLoading, services } = get();
    if (!servicesLoaded || servicesLoading) {
      return;
    }
    const currentService = services.find((service) => service.id === id);
    if (currentService && (currentService.loading || currentService.previewImage != null)) {
      return;
    }
    set((state) => ({
      services: state.services.map((service) => {
        if (service.id !== id) {
          return service;
        }
        return {
          ...service,
          loading: true,
        };
      }),
    }));
    let data = null;
    let url = null;
    let previewImage = '';
    try {
      data = await loadServiceImage(id);
      url = URL.createObjectURL(data);
      previewImage = await data.text();
    } catch (error) {
      console.error(error);
    }
    set((state) => ({
      services: state.services.map((service) => {
        if (service.id !== id) {
          return service;
        }
        return {
          ...service,
          loading: false,
          previewImage,
          url,
          data,
        };
      }),
    }));
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
    set((state) => ({
      servicesLoading: false,
      services: [...state.services, { ...addedDish, loading: false, data: null, url: null }],
    }));
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
      services: deleteElementWithId(state.services, removedDish as unknown as IServiceState),
    }));
  },
}));
