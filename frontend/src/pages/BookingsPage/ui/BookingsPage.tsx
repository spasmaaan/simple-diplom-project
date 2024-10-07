import cn from 'classnames';
import { IBookingsPageProps } from '../lib';

import * as styles from './BookingsPage.module.scss';

export const BookingsPage = ({ className }: IBookingsPageProps) => {
  return <div className={cn(className, styles.BookingsPage)}>Список бронирований</div>;
};
