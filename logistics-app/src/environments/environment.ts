import { commonEnv } from "./environment.common";
import { SecuredRoutes } from "./securedRoutes";

const domainUrl :string = "https://localhost:44349";

const securedRoutes = SecuredRoutes.getSecuredRoutes(domainUrl);

const env: Partial<typeof commonEnv> = {
  serviceURL: domainUrl, // Service url might get replaced at runtime

  securedRoutes: securedRoutes,
};

// replace all properties changed in temporary object
export const environment = Object.assign(commonEnv, env);
