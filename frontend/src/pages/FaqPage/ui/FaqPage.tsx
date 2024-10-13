import cn from 'classnames';
import { IFaqPageProps } from '../lib';

import * as styles from './FaqPage.module.scss';

export const FaqPage = ({ className }: IFaqPageProps) => {
  return <div className={cn(className, styles.FaqPage)}>FaqPage</div>;
};
