import cn from 'classnames';
import { FaqsPanel } from 'widgets/FaqsPanel';
import { FaqId, IFaq, useFaqsStore } from 'entities/faqs';
import { FaqDialog } from 'features/FaqDialog';
import { ManagementProvider } from 'widgets/ManagementProvider';
import { useCallback, useEffect } from 'react';
import { IFaqData } from 'entities/faqs/lib';
import { IFaqPageProps } from '../lib';

import * as styles from './FaqPage.module.scss';

export const FaqPage = ({ className }: IFaqPageProps) => {
  const { faqs, faqsLoaded, add, edit, remove, load } = useFaqsStore();

  const createDialogOkHandler = useCallback(
    (id: FaqId | undefined, handleOk: (item: IFaq | IFaqData) => void) => (data: IFaqData) => {
      handleOk({
        ...data,
        ...(id ? { id } : {}),
      });
    },
    []
  );

  useEffect(() => {
    if (!faqsLoaded) {
      load();
    }
  }, [load, faqsLoaded]);

  return (
    <ManagementProvider
      className={cn(styles.FaqPage, className)}
      addTitle="Новый вопрос-ответ"
      editTitle="Изменить вопрос-ответ"
      removeTitle="Удалить вопрос-ответ?"
      renderDialog={({ open, id, title, okText, onOk, onClose }) => (
        <FaqDialog
          open={open}
          title={title}
          defaults={faqs.find((faq) => faq.id === id)}
          okText={okText}
          onOk={createDialogOkHandler(id, onOk)}
          onClose={onClose}
        />
      )}
      renderContent={(onAdd, onEdit, onRemove) =>
        faqsLoaded ? (
          <FaqsPanel faqs={faqs} onAdd={onAdd} onEdit={onEdit} onRemove={onRemove} />
        ) : null
      }
      add={add}
      edit={edit}
      remove={remove}
    />
  );
};
