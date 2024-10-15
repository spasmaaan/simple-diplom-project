import { backendDelete, backendGet, backendPost, backendPut } from 'shared/api';
import { ServicesBackendUrl } from 'shared/config';
import { IService, IServiceData, ServiceId } from '../lib';
import { MOCK_SERVICES } from './mock';

const getServicesPath = (url: string = '') => `${ServicesBackendUrl}${url}`;

export const loadServices = async (): Promise<IService[]> => {
  return MOCK_SERVICES; // (await backendGet(null, getServicesPath())).json();
};
export const addService = async (serviceData: IServiceData): Promise<IService> => {
  return (await backendPost(null, getServicesPath(), serviceData)).json();
};
export const editService = async (service: IService): Promise<IService> => {
  return (await backendPut(null, getServicesPath(), service)).json();
};
export const removeService = async (serviceId: ServiceId): Promise<IService> => {
  return (await backendDelete(null, getServicesPath(`/${serviceId}`))).json();
};
