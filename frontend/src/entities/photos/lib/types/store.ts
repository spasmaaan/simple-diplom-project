import { IPhoto, IPhotoData, IPhotoState, PhotoId } from './types';

export type PhotosState = {
  photosLoading: boolean;
  photosLoaded: boolean;
  photos: IPhotoState[];
};

export type PhotosAction = {
  load: () => Promise<void>;
  loadImage: (id: PhotoId) => Promise<void>;
  add: (photo: IPhotoData) => Promise<void>;
  edit: (photo: IPhoto) => Promise<void>;
  remove: (photoId: PhotoId) => Promise<void>;
};
