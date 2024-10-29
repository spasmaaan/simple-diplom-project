import { IImageItem } from 'shared/types';

export type PhotoId = number;

export interface IPhotoData {
  image?: string;
}

export interface IPhoto extends IPhotoData {
  id: PhotoId;
}

export interface IPhotoState extends IPhoto, IImageItem {}
