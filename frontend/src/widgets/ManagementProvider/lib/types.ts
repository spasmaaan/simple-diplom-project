export interface IManagementProviderProps<
  TId,
  TItemData extends object,
  TItem extends IManagementProviderItem<TId> & TItemData,
> {
  className?: string;
  addTitle: string;
  editTitle: string;
  removeTitle: string;
  renderDialog: (
    bindings: IManagementProviderDialogState<TId, TItemData, TItem>
  ) => React.ReactElement;
  renderContent: (
    onAdd: () => void,
    onEdit: (id: TId) => void,
    onRemove: (id: TId) => void
  ) => React.ReactElement | null;
  add: (item: TItemData) => void;
  edit: (item: TItem) => void;
  remove: (id: TId) => void;
}

export interface IManagementProviderItem<TId> {
  id: TId;
}

export interface IManagementProviderDialogState<
  TId,
  TItemData extends object,
  TItem extends IManagementProviderItem<TId> & TItemData,
> {
  id?: TId;
  okText: string;
  open: boolean;
  title: string;
  onOk: (item: TItemData | TItem) => void;
  onClose: () => void;
}
