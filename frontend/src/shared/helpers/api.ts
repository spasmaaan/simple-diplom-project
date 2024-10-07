export const getItemById = <TId, TItem extends { id: TId }>(items: TItem[], id: TId) => {
  const index = items.findIndex((item) => item.id === id);
  if (index < 0) {
    return null;
  }
  const item = items[index];
  return {
    index,
    item,
  };
};

export const setElementWithId = <TId, TItem extends { id: TId }>(
  sourceItems: TItem[],
  item: Partial<TItem>,
  doMerge?: boolean
) => {
  const items = [...sourceItems];
  const searchedItem = getItemById(items, item.id);
  if (!searchedItem) {
    return sourceItems;
  }
  items[searchedItem.index] = doMerge
    ? {
        ...searchedItem.item,
        ...item,
      }
    : (item as TItem);
  return items;
};

export const deleteElementWithId = <TId, TItem extends { id: TId }>(
  sourceItems: TItem[],
  item: TItem
) => {
  const items = [...sourceItems];
  const searchedItem = getItemById(items, item.id);
  if (!searchedItem) {
    return sourceItems;
  }
  delete items[searchedItem.index];
  return items;
};
