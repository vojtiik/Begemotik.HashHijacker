using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Begemotik.User.Data;

namespace Begemotik.Tests.DataLayers
{
    [TestClass]
    public class UsersToken
    {
        [TestMethod]
        public void ReadAndWriteUsersToken()
        {
            // write one in first
            string token = "ddddddddddddddddddddddddddddd";
            string secret = "fffffffffffffffffffffffffffff";
            long id = new Random().Next(0, int.MaxValue);
            UserRepositoryFactory.GetRepository().SaveUserAccessToken(id, token, secret, "Bobby " + id);

            // read it back 
            var userToken = UserRepositoryFactory.GetRepository().ReadUserAccessToken("Bobby" + id);
            Assert.AreEqual(userToken.UserId, id);
            Assert.AreEqual(userToken.Token, token);
            Assert.AreEqual(userToken.TokenSecret, secret);
       }

        [TestMethod]
        public void ReadAndWriteUsersToken_load()
        {
            // write one in first
            string token = "ddddddddddddddddddddddddddddd";
            string secret = "fffffffffffffffffffffffffffff";
            long id = new Random().Next(0, int.MaxValue);
            for (int i = 0; i < 1000; i++)
            {
                UserRepositoryFactory.GetRepository().SaveUserAccessToken(id + i, token, secret, "Bobby" + i);
            }
            
            // read it back 
            for (int i = 0; i < 1000; i++)
            {
                var userToken = UserRepositoryFactory.GetRepository().ReadUserAccessToken("Bobby" + i);
                Assert.AreEqual(userToken.UserId, id + i);
                Assert.AreEqual(userToken.Token, token);
                Assert.AreEqual(userToken.TokenSecret, secret);
            }
        }
    }
}
