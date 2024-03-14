export class SecuredRoutes 
{
  public static getSecuredRoutes(domainUrl:string):any
  {
    return  ([
      domainUrl + "/Transportbox",
      domainUrl + "/Project",
      domainUrl + "/Article",
      domainUrl + "/PDF",    
      domainUrl + "/Login",      
      domainUrl + "/ExportDatabase",      

    ]);
  }
}
