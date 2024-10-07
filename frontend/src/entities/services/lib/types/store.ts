import { IService, ServiceId } from './types';

export type ServiceState = {
  servicesLoading: boolean;
  servicesLoaded: boolean;
  services: IService[];
};

export type ServiceAction = {
  load: () => Promise<void>;
  add: (service: IService) => Promise<void>;
  edit: (service: IService) => Promise<void>;
  remove: (serviceId: ServiceId) => Promise<void>;
};
