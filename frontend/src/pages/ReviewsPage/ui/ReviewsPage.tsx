import cn from 'classnames';
import { useCallback, useEffect } from 'react';
import { ManagementProvider } from 'widgets/ManagementProvider';
import { useReviewsStore, ReviewId, IReview, IReviewData } from 'entities/reviews';
import { ReviewsPanel } from 'widgets/ReviewsPanel';
import { ReviewDialog } from 'features/ReviewDialog';
import { IReviewsPageProps } from '../lib';

import * as styles from './ReviewsPage.module.scss';

export const ReviewsPage = ({ className }: IReviewsPageProps) => {
  const { reviews, reviewsLoaded, add, edit, remove, load } = useReviewsStore();

  const createDialogOkHandler = useCallback(
    (id: ReviewId | undefined, handleOk: (item: IReview | IReviewData) => void) =>
      (data: IReviewData) => {
        handleOk({
          ...data,
          ...(id ? { id } : {}),
        });
      },
    []
  );

  useEffect(() => {
    if (!reviewsLoaded) {
      load();
    }
  }, [load, reviewsLoaded]);

  return (
    <ManagementProvider
      className={cn(styles.ReviewsPage, className)}
      addTitle="Написать отзыв"
      editTitle="Изменить отзыв"
      removeTitle="Удалить отзыв?"
      renderDialog={({ open, id, title, okText, onOk, onClose }) => (
        <ReviewDialog
          open={open}
          title={title}
          defaults={reviews.find((review) => review.id === id)}
          okText={okText}
          onOk={createDialogOkHandler(id, onOk)}
          onClose={onClose}
        />
      )}
      renderContent={(onAdd, onEdit, onRemove) =>
        reviewsLoaded ? (
          <ReviewsPanel reviews={reviews} onAdd={onAdd} onEdit={onEdit} onRemove={onRemove} />
        ) : null
      }
      add={add}
      edit={edit}
      remove={remove}
    />
  );
};
