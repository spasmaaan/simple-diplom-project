import cn from 'classnames';
import { Input, Modal, Space } from 'antd';
import { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { IFaqDialogProps, DialogWidth } from '../lib';

import * as styles from './FaqDialog.module.scss';

export const FaqDialog = ({
  className,
  open,
  defaults,
  title,
  okText,
  onClose,
  onOk,
}: IFaqDialogProps) => {
  const [question, setQuestion] = useState<string>('');
  const [answer, setAnswer] = useState<string>('');

  const onQuestionChanged = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setQuestion(event?.target.value || '');
  }, []);
  const onAnswerChanged = useCallback((event?: ChangeEvent<HTMLInputElement>) => {
    setAnswer(event?.target.value || '');
  }, []);

  const closeDialog = useCallback(() => {
    setQuestion('');
    setAnswer('');
    onClose();
  }, [onClose]);

  const handleOk = useCallback(() => {
    onOk({
      answer,
      question,
    });
  }, [answer, question, onOk]);

  useEffect(() => {
    setQuestion(defaults?.question || '');
    setAnswer(defaults?.answer || '');
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
        <Input placeholder="Вопрос" value={question} onChange={onQuestionChanged} />
        <Input placeholder="Ответ" value={answer} onChange={onAnswerChanged} />
      </Space>
    </Modal>
  );
};
