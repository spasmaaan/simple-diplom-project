import cn from 'classnames';
import { IStatisticsPageProps } from '../lib';

import * as styles from './StatisticsPage.module.scss';

export const StatisticsPage = ({ className }: IStatisticsPageProps) => {
  return <div className={cn(className, styles.StatisticsPage)}>йцуй</div>;
};
