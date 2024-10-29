import { IPhotoState, PhotoId } from 'entities/photos';

export interface IPhotosPanelProps {
  className?: string;
  photos: IPhotoState[];
  photoWidth: number;
  onAdd: () => void;
  onEdit: (id: PhotoId) => void;
  onRemove: (id: PhotoId) => void;
}
