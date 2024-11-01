import cn from 'classnames';
import { useDishesStore, DishId, IDish, IDishData } from 'entities/dishes';
import { DishDialog } from 'features/DishDialog';
import { useCallback, useEffect, useMemo } from 'react';
import { DishesPanel } from 'widgets/DishesPanel';
import { ManagementProvider } from 'widgets/ManagementProvider';
import { useMatch } from 'react-router-dom';
import { useBookingStore } from 'entities/booking';
import { IDishesPageProps } from '../lib';

import * as styles from './DishesPage.module.scss';

export const DishesPage = ({ className }: IDishesPageProps) => {
  const match = useMatch('/dishes/:dishCategoryId');
  const {
    categories,
    categoriesLoaded,
    dishes,
    dishesLoaded,
    addDish,
    editDish,
    removeDish,
    loadDish,
    loadDishImage,
    loadCategories,
  } = useDishesStore();
  const { newBooking, setDish } = useBookingStore();

  const categoryId: number | null = useMemo(() => {
    if (!match?.params.dishCategoryId) {
      return null;
    }
    const parsedCategoryId = Number(match.params.dishCategoryId);
    return Number.isFinite(parsedCategoryId) ? parsedCategoryId : null;
  }, [match]);
  const currentCategory = useMemo(
    () => categories.find((category) => category.id === categoryId),
    [categories, categoryId]
  );
  const currentDishes = useMemo(
    () => dishes.filter((dish) => dish.categoryId === categoryId),
    [categoryId, dishes]
  );

  const createDialogOkHandler = useCallback(
    (id: DishId | undefined, handleOk: (item: IDish | IDishData) => void) => (data: IDishData) => {
      if (!categoryId) {
        return;
      }
      handleOk({
        ...data,
        ...(id ? { id } : {}),
        categoryId,
      });
    },
    [categoryId]
  );

  useEffect(() => {
    if (!categoriesLoaded) {
      loadCategories();
    }
    if (!dishesLoaded) {
      loadDish();
    } else {
      dishes.forEach(({ id }) => {
        loadDishImage(id);
      });
    }
  }, [dishesLoaded, categoriesLoaded, dishes, loadCategories, loadDish, loadDishImage]);

  if (!categoryId) {
    return null;
  }
  return (
    <ManagementProvider
      className={cn(styles.DishesPage, className)}
      addTitle="Новое блюдо"
      editTitle="Изменить блюдо"
      removeTitle="Удалить блюдо?"
      renderDialog={({ open, id, title, okText, onOk, onClose }) => (
        <DishDialog
          open={open}
          title={title}
          defaults={currentDishes.find((dish) => dish.id === id)}
          categories={categories}
          okText={okText}
          onOk={createDialogOkHandler(id, onOk)}
          onClose={onClose}
        />
      )}
      renderContent={(onAdd, onEdit, onRemove) =>
        dishesLoaded ? (
          <DishesPanel
            category={currentCategory}
            dishes={currentDishes}
            counts={newBooking?.dishes || {}}
            showCounts
            pricePostfix="₽"
            onChangeCount={setDish}
            onAdd={onAdd}
            onEdit={onEdit}
            onRemove={onRemove}
          />
        ) : null
      }
      add={addDish}
      edit={editDish}
      remove={removeDish}
    />
  );
};
