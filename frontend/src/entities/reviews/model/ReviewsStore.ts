import { create } from 'zustand';
import { deleteElementWithId, setElementWithId } from 'shared/helpers';
import { IReview, IReviewData, ReviewId, ReviewsAction, ReviewsState } from '../lib';
import { addReview, editReview, loadReviews, removeReview } from '../api';

export const useReviewsStoreBase = create<ReviewsState & ReviewsAction>()((set, get) => ({
  reviewsLoading: false,
  reviewsLoaded: false,
  reviews: [],
  load: async () => {
    const { reviewsLoaded, reviewsLoading } = get();
    if (reviewsLoaded || reviewsLoading) {
      return;
    }
    set(() => ({ reviewsLoaded: false, reviewsLoading: true }));
    const reviews = await loadReviews();
    set(() => ({ reviewsLoaded: true, reviewsLoading: false, reviews }));
  },
  add: async (reviewData: IReviewData) => {
    if (!get().reviewsLoaded) {
      await get().load();
    }
    const { reviewsLoading } = get();
    if (reviewsLoading) {
      return;
    }
    set(() => ({ reviewsLoading: true }));
    const addedReview = await addReview(reviewData);
    set((state) => ({ reviewsLoading: false, reviews: [...state.reviews, addedReview] }));
  },
  edit: async (review: IReview) => {
    if (!get().reviewsLoaded) {
      await get().load();
    }
    const { reviewsLoading, reviews } = get();
    if (reviewsLoading || !reviews.find((currentDish) => currentDish.id === review.id)) {
      return;
    }
    set(() => ({ reviewsLoading: true }));
    const editedReview = await editReview(review);
    set((state) => ({
      reviewsLoading: false,
      reviews: setElementWithId(state.reviews, editedReview),
    }));
  },
  remove: async (reviewId: ReviewId) => {
    if (!get().reviewsLoaded) {
      await get().load();
    }
    const { reviewsLoading, reviews } = get();
    if (reviewsLoading || !reviews.find((currentDish) => currentDish.id === reviewId)) {
      return;
    }
    set(() => ({ reviewsLoading: true }));
    const removedReview = await removeReview(reviewId);
    set((state) => ({
      reviewsLoading: false,
      reviews: deleteElementWithId(state.reviews, removedReview),
    }));
  },
}));
