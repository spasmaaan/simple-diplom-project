import { IReview, ReviewId } from 'entities/reviews';

export interface IReviewsPanelProps {
  className?: string;
  reviews: IReview[];
  onAdd: () => void;
  onEdit: (id: ReviewId) => void;
  onRemove: (id: ReviewId) => void;
}
