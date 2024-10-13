import { IPhoto, IPhotoData, PhotoId } from './types';

export type PhotosState = {
  photosLoading: boolean;
  photosLoaded: boolean;
  photos: IPhoto[];
};

export type PhotosAction = {
  load: () => Promise<void>;
  add: (photoData: IPhotoData) => Promise<void>;
  edit: (photo: IPhoto) => Promise<void>;
  remove: (photoId: PhotoId) => Promise<void>;
};
