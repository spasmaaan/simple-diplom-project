import cn from 'classnames';
import { Outlet } from 'react-router-dom';
import { Navbar } from 'widgets/Navbar';
import { Layout } from 'antd';
import { Sidebar } from 'widgets/Sidebar';
import { LoaderProvider } from 'widgets/LoaderProvider';
import { IAppLayoutProps } from '../lib';

import * as styles from './AppLayout.module.scss';

const { Header, Content } = Layout;

export const AppLayout = ({ className }: IAppLayoutProps) => {
  return (
    <LoaderProvider>
      <Layout className={cn(styles.Root, className)}>
        <Header className={styles.Header}>
          <Navbar className={styles.Navbar} />
        </Header>
        <Layout>
          {/* <Sidebar /> */}
          <Layout>
            <Content className={styles.Content}>
              <div className={styles.PageWrapper}>
                <Outlet />
              </div>
            </Content>
          </Layout>
        </Layout>
      </Layout>
    </LoaderProvider>
  );
};
