import path from 'path';
import { Configuration } from 'webpack';
import { BuildPaths, BuildEnv, buildWebpackConfig } from './config/webpack';

export default (env: BuildEnv): Configuration => {
  const paths: BuildPaths = {
    entry: path.resolve(__dirname, 'src', 'index.tsx'),
    build: path.resolve(__dirname, 'build'),
    html: path.resolve(__dirname, 'public', 'index.html'),
    src: path.resolve(__dirname, 'src'),
  };

  const mode = env.mode || 'development';
  const port = env.port || 3000;
  const isDev = mode === 'development';

  return buildWebpackConfig({
    mode,
    paths,
    isDev,
    port,
  });
};
