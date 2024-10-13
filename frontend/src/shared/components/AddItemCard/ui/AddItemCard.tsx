import cn from 'classnames';
import { AddItemContent } from 'shared/components/AddItemContent';
import { Flex } from 'antd';
import { IAddItemCardProps } from '../lib';

import * as styles from './AddItemCard.module.scss';

export const AddItemCard = ({
  className,
  disabled,
  height,
  width,
  text,
  onClick,
}: IAddItemCardProps) => {
  return (
    <Flex
      className={cn(styles.Card, className, {
        [styles.Disabled]: disabled,
      })}
      vertical
      justify="center"
      align="center"
      role="button"
      tabIndex={0}
      style={{ width, height }}
      onClick={disabled ? undefined : onClick}
      onKeyDown={disabled ? undefined : onClick}
    >
      <AddItemContent text={text} />
    </Flex>
  );
};
