import cn from 'classnames';
import { IDishPageProps } from '../lib';

import * as styles from './DishPage.module.scss';

export const DishPage = ({ className }: IDishPageProps) => {
  return <div className={cn(className, styles.DishPage)}>йцуй</div>;
};
