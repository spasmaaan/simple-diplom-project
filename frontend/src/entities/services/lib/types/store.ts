import { IService, IServiceData, ServiceId } from './types';

export type ServiceState = {
  servicesLoading: boolean;
  servicesLoaded: boolean;
  services: IService[];
};

export type ServiceAction = {
  load: () => Promise<void>;
  add: (serviceData: IServiceData) => Promise<void>;
  edit: (service: IService) => Promise<void>;
  remove: (serviceId: ServiceId) => Promise<void>;
};
