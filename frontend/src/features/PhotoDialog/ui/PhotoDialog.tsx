import cn from 'classnames';
import { Modal, Space } from 'antd';
import { useCallback, useState } from 'react';
import { UploadImageCard } from 'shared/components/UploadImageCard';
import { IPhotoDialogProps, PhotoDialogWidth } from '../lib';

import * as styles from './PhotoDialog.module.scss';

export const PhotoDialog = ({
  className,
  okText,
  onClose,
  onOk,
  open,
  title,
}: IPhotoDialogProps) => {
  const [uploadingImageBase64, setUploadingImageBase64] = useState<string>('');

  const onImageUploaded = useCallback((imageBase64: string) => {
    setUploadingImageBase64(imageBase64);
  }, []);

  const closePhotoDialog = useCallback(() => {
    setUploadingImageBase64('');
    onClose();
  }, [onClose]);

  const handleOk = useCallback(() => {
    onOk({
      image: uploadingImageBase64,
    });
  }, [uploadingImageBase64, onOk]);

  return (
    <Modal
      className={className}
      open={open}
      title={title}
      okText={okText}
      cancelText="Отмена"
      width={PhotoDialogWidth}
      onOk={handleOk}
      onCancel={closePhotoDialog}
      onClose={closePhotoDialog}
    >
      <Space className={styles.Content} direction="vertical" size="middle">
        <UploadImageCard
          text="Изображение"
          previewLoadedImage={uploadingImageBase64}
          onImageEndLoading={onImageUploaded}
        />
      </Space>
    </Modal>
  );
};
