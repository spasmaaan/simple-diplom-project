import { create } from 'zustand';
import { deleteElementWithId, setElementWithId } from 'shared/helpers';
import { IPhoto, IPhotoData, IPhotoState, PhotoId, PhotosAction, PhotosState } from '../lib';
import { addPhoto, editPhoto, loadImage, loadPhotos, removePhoto } from '../api';

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
    const result = await loadPhotos();
    set(() => ({
      photosLoaded: true,
      photosLoading: false,
      photos: result.items.map((item) => ({ ...item, loading: false, url: null, data: null })),
    }));
  },
  loadImage: async (id: PhotoId) => {
    const { photosLoaded, photosLoading, photos } = get();
    if (!photosLoaded || photosLoading) {
      return;
    }
    const currentPhoto = photos.find((photo) => photo.id === id);
    if (currentPhoto && (currentPhoto.loading || currentPhoto.image != null)) {
      return;
    }
    set((state) => ({
      photos: state.photos.map((photo) => {
        if (photo.id !== id) {
          return photo;
        }
        return {
          ...photo,
          loading: true,
        };
      }),
    }));
    let data = null;
    let url = null;
    let image = '';
    try {
      data = await loadImage(id);
      url = URL.createObjectURL(data);
      image = await data.text();
    } catch (error) {
      console.error(error);
    }
    set((state) => ({
      photos: state.photos.map((photo) => {
        if (photo.id !== id) {
          return photo;
        }
        return {
          ...photo,
          loading: false,
          image,
          url,
          data,
        };
      }),
    }));
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
    set((state) => ({
      photosLoading: false,
      photos: [
        ...state.photos,
        {
          ...addedPhoto,
          loading: false,
          url: null,
          data: null,
        },
      ],
    }));
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
    const removedPhoto = (await removePhoto(photoId)) as IPhotoState;
    set((state) => ({
      photosLoading: false,
      photos: deleteElementWithId(state.photos, removedPhoto),
    }));
  },
}));
