import cn from 'classnames';
import { Modal, Space } from 'antd';
import { useCallback, useEffect, useState } from 'react';
import { UploadImageCard } from 'shared/components/UploadImageCard';
import { IPhotoDialogProps, DialogWidth } from '../lib';

import * as styles from './PhotoDialog.module.scss';

export const PhotoDialog = ({
  className,
  okText,
  open,
  title,
  defaults,
  onClose,
  onOk,
}: IPhotoDialogProps) => {
  const [image, setImage] = useState<string>('');

  const onImageUploaded = useCallback((imageBase64: string) => {
    setImage(imageBase64);
  }, []);

  const closeDialog = useCallback(() => {
    setImage('');
    onClose();
  }, [onClose]);

  const handleOk = useCallback(() => {
    onOk({
      image,
    });
  }, [image, onOk]);

  useEffect(() => {
    setImage(defaults?.image || '');
  }, [defaults]);

  return (
    <Modal
      className={className}
      open={open}
      title={title}
      okText={okText}
      cancelText="Отмена"
      width={DialogWidth}
      onOk={handleOk}
      onCancel={closeDialog}
      onClose={closeDialog}
    >
      <Space className={styles.Content} direction="vertical" size="middle">
        <UploadImageCard
          text="Изображение"
          previewLoadedImage={image}
          onImageEndLoading={onImageUploaded}
        />
      </Space>
    </Modal>
  );
};
