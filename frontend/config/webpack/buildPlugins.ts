import webpack, { WebpackPluginInstance } from 'webpack';
import HtmlWebpackPlugin from 'html-webpack-plugin';
import MiniCssExtractPlugin from 'mini-css-extract-plugin';
import ReactRefreshWebpackPlugin from '@pmmmwh/react-refresh-webpack-plugin';
import CopyPlugin from 'copy-webpack-plugin';
import { BuildOptions } from './types';

export const buildPlugins = ({ paths, isDev }: BuildOptions): WebpackPluginInstance[] => {
  const plugins: WebpackPluginInstance[] = [
    new HtmlWebpackPlugin({
      template: paths.html,
    }),
    new webpack.ProgressPlugin(),
    new MiniCssExtractPlugin({
      filename: 'css/[name].[contenthash].css',
      chunkFilename: 'css/[name].[contenthash].css',
    }),
    new webpack.DefinePlugin({
      __IS_DEV__: JSON.stringify(isDev),
    }),
    new CopyPlugin({
      patterns: [
        {
          context: 'public',
          from: '**/*',
          // to: '[name][ext]',
          globOptions: {
            ignore: ['**/index.html'],
          },
        },
      ],
    }),
  ];

  if (isDev) {
    plugins.push(new ReactRefreshWebpackPlugin(), new webpack.HotModuleReplacementPlugin());
  }

  return plugins;
};
