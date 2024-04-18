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

            string connectionString = "User Id=postgres.fstwndfgkrxyywdmrkql;Password=dPaue%iMJ7Z%E3rFY6e7;Server=aws-0-eu-central-1.pooler.supabase.com;Port=5432;Database=postgres;";
            DAL.ConnectionManager.Initialize(connectionString);
            //Act
            DAL.UserDataManager.UserSubmit("ben@gmail.com");
            //Assert
        }
        [TestMethod]
        public void Activity_submit()
        {
			//Arrange

			string connectionString = "data source=localhost;initial catalog=TALPADB;trusted_connection=true;Encrypt=true;TrustServerCertificate=true";
			DAL.ConnectionManager.Initialize(connectionString);

            List<LimitationTypes> limitations = new();
            limitations.Add((LimitationTypes)1);
            limitations.Add((LimitationTypes)2);
            limitations.Add((LimitationTypes)5);
            BLL.Activity activity = new Activity("Test", "Dit is een test activity om te kijken of het submitten werkt",limitations, "9", DateTime.Now);
            ActivityManager AM = new();
            //Act

            AM.SubmitToDatabase(activity);

            //Assert
        }

        [TestMethod]
        public void Get_Activity()
        {
			//Arrange
			string connectionString = "data source=localhost;initial catalog=TALPADB;trusted_connection=true;Encrypt=true;TrustServerCertificate=true";
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