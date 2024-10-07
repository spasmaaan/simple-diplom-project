import { memo } from 'react';
import { SpreadSheetWidgetProps } from '../lib';

import './SpreadSheetWidget.scss';

export const SpreadSheetWidget = memo(
  (props: SpreadSheetWidgetProps) => {
    const { className } = props;
    return <div className={className} />;
  } /* shallowCompareConfig */
);

SpreadSheetWidget.displayName = 'SpreadSheet';
