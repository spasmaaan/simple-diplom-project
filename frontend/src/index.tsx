import React from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { App } from 'app/App';
import { ThemeProvider } from 'app/providers/ThemeProvider';
// import { SheetProvider } from 'app/providers/SheetProvider';

import './index.scss';

const rootElement = document.getElementById('root');

if (rootElement) {
  const root = createRoot(rootElement);
  root.render(
    <React.StrictMode>
      <BrowserRouter>
        <ThemeProvider>
          {/* <SheetProvider templatePath="/templatesList.json"> */}
          <App />
          {/* </SheetProvider> */}
        </ThemeProvider>
      </BrowserRouter>
    </React.StrictMode>
  );
}
