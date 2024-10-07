import { UserId } from 'shared/types';

export type ReviewId = number;

export interface IReview {
  id: ReviewId;
  userId: UserId;
  userName: string;
  message: string;
  rating: number;
  creationDate: Date;
}
