export interface IPhotoDialogProps {
  className?: string;
  open: boolean;
  title: string;
  okText: string;
  onOk: (data: IPhotoDialogData) => void;
  onClose: () => void;
}

export interface IPhotoDialogData {
  image: string;
}
