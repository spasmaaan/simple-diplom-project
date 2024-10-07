import { Button } from 'antd';
// import { useSheet } from 'entities/sheet';
import { useEffect, useState } from 'react';
// import { useSpreadSheetWidget } from 'shared/components/SpreadSheetWidget';
import { IProfileButtonProps } from '../lib';

export const ProfileButton = ({ className }: IProfileButtonProps) => {
  // const { currentTemplate } = useSheet();

  // const { instance: sheetInstance } = useSpreadSheetWidget();
  const [isDisabled, setIsDisabled] = useState(true);
  // const [currentSheetConfig, setCurrentSheetConfig] = useState<SheetConfig>();

  const onApply = () => {
    // if (!currentSheetConfig || !sheetInstance) {
    //   return;
    // }
    // sheetInstance.adjust();
    // sheetInstance.refresh();
  };

  return (
    <Button className={className} type="primary" onClick={onApply} disabled={isDisabled}>
      Apply config
    </Button>
  );
};
