import { backendDelete, backendGet, backendPost, backendPut } from 'shared/api';
import { ReviewsBackendUrl } from 'shared/config';
import { IReview, ReviewId } from '../lib';
import { MOCK_REVIEWS } from './mock';

const getReviewsPath = (url: string = '') => `${ReviewsBackendUrl}${url}`;

export const loadReviews = async (): Promise<IReview[]> => {
  return MOCK_REVIEWS; // (await backendGet(null, getReviewsPath())).json();
};
export const addReview = async (review: IReview): Promise<IReview> => {
  return (await backendPost(null, getReviewsPath(), review)).json();
};
export const editReview = async (review: IReview): Promise<IReview> => {
  return (await backendPut(null, getReviewsPath(), review)).json();
};
export const removeReview = async (reviewId: ReviewId): Promise<IReview> => {
  return (await backendDelete(null, getReviewsPath(`/${reviewId}`))).json();
};
