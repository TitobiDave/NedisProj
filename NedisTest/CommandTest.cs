using CommandLib;
using DatabaseLib.DataStruct;
using DatabaseLib.Sevices.Expiration.Contract;
using DatabaseLib.Sevices.Expiration.Handler;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NedisTest
{
    public class CommandTest
    {
        private readonly Mock<DictDb> _db;
        private readonly Mock<IExpireKey> _expireKey;
        private readonly Command command;
        public CommandTest()
        {
            _db = new Mock<DictDb>();
            _expireKey = new Mock<IExpireKey>();
            command = new Command(_db.Object, _expireKey.Object);
            
        }

        [Fact]
        public void TestSetValue()
        {
            //Arrange
            string comm = "Set UserName Titobi";

            //Assign
            var result = command.ParseCommand(comm);


            //Assert

            Assert.True(result.IsSuccesful);
        }
    }
}
