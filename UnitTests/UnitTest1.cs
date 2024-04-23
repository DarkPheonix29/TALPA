using System.Data;
using System.Diagnostics;
using BLL;
using Activity = BLL.Activity;


namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
	    string connectionString = "Server=mssqlstud.fhict.local;Database=dbi532486_talpadb;User Id=dbi532486_talpadb;Password=5J@bmcLekt;Encrypt=true;TrustServerCertificate=true";
		[TestMethod]
        public void User_submit()
        {
            //Arange

            DAL.ConnectionManager.Initialize(connectionString);
            //Act
            DAL.UserDataManager.UserSubmit("auth0|66052e2b423e9ac1d787cb32");
            //Assert
        }
        [TestMethod]
        public void Activity_submit()
        {
			//Arrange

			DAL.ConnectionManager.Initialize(connectionString);

            List<LimitationTypes> limitations = new();
            limitations.Add((LimitationTypes)1);
            limitations.Add((LimitationTypes)1);
            limitations.Add((LimitationTypes)1);
            List<DateTime> dates = new();
            dates.Add(DateTime.Now);
            dates.Add(DateTime.Now);
            BLL.Activity activity = new Activity("Test", "Dit is een test activity om te kijken of het submitten werkt",limitations, "auth0|66052e2b423e9ac1d787cb32", DateTime.Now);
            activity.Dates = dates;
            ActivityManager AM = new();
            //Act

            AM.SubmitToDatabase(activity);

            //Assert
        }

        [TestMethod]
        public void Get_Activity()
        {
			//Arrange
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

        [TestMethod]
        public void Submit_Limitation()
        {
	        //Arrange
	        DAL.ConnectionManager.Initialize(connectionString);
	        DAL.ActivityDataManager adm = new();

	        string description = "This is a test limitation, don't forget to delete it.";
	        string type = "test";
	        //Act
	        adm.SubmitLimitation(description, type);
	        //Assert
		}

        [TestMethod]
        public void Create_Poll()
        {
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			DAL.PollDataManager pdm = new();
			List<int> activitys = new();
			activitys.Add(1);
            activitys.Add(1);
            activitys.Add(1);

			//Act
			pdm.PollSubmit(1, DateTime.Now, activitys);
			//Assert
        }

        [TestMethod]
        public void Update_Votes()
        {
            //Arrange
	        DAL.ConnectionManager.Initialize(connectionString);
	        DAL.PollDataManager pdm = new();

			//Act
			pdm.UpdateVotes(1, 1);

			//Assert
		}

        [TestMethod]
        public void Delete_Poll()
        {
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			DAL.PollDataManager pdm = new();

			//Act
            pdm.DeletePoll(1);

			//Assert
		}

        [TestMethod]
        public void Create_Team()
        {
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			DAL.TeamDataManager tdm = new();

			//Act
            tdm.CreateTeam("auth0|66052e2b423e9ac1d787cb32");

			//Assert
		}

        [TestMethod]
        public void Add_member_to_team()
        {
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			DAL.TeamDataManager tdm = new();

			//Act
            tdm.AddMemberToTeam("auth0|66052e2b423e9ac1d787cb32", 1);

			//Assert
		}

        [TestMethod]
        public void Remove_member_from_team()
        {
	        //Arrange
	        DAL.ConnectionManager.Initialize(connectionString);
	        DAL.TeamDataManager tdm = new();

	        //Act
	        tdm.RemoveMemberFromTeam("auth0|66052e2b423e9ac1d787cb32");

	        //Assert
		}
	}
}