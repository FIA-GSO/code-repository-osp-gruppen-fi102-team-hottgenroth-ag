export interface INavRailItem
{
  name: string;
  click: () => void;
  identifier: string;
  icon: string | undefined;
}
