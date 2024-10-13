import cn from 'classnames';
import { IServicesPageProps } from '../lib';

import * as styles from './ServicesPage.module.scss';

export const ServicesPage = ({ className }: IServicesPageProps) => {
  return <div className={cn(className, styles.ServicesPage)}>ServicesPage</div>;
};
