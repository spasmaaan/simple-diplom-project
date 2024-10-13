import { Modal } from 'antd';
import { useCallback, useState } from 'react';
import {
  IManagementProviderDialogState,
  IManagementProviderItem,
  IManagementProviderProps,
} from '../lib';

export const ManagementProvider = <
  TId,
  TItemData extends object,
  TItem extends IManagementProviderItem<TId> & TItemData,
>({
  className,
  addTitle,
  editTitle,
  removeTitle,
  renderDialog,
  renderContent,
  add,
  edit,
  remove,
}: IManagementProviderProps<TId, TItemData, TItem>) => {
  const [modal, contextHolder] = Modal.useModal();

  const [dialogData, setDialogData] = useState<
    IManagementProviderDialogState<TId, TItemData, TItem>
  >({
    okText: '',
    open: false,
    title: '',
    onOk: () => {},
    onClose: () => {},
  });

  const onClose = useCallback(() => {
    setDialogData({
      open: false,
      okText: '',
      onOk: () => {},
      onClose: () => {},
      title: '',
    });
  }, []);

  const onAdd = useCallback(() => {
    setDialogData({
      open: true,
      okText: 'Добавить',
      title: addTitle,
      onOk: (item) => {
        add(item as TItemData);
      },
      onClose,
    });
  }, [addTitle, add, onClose]);
  const onEdit = useCallback(
    (id: TId) => {
      setDialogData({
        id,
        open: true,
        okText: 'Обновить',
        title: editTitle,
        onOk: (item) => {
          edit(item as TItem);
        },
        onClose,
      });
    },
    [editTitle, edit, onClose]
  );
  const onRemove = useCallback(
    (id: TId) => {
      void modal.confirm({
        title: removeTitle,
        okText: 'Да',
        cancelText: 'Нет',
        onOk: () => {
          remove(id);
        },
      });
    },
    [modal, removeTitle, remove]
  );

  return (
    <div className={className}>
      {renderDialog(dialogData)}
      {contextHolder}
      {renderContent(onAdd, onEdit, onRemove)}
    </div>
  );
};
