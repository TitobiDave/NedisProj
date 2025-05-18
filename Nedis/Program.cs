
using DatabaseLib.DataStruct;

DictDb db = new DictDb();
var result = db.DbSetValue("tito", "name", 10);
Console.WriteLine(result.data);