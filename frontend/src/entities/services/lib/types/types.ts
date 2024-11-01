import { IImageItem } from 'shared/types';

export type ServiceId = number;

export interface IServiceData {
  name: string;
  description: string;
  previewImage: string;
  price: number;
  maxCount: number;
}

export interface IService extends IServiceData {
  id: ServiceId;
}

export interface IServiceState extends IService, IImageItem {}
