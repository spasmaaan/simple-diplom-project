import cn from 'classnames';
import { Input, Modal, Rate, Space } from 'antd';
import { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { IReviewData } from 'entities/reviews';
import TextArea from 'antd/es/input/TextArea';
import { IReviewDialogProps, DialogWidth } from '../lib';

import * as styles from './ReviewDialog.module.scss';

export const ReviewDialog = ({
  className,
  open,
  defaults,
  title,
  okText,
  onClose,
  onOk,
}: IReviewDialogProps) => {
  const [message, setMessage] = useState<string>('');
  const [rating, setRating] = useState<number | undefined>(undefined);

  const onMessageChanged = useCallback((event?: ChangeEvent<HTMLTextAreaElement>) => {
    setMessage(event?.target.value || '');
  }, []);
  const onRatingChanged = useCallback(
    (value: number | undefined) => {
      setRating(rating !== value ? value : undefined);
    },
    [rating]
  );

  const closeDialog = useCallback(() => {
    setMessage('');
    setRating(undefined);

    onClose();
  }, [onClose]);

  const handleOk = useCallback(() => {
    const { creationDate, me, shortName, fullName } = defaults || {
      creationDate: new Date(Date.now()),
      shortName: '',
      fullName: '',
      me: true,
    };
    onOk({
      creationDate,
      me,
      shortName,
      fullName,
      message,
      rating,
    });
  }, [defaults, message, rating, onOk]);

  useEffect(() => {
    setMessage(defaults?.message || '');
    setRating(defaults?.rating);
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
        <TextArea
          placeholder="Отзыв"
          rows={5}
          maxLength={600}
          value={message}
          onChange={onMessageChanged}
        />
        <Rate value={rating} onChange={onRatingChanged} />
      </Space>
    </Modal>
  );
};
