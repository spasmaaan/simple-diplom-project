import cn from 'classnames';
import { IServicePageProps } from '../lib';

import * as styles from './ServicePage.module.scss';

export const ServicePage = ({ className }: IServicePageProps) => {
  return <div className={cn(className, styles.ServicePage)}>ServicePage</div>;
};
