import cn from 'classnames';
import { IDishesPageProps } from '../lib';

import * as styles from './DishesPage.module.scss';

export const DishesPage = ({ className }: IDishesPageProps) => {
  return <div className={cn(className, styles.DishesPage)}>йцуй</div>;
};
