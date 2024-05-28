using System.Data;
using System.Diagnostics;
using BLL;
using DAL;
using Activity = BLL.Models.Activity;


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
            UserDataManager udm = new();

            //Act
            udm.UserSubmit("auth0|66052e2b423e9ac1d787cb32");
            //Assert
        }

        [TestMethod]
        public void Get_Activity()
        {
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			ActivityDataManager adm = new();

			//Act
			DataRow row = adm.GetActivity(9);

			//Assert
			Console.WriteLine(row.ToString());
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
            tdm.CreateTeam( "testTeam");

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

        [TestMethod]
        public void Delete_team()
        {
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			DAL.TeamDataManager tdm = new();

			//Act
			tdm.DeleteTeam(1);

			//Assert
		}

        [TestMethod]
        public void Get_all_activities()
        {
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			DAL.ActivityDataManager adm = new();

			//Act
			DataTable dt = adm.GetAllActivity();

			foreach (DataRow row in dt.Rows)
			{
				Console.WriteLine($"{row["name"]} : {row["description"]} : {row["proposing_user"]} : {row["date_added"]}");
			}

			//Assert
		}

        [TestMethod]
        public void Delete_activity()
        {
	        //Arrange
	        DAL.ConnectionManager.Initialize(connectionString);
	        DAL.ActivityDataManager adm = new();

	        //Act
	        adm.DeleteActivityById(9);

	        //Assert
		}

        [TestMethod]
        public void Update_voted_users()
        {
	        //Arrange
	        DAL.ConnectionManager.Initialize(connectionString);
	        DAL.ActivityDataManager adm = new();

	        //Act
	        adm.VotedUserUpdate("auth0|66052e2b423e9ac1d787cb32", 1);

	        //Assert
		}

        [TestMethod]
        public void Get_points_of_user()
        {
	        //Arrange
	        DAL.ConnectionManager.Initialize(connectionString);
	        DAL.UserDataManager udm = new();

	        //Act
	        udm.GetPoints("auth0|66052e2b423e9ac1d787cb32");

	        //Assert
		}

        [TestMethod]
		public void Check_team_activity()
        {
	        //Arrange
	        DAL.ConnectionManager.Initialize(connectionString);
	        DAL.TeamDataManager tdm = new();

	        //Act
	        tdm.CheckPlannedActivity(tdm.GetTeamId("testTeam"));

	        //Assert
		}

		[TestMethod]
		public void Get_team_activity()
		{
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			BLL.ActivityManager am = new();

			//Act
			Activity activity = am.GetActivity("testTeam");

			//Assert
			Console.WriteLine($"{activity.Name}, {activity.Description}, {activity.Location}");
		}

		[TestMethod]
		public void Check_team_poll()
		{
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			BLL.PollManager pm = new();

			//Act
			bool result = pm.PollActive("testTeam");

			//Assert
			Console.WriteLine(result);
		}

		[TestMethod]
		public void Check_of_user_voted()
		{
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			BLL.PollManager pm = new();

			//Act
			bool result = pm.PollChosen("auth0|66052e2b423e9ac1d787cb32");

			//Assert
			Console.WriteLine(result);
		}

		[TestMethod]
		public void Get_poll_id_with_team_id()
		{
			//Arrange
			DAL.ConnectionManager.Initialize(connectionString);
			DAL.PollDataManager pdm = new();

			//Act
			int pollId = pdm.GetPollIdWithTeamId(3);

			//Assert
			Assert.AreEqual(11, pollId);
		}
	}
}