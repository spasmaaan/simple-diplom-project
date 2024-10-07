export type ServiceId = number;

export interface IService {
  id: ServiceId;
  name: string;
  description: string;
  previewImage: string;
  price: number;
}
