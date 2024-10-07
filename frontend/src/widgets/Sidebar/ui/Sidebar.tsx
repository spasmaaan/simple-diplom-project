import { Layout, Space } from 'antd';

import * as styles from './Sidebar.module.scss';

const { Sider } = Layout;

export const Sidebar = () => {
  const dataAreas = ['DA1', 'DA2'];
  return (
    <Sider className={styles.Sidebar} width={300}>
      <Space className={styles.Container} direction="vertical">
        asd
      </Space>
    </Sider>
  );
};
