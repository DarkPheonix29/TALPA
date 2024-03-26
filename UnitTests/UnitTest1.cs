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
            DAL.SubmitUser.UserSubmit("ben@gmail.com");
            //Assert
        }
        [TestMethod]
        public void Activity_submit()
        {
			//Arrange

			string connectionString = "data source=localhost;initial catalog=TALPADB;trusted_connection=true;Encrypt=true;TrustServerCertificate=true";
			DAL.ConnectionManager.Initialize(connectionString);

			BLL.User user = new User("ben@gmail.com", 9);
            List<LimitationTypes> limitations = new();
            limitations.Add((LimitationTypes)1);
            limitations.Add((LimitationTypes)2);
            limitations.Add((LimitationTypes)5);
            BLL.Activity activity = new Activity("Test", "Dit is een test activity om te kijken of het submitten werkt",limitations, user, DateTime.Now);
            //Act

            activity.SubmitToDatabase();

            //Assert
        }
    }
}