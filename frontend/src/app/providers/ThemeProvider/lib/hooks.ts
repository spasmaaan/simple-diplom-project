import { useContext } from 'react';
import { ThemeConfig } from 'antd';
import { ThemeContext } from './constants';

export const useThemeConfig = () => {
  const { themeConfig, setThemeConfig } = useContext(ThemeContext);

  const updateThemeConfig = (data: Partial<ThemeConfig>) => {
    if (setThemeConfig) {
      setThemeConfig((previous) => ({
        ...previous,
        ...data,
      }));
    }
  };

  return {
    updateThemeConfig,
    themeConfig,
  };
};
