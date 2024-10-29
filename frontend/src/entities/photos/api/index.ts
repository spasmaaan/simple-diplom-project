import { backendDelete, backendGet, backendPost, backendPut } from 'shared/api';
import { PhotosBackendUrl } from 'shared/config';
import { IPaginationResult } from 'shared/types';
import { IPhoto, IPhotoData, PhotoId } from '../lib';

const getPhotosPath = (url: string = '') => `${PhotosBackendUrl}${url}`;

export const loadPhotos = async (): Promise<IPaginationResult<IPhoto>> => {
  return (await backendGet(null, getPhotosPath())).json();
};
export const loadImage = async (id: PhotoId): Promise<Blob> => {
  return (await backendGet(null, getPhotosPath(`/${id}`))).blob();
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
