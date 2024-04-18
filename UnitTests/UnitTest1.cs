using System.Data;
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

            string connectionString = "Server=mssqlstud.fhict.local;Database=dbi532486_talpadb;User Id=dbi532486_talpadb;Password=5J@bmcLekt;Encrypt=true;TrustServerCertificate=true";
            DAL.ConnectionManager.Initialize(connectionString);
            //Act
            DAL.UserDataManager.UserSubmit("auth0|66052e2b423e9ac1d787cb32");
            //Assert
        }
        [TestMethod]
        public void Activity_submit()
        {
			//Arrange

			string connectionString = "Server=mssqlstud.fhict.local;Database=dbi532486_talpadb;User Id=dbi532486_talpadb;Password=5J@bmcLekt;Encrypt=true;TrustServerCertificate=true";
			DAL.ConnectionManager.Initialize(connectionString);

            List<LimitationTypes> limitations = new();
            limitations.Add((LimitationTypes)1);
            limitations.Add((LimitationTypes)2);
            limitations.Add((LimitationTypes)5);
            BLL.Activity activity = new Activity("Test", "Dit is een test activity om te kijken of het submitten werkt",limitations, "auth0|66052e2b423e9ac1d787cb32", DateTime.Now);
            ActivityManager AM = new();
            //Act

            AM.SubmitToDatabase(activity);

            //Assert
        }

        [TestMethod]
        public void Get_Activity()
        {
			//Arrange
			string connectionString = "Server=mssqlstud.fhict.local;Database=dbi532486_talpadb;User Id=dbi532486_talpadb;Password=5J@bmcLekt;Encrypt=true;TrustServerCertificate=true";
			DAL.ConnectionManager.Initialize(connectionString);

			//Act
			DataTable dt = DAL.ActivityDataManager.GetActivity(9);

			DataRow row = dt.Rows[0];
            foreach (DataColumn column in dt.Columns)
            {
	            Console.WriteLine($"{column.ColumnName}: {row[column]}");
            }
			//Assert
		}
    }
}