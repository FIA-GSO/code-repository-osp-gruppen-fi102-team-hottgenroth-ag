namespace Logisitcs.DAL;

public record ArticleAndBoxAssignment(
    string ArticleGuid, string ArticleName, string Description, long? Gtin, string Unit, string AssignmentGuid, string BoxGuid, double? Position, int? Status, int? Quantity, string ExpireDate);