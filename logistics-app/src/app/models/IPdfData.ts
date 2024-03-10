import { IArticleData } from "./IArticleData";
import { IProjectData } from "./IProjectData";
import { ITransportBoxData } from "./ITransportBoxData";

export interface IPdfData
{
  transportbox: ITransportBoxData[];
  project: IProjectData | undefined;
  articles: IArticleData[];
}