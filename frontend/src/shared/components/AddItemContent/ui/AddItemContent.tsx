import cn from 'classnames';
import { PlusOutlined } from '@ant-design/icons';
import { IAddItemContentProps } from '../lib';

import * as styles from './AddItemContent.module.scss';

export const AddItemContent = ({
  className,
  text,
  disabled,
  icon,
  onClick,
}: IAddItemContentProps) => {
  return (
    <button
      className={cn(styles.Button, className)}
      type="button"
      disabled={disabled}
      onClick={onClick}
    >
      {icon || <PlusOutlined />}
      <div className={styles.Title}>{text}</div>
    </button>
  );
};
