import cn from 'classnames';
import { useDishesStore, IDishCategoryData, IDishCategory, DishCategoryId } from 'entities/dishes';
import { useCallback, useEffect, useMemo } from 'react';
import { ManagementProvider } from 'widgets/ManagementProvider';
import { DishCategoriesPanel } from 'widgets/DishCategoriesPanel';
import { DishCategoryDialog } from 'features/DishCategoryDialog';
import { IDishCategoriesPageProps } from '../lib';

import * as styles from './DishCategoriesPage.module.scss';

// Не хватает страницы с кухнями.

export const DishCategoriesPage = ({ className }: IDishCategoriesPageProps) => {
  const {
    categories,
    categoriesLoaded,
    addCategory,
    editCategory,
    removeCategory,
    loadCategories,
  } = useDishesStore();

  const createDialogOkHandler = useCallback(
    (id: DishCategoryId | undefined, handleOk: (item: IDishCategory | IDishCategoryData) => void) =>
      (data: IDishCategoryData) => {
        handleOk({
          ...data,
          ...(id ? { id } : {}),
        });
      },
    []
  );

  useEffect(() => {
    if (!categoriesLoaded) {
      loadCategories();
    }
  }, [categoriesLoaded, loadCategories]);

  return (
    <ManagementProvider
      className={cn(styles.DishCategoriesPage, className)}
      addTitle="Новая категория блюд"
      editTitle="Изменить категорию блюд"
      removeTitle="Удалить категорию блюд вместе со всеми блюдами?"
      renderDialog={({ open, id, title, okText, onOk, onClose }) => (
        <DishCategoryDialog
          open={open}
          title={title}
          defaults={categories.find((category) => category.id === id)}
          okText={okText}
          onOk={createDialogOkHandler(id, onOk)}
          onClose={onClose}
        />
      )}
      renderContent={(onAdd, onEdit, onRemove) =>
        categoriesLoaded ? (
          <DishCategoriesPanel
            dishCategories={categories}
            onAdd={onAdd}
            onEdit={onEdit}
            onRemove={onRemove}
          />
        ) : null
      }
      add={addCategory}
      edit={editCategory}
      remove={removeCategory}
    />
  );
};
