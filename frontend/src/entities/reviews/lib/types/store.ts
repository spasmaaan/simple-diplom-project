import { IReview, IReviewData, ReviewId } from './types';

export type ReviewsState = {
  reviewsLoading: boolean;
  reviewsLoaded: boolean;
  reviews: IReview[];
};

export type ReviewsAction = {
  load: () => Promise<void>;
  add: (reviewData: IReviewData) => Promise<void>;
  edit: (review: IReview) => Promise<void>;
  remove: (reviewId: ReviewId) => Promise<void>;
};
