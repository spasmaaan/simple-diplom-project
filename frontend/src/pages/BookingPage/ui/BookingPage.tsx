import cn from 'classnames';
import { IBookingPageProps } from '../lib';

import * as styles from './BookingPage.module.scss';

export const BookingPage = ({ className }: IBookingPageProps) => {
  return <div className={cn(className, styles.BookingPage)}>BookingPage</div>;
};
