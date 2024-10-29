export type ReviewId = number;

export interface IReviewData {
  fullName: string;
  shortName: string;
  message: string;
  rating?: number;
  creationDate: string;
  me: boolean;
}

export interface IReview extends IReviewData {
  id: ReviewId;
}
