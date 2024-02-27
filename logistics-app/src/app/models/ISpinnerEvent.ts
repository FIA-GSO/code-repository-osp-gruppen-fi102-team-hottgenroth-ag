export interface ISpinnerEvent
{
  message: string;
  action: Array<Promise<void>>;
}