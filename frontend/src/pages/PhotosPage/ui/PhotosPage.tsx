import cn from 'classnames';
import { IPhoto, PhotoId, usePhotosStore } from 'entities/photos';
import { PhotoDialog } from 'features/PhotoDialog';
import { PhotosPanel } from 'widgets/PhotosPanel';
import { ManagementProvider } from 'widgets/ManagementProvider';
import { useCallback, useEffect } from 'react';
import { IPhotoData } from 'entities/photos/lib';
import { IPhotosPageProps } from '../lib';

import * as styles from './PhotosPage.module.scss';

export const PhotosPage = ({ className }: IPhotosPageProps) => {
  const { add, edit, remove, load, photos, photosLoaded } = usePhotosStore();

  const createDialogOkHandler = useCallback(
    (id: PhotoId | undefined, handleOk: (item: IPhoto | IPhotoData) => void) =>
      (data: IPhotoData) => {
        handleOk({
          ...data,
          ...(id ? { id } : {}),
        });
      },
    []
  );

  useEffect(() => {
    if (!photosLoaded) {
      load();
    }
  }, [load, photosLoaded]);

  return (
    <ManagementProvider
      className={cn(styles.PhotosPage, className)}
      addTitle="Новое изображение"
      editTitle="Заменить изображение"
      removeTitle="Удалить изображение?"
      renderDialog={({ open, id, title, okText, onOk, onClose }) => (
        <PhotoDialog
          open={open}
          defaults={photos.find((photo) => photo.id === id)}
          title={title}
          okText={okText}
          onOk={createDialogOkHandler(id, onOk)}
          onClose={onClose}
        />
      )}
      renderContent={(onAdd, onEdit, onRemove) =>
        photosLoaded ? (
          <PhotosPanel
            photoWidth={200}
            photos={photos}
            onAdd={onAdd}
            onEdit={onEdit}
            onRemove={onRemove}
          />
        ) : null
      }
      add={add}
      edit={edit}
      remove={remove}
    />
  );
};
