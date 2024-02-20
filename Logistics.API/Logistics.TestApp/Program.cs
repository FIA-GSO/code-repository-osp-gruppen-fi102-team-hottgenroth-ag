using Logisitcs.DAL;



//LogisticsDbContext logisticsDbContext = new LogisticsDbContext();
//DBCommands dBCommands = new DBCommands();
Guid guid = Guid.Parse("3c023d90-17b7-4f94-9bf3-7a881b10aea1");
//Article article = new()
//{
//    ArticleGuid = guid.ToString(),
//    ArticleName = "Artur",
//    Description = "Sartison",
//    Gtin = 1234,
//    Unit = "m2"
//};
//dBCommands.AddArticles(article);
var t = DBCommands.GetAllArticle();

Console.WriteLine("Hello, World!");