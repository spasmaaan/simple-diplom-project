import { IPhoto, PhotoId } from 'entities/photos';

export interface IPhotosPanelProps {
  className?: string;
  photos: IPhoto[];
  photoWidth: number;
  onAdd: () => void;
  onEdit: (id: PhotoId) => void;
  onRemove: (id: PhotoId) => void;
}
