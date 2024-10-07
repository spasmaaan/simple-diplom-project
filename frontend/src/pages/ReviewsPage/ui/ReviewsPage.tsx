import cn from 'classnames';
import { IReviewsPageProps } from '../lib';

import * as styles from './ReviewsPage.module.scss';

export const ReviewsPage = ({ className }: IReviewsPageProps) => {
  return <div className={cn(className, styles.ReviewsPage)}>йцуй</div>;
};
