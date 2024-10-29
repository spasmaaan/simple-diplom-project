import { IService, IServiceData, IServiceState, ServiceId } from './types';

export type ServiceState = {
  servicesLoading: boolean;
  servicesLoaded: boolean;
  services: IServiceState[];
};

export type ServiceAction = {
  load: () => Promise<void>;
  loadImage: (id: ServiceId) => Promise<void>;
  add: (serviceData: IServiceData) => Promise<void>;
  edit: (service: IService) => Promise<void>;
  remove: (serviceId: ServiceId) => Promise<void>;
};
