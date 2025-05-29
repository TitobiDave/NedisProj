using DatabaseLib.DataStruct;
using Misc;
using Moq;

namespace NedisTest
{
    public class DatabaseTest
    {
        public DatabaseTest()
        {
        }
        [Fact]
        public void TestDbRemove()
        {
            //Arrange
            string key = "testKey";
            var _db = new DictDb();
            //Act
            ResponseModel result = _db.GetItemByKey(key, out _);

            //Assert

            Assert.Equal("key doesn't exist", result.ErrorMessage);
        }
    }
}
