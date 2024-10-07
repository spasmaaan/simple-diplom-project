import { ThemeConfig } from 'antd';

export interface ThemeContextProps {
  themeConfig?: ThemeConfig;
  setThemeConfig?: React.Dispatch<React.SetStateAction<ThemeConfig>>;
}
