import { backendDelete, backendGet, backendPost, backendPut } from 'shared/api';
import { PhotosBackendUrl } from 'shared/config';
import { IPhoto, IPhotoData, PhotoId } from '../lib';
import { MOCK_PHOTOS } from './mock';

const getPhotosPath = (url: string = '') => `${PhotosBackendUrl}${url}`;

export const loadPhotos = async (): Promise<IPhoto[]> => {
  return [...MOCK_PHOTOS]; // (await backendGet(null, getPhotosPath())).json();
};
export const addPhoto = async (photoData: IPhotoData): Promise<IPhoto> => {
  return (await backendPost(null, getPhotosPath(), photoData)).json();
};
export const editPhoto = async (photo: IPhoto): Promise<IPhoto> => {
  return (await backendPut(null, getPhotosPath(), photo)).json();
};
export const removePhoto = async (photoId: PhotoId): Promise<IPhoto> => {
  return (await backendDelete(null, getPhotosPath(`/${photoId}`))).json();
};
