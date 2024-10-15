import { IPhotoData } from 'entities/photos/lib';

export interface IPhotoDialogProps {
  className?: string;
  open: boolean;
  defaults: IPhotoData | undefined;
  title: string;
  okText: string;
  onOk: (data: IPhotoData) => void;
  onClose: () => void;
}
