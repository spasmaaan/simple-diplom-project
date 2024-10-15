export type ReviewId = number;

export interface IReviewData {
  fullName: string;
  shortName: string;
  message: string;
  rating?: number;
  creationDate: Date;
  me: boolean;
}

export interface IReview extends IReviewData {
  id: ReviewId;
}
