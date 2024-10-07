import React, { useMemo, useState } from 'react';
import { ConfigProvider, ThemeConfig } from 'antd';
import { ThemeContext, defaultTheme } from '../lib';

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [themeConfig, setThemeConfig] = useState<ThemeConfig>(defaultTheme);

  const defaultProps = useMemo(
    () => ({
      themeConfig,
      setThemeConfig,
    }),
    [themeConfig]
  );

  return (
    <ThemeContext.Provider value={defaultProps}>
      <ConfigProvider theme={{ ...themeConfig }}>{children}</ConfigProvider>
    </ThemeContext.Provider>
  );
};
