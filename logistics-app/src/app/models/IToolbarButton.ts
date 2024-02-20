export interface IToolbarButton
{
  id: string;
  icon: string;
  text?: string;
  click?: () => void;
}