import { ResolveOptions } from 'webpack';
import { BuildOptions } from './types';

export const buildResolvers = ({ paths }: BuildOptions): ResolveOptions => {
  return {
    extensions: ['.tsx', '.ts', '.jsx', '.js'],
    preferAbsolute: true,
    modules: [paths.src, 'node_modules'],
    mainFiles: ['index'],
    alias: {},
  };
};
