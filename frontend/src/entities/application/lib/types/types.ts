export type ApplicationOptionId = string;
export type OutboxMessageId = number;

export interface IApplicationOptions {
  [id: ApplicationOptionId]: string | null;
}

export interface IOutboxMessage {
  id: OutboxMessageId;
  type: string;
  payload: string;
  createdAt: Date;
  processedAt: Date | null;
  error: string;
}
