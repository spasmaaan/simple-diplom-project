import classNames from 'classnames';
import { IMainPageProps } from '../lib';

import * as styles from './MainPage.module.scss';

export const MainPage = ({ className }: IMainPageProps) => {
  return <div className={classNames(className, styles.MainPage)}>MainPage</div>;
};
