import { create } from 'zustand';
import { deleteElementWithId, setElementWithId } from 'shared/helpers';
import { IPhoto, IPhotoData, PhotoId, PhotosAction, PhotosState } from '../lib';
import { addPhoto, editPhoto, loadPhotos, removePhoto } from '../api';

export const usePhotosStoreBase = create<PhotosState & PhotosAction>()((set, get) => ({
  photosLoading: false,
  photosLoaded: false,
  photos: [],
  load: async () => {
    const { photosLoaded, photosLoading } = get();
    if (photosLoaded || photosLoading) {
      return;
    }
    set(() => ({ photosLoaded: false, photosLoading: true }));
    const photos = await loadPhotos();
    set(() => ({ photosLoaded: true, photosLoading: false, photos }));
  },
  add: async (photoData: IPhotoData) => {
    if (!get().photosLoaded) {
      await get().load();
    }
    const { photosLoading } = get();
    if (photosLoading) {
      return;
    }
    set(() => ({ photosLoading: true }));
    const addedPhoto = await addPhoto(photoData);
    set((state) => ({ photosLoading: false, photos: [...state.photos, addedPhoto] }));
  },
  edit: async (photo: IPhoto) => {
    if (!get().photosLoaded) {
      await get().load();
    }
    const { photosLoading, photos } = get();
    if (photosLoading || !photos.find((currentDish) => currentDish.id === photo.id)) {
      return;
    }
    set(() => ({ photosLoading: true }));
    const editedPhoto = await editPhoto(photo);
    set((state) => ({ photosLoading: false, photos: setElementWithId(state.photos, editedPhoto) }));
  },
  remove: async (photoId: PhotoId) => {
    if (!get().photosLoaded) {
      await get().load();
    }
    const { photosLoading, photos } = get();
    if (photosLoading || !photos.find((currentDish) => currentDish.id === photoId)) {
      return;
    }
    set(() => ({ photosLoading: true }));
    const removedPhoto = await removePhoto(photoId);
    set((state) => ({
      photosLoading: false,
      photos: deleteElementWithId(state.photos, removedPhoto),
    }));
  },
}));
