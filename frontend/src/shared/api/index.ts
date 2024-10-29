import { authStore } from 'entities/auth';
import { BackendUrl } from 'shared/config';
import { FetchBody, FetchHeaders, UserId } from 'shared/types';

const bodyIsObject = (body: FetchBody | undefined) =>
  body &&
  typeof body === 'object' &&
  !(
    body instanceof ReadableStream ||
    body instanceof Blob ||
    body instanceof FormData ||
    body instanceof URLSearchParams ||
    body instanceof SourceBuffer
  );

const getApplicationUrl = () => {
  const { host, protocol } = window.location;
  return `${protocol}//${process.env.BACKEND_HOST || host}/${process.env.BACKEND_PATH || BackendUrl}`;
};

export const getBackendUrl = (backendUrl: string) => `${getApplicationUrl()}${backendUrl}`;

export const backendFetch = async (
  userId: UserId | null,
  backendUrl: string,
  method: string,
  body?: FetchBody,
  rawHeaders?: FetchHeaders
) => {
  const { authenticated, token } = authStore.getState();

  const url = getBackendUrl(backendUrl);
  const doUseJson = bodyIsObject(body);
  const headers: FetchHeaders = {
    ...(rawHeaders || {}),
  };
  if (authenticated && token) {
    headers.Authorization = `${token.tokenType} ${token.accessToken}`;
  }
  if (doUseJson) {
    headers['Content-Type'] = 'application/json';
  }
  const requestOptions: RequestInit = {
    method,
    headers,
    body: doUseJson ? JSON.stringify(body) : (body as BodyInit),
  };
  return fetch(url, requestOptions);
};

export const backendPut = async (
  userKey: UserId | null,
  backendUrl: string,
  body?: FetchBody,
  headers?: FetchHeaders
) => {
  return backendFetch(userKey, backendUrl, 'PUT', body, headers);
};

export const backendGet = async (
  userKey: UserId | null,
  backendUrl: string,
  headers?: FetchHeaders
) => {
  return backendFetch(userKey, backendUrl, 'GET', undefined, headers);
};

export const backendPost = async (
  userKey: UserId | null,
  backendUrl: string,
  body?: FetchBody,
  headers?: FetchHeaders
) => {
  return backendFetch(userKey, backendUrl, 'POST', body, headers);
};

export const backendDelete = async (
  userKey: UserId | null,
  backendUrl: string,
  body?: FetchBody,
  headers?: FetchHeaders
) => {
  return backendFetch(userKey, backendUrl, 'DELETE', body, headers);
};
