import { RuleSetRule } from 'webpack';
import MiniCssExtractPlugin from 'mini-css-extract-plugin';
import { BuildOptions } from './types';

export const buildLoaders = ({ isDev }: BuildOptions): RuleSetRule[] => {
  const typescriptLoader: RuleSetRule = {
    test: /\.tsx?$/,
    use: 'ts-loader',
    exclude: /node_modules/,
  };

  const styleLoaderValue = isDev ? 'style-loader' : MiniCssExtractPlugin.loader;
  const cssLoaderValue = {
    loader: 'css-loader',
    options: {
      modules: {
        auto: (resourcePath: string) => resourcePath.includes('.module.'),
        localIdentName: isDev ? '[path][name]__[local]--[hash:base64:5]' : '[hash:base64:8]',
      },
    },
  };

  const cssLoader: RuleSetRule = {
    test: /\.css$/i,
    use: [styleLoaderValue, cssLoaderValue],
  };

  const scssLoader: RuleSetRule = {
    test: /\.s[ac]ss$/i,
    use: [styleLoaderValue, cssLoaderValue, 'sass-loader'],
  };

  const fileLoader: RuleSetRule = {
    test: /\.(png|jpe?g|gif)$/i,
    type: 'asset/resource',
    generator: {
      filename: 'assets/img/[hash][ext][query]',
    },
  };

  return [typescriptLoader, cssLoader, scssLoader, fileLoader];
};
