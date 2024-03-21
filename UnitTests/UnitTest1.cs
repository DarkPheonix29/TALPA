using System.Diagnostics;
using BLL;
using Activity = BLL.Activity;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void User_submit()
        {
            //Arange

            string connectionString = "data source=localhost;initial catalog=TALPADB;trusted_connection=true;Encrypt=true;TrustServerCertificate=true";
            DAL.ConnectionManager.Initialize(connectionString);
            //Act
            DAL.SubmitUser.UserSubmit("Ben");
            //Assert
        }
        [TestMethod]
        public void Activity_submit()
        {
			//Arrange

			string connectionString = "data source=localhost;initial catalog=TALPADB;trusted_connection=true;Encrypt=true;TrustServerCertificate=true";
			DAL.ConnectionManager.Initialize(connectionString);

			BLL.User user = new User("Ben", 2);
            List<int> limitations = new();
            limitations.Add(1);
            limitations.Add(2);
            limitations.Add(3);
            BLL.Activity activity = new Activity("Test", "Dit is een test activity om te kijken of het submitten werkt",limitations, user, DateTime.Now);
            //Act

            activity.SubmitToDatabase();

            //Assert
        }
    }
}