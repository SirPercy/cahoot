using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;
using System.Data;
using System.Web.Caching;
using System.Collections;
using cahoot.Models;
using cahoot.Models.ViewModels;
using cahoot.Extensions;
using cahoot.Models.ViewModels;

namespace cahoot.Models.Repository
{
    public class Repository : RepositoryBase
    {
       
        public override string ValidatePassWord(string username, string password)
        {
            OdbcConnection connection = new OdbcConnection(ConnectionString);
            using (connection)
            {
                try
                {

                    string sqlStr = "SELECT * FROM Medlem WHERE Losen=? AND Anvnamn=?";

                    OdbcCommand command = new OdbcCommand(sqlStr, connection);
                    command.Parameters.AddWithValue("?Password", password);
                    command.Parameters.AddWithValue("?Username", username);
                    connection.Open();

                    OdbcDataReader myReader = command.ExecuteReader();

                    string status = string.Empty;

                    while (myReader.Read())
                    {
                        int pwordindex = myReader.GetOrdinal("Losen");
                        string pword = myReader.GetString(pwordindex);

                        if (string.Compare(pword, password, false) == 0)
                        {
                            int statusindex = myReader.GetOrdinal("Status");
                            status = myReader.GetString(statusindex);
                        }
                        else
                        {
                            status = String.Empty;
                        }
                    }
                    return status;

                }
                catch (Exception e)
                {
                    throw new ApplicationException("Database error", e);
                }
            }
        }

        public override List<Member> GetMembers(int? id)
        {
            OdbcConnection connection = new OdbcConnection(ConnectionString);

            using (connection)
            {
                try
                {
                    string sqlStr = "SELECT * FROM Medlem";
                    OdbcCommand command = new OdbcCommand(sqlStr, connection);
                    connection.Open();

                    List<Member> members = new List<Member>();
                    using (OdbcDataReader myReader = command.ExecuteReader())
                    {

                        int idIndex = myReader.GetOrdinal("MedlemID");
                        int nameIndex = myReader.GetOrdinal("Namn");
                        int phoneIndex = myReader.GetOrdinal("Telefon");
                        int emailIndex = myReader.GetOrdinal("Epost");
                        int genderIndex = myReader.GetOrdinal("Kon");
                        int statusIndex = myReader.GetOrdinal("Status");
                        int userNameIndex = myReader.GetOrdinal("Anvnamn");
                        int passWordIndex = myReader.GetOrdinal("Losen");

                        while (myReader.Read())
                        {
                            members.Add(new Member
                            {
                                MemberID = myReader.GetInt32(idIndex),
                                Name = myReader.GetString(nameIndex),
                                Phone = !myReader.IsDBNull(phoneIndex) ? myReader.GetString(phoneIndex) : string.Empty,
                                Email = !myReader.IsDBNull(emailIndex) ? myReader.GetString(emailIndex) : string.Empty,
                                Gender = myReader.GetString(genderIndex),
                                Status = myReader.GetString(statusIndex),
                                UserName = myReader.GetString(userNameIndex),
                                PassWord = myReader.GetString(passWordIndex)
                            });

                        }


                        if (id == null)
                            return members;
                        return (from n in members where n.MemberID == id select n).ToList();
                    }

                }
                catch (Exception e)
                {
                    throw new ApplicationException("Database error", e);
                }
            }

        }
        public override bool CreateMemberEntry(MemberModel memberEntryToCreate)
        {
          
            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "INSERT INTO Medlem(Namn, Telefon, Epost, Kon, Status, Anvnamn, Losen) VALUES(?,?,?,?,?,?,?)";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_Name", memberEntryToCreate.Member.Name);
                    command.Parameters.AddWithValue("?p_Phone", memberEntryToCreate.Member.Phone ?? string.Empty);
                    command.Parameters.AddWithValue("?p_Email", memberEntryToCreate.Member.Email); 
                    command.Parameters.AddWithValue("?p_Gender", memberEntryToCreate.Member.Gender.Substring(0,1));
                    command.Parameters.AddWithValue("?p_Status", !string.IsNullOrEmpty(memberEntryToCreate.Member.Status) ? memberEntryToCreate.Member.Status : "new");
                    command.Parameters.AddWithValue("?p_UserName", memberEntryToCreate.Member.UserName);
                    command.Parameters.AddWithValue("?p_PassWord", !string.IsNullOrEmpty(memberEntryToCreate.Member.PassWord) ? memberEntryToCreate.Member.PassWord : "new");

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public override bool UpdateMemberEntry(MemberModel memberEntryToUpdate)
        {
            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {
                    string sql = "UPDATE Medlem SET Namn=?, Telefon=?, Epost=?, Kon=?, Status=?, Anvnamn=?, Losen=? WHERE MedlemID=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_Name", memberEntryToUpdate.Member.Name);
                    command.Parameters.AddWithValue("?p_Phone", memberEntryToUpdate.Member.Phone ?? string.Empty);
                    command.Parameters.AddWithValue("?p_Email", memberEntryToUpdate.Member.Email);
                    command.Parameters.AddWithValue("?p_Gender", memberEntryToUpdate.Member.Gender.Substring(0, 1));
                    command.Parameters.AddWithValue("?p_Status", memberEntryToUpdate.Member.Status);
                    command.Parameters.AddWithValue("?p_UserName", memberEntryToUpdate.Member.UserName);
                    command.Parameters.AddWithValue("?p_Losen", memberEntryToUpdate.Member.PassWord.ToBase64String());
                    command.Parameters.AddWithValue("?p_MedlemId", memberEntryToUpdate.Member.MemberID);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public override bool DeleteMemberEntry(int memberEntryId)
        {
            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "Delete from Medlem Where MedlemId=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_id", memberEntryId);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        public override List<TopFive> GetTopFiveScores()
        {
             List<TopFive> topFive = HttpContext.Current.Cache["topfive"] as List<TopFive>;
             if (topFive == null)
             {
                 OdbcConnection connection = new OdbcConnection(ConnectionString);
                 using (connection)
                 {
                     try
                     {
                         OdbcCommand cmd = new OdbcCommand("{ CALL `135540-cahoot`.GetTopFiveScores() }", connection);
                         cmd.CommandType = CommandType.StoredProcedure;

                         connection.Open();

                         topFive = new List<TopFive>(5);
                         using (OdbcDataReader myReader = cmd.ExecuteReader())
                         {

                             int nameIndex = myReader.GetOrdinal("Namn");
                             int resultIndex = myReader.GetOrdinal("Resultat");


                             while (myReader.Read())
                             {
                                 topFive.Add(new TopFive
                                 {
                                     Name = myReader.GetString(nameIndex),
                                     Result = myReader.GetInt32(resultIndex)
                                 });

                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         throw new ApplicationException("Database error", ex);
                     }

                 }
                 HttpContext.Current.Cache.Insert("topfive", topFive, null, DateTime.Now.AddHours(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
 
             }
             return topFive;
        }
        public override List<LatestResult> GetLatestResult()
        {
            List<LatestResult> latestResult = HttpContext.Current.Cache["latestresult"] as List<LatestResult>;
            if (latestResult == null)
            {

                OdbcConnection connection = new OdbcConnection(ConnectionString);

                using (connection)
                {

                    try
                    {
                        OdbcCommand cmd = new OdbcCommand("{ CALL `135540-cahoot`.GetLatestResult() }", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        latestResult = new List<LatestResult>();
                        using (OdbcDataReader myReader = cmd.ExecuteReader())
                        {

                            int teamIndex = myReader.GetOrdinal("Lag");
                            int locationIndex = myReader.GetOrdinal("Plats");
                            int homeTeamPtsIndex = myReader.GetOrdinal("Poang_vi");
                            int opponentIndex = myReader.GetOrdinal("Motst");
                            int awayTeamPtsIndex = myReader.GetOrdinal("Poang_de");

                            while (myReader.Read())
                            {
                                latestResult.Add(new LatestResult
                                {
                                    Team = myReader.GetString(teamIndex),
                                    Location = myReader.GetString(locationIndex),
                                    PointsHomeTeam = myReader.GetByte(homeTeamPtsIndex),
                                    Opponent = myReader.GetString(opponentIndex),
                                    PointsAwayTeam = myReader.GetByte(awayTeamPtsIndex)
                                });

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Database error", ex);
                    }
                }
                HttpContext.Current.Cache.Insert("latestresult", latestResult, null, DateTime.Now.AddHours(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            return latestResult;
        }
        
        public override List<Calendar> GetCalendarEvents(int? id)
        {
            OdbcConnection connection = new OdbcConnection(ConnectionString);
            using (connection)
            {

                try
                {
                    string sql = "SELECT * FROM Calendar ORDER BY EventDate";
                    OdbcCommand cmd = new OdbcCommand(sql, connection);

                    connection.Open();

                    List<Calendar> calendarEvents = new List<Calendar>();
                    using (OdbcDataReader myReader = cmd.ExecuteReader())
                    {

                        int idIndex = myReader.GetOrdinal("EventId");
                        int textIndex = myReader.GetOrdinal("EventText");
                        int infoIndex = myReader.GetOrdinal("EventInfo");
                        int dateIndex = myReader.GetOrdinal("EventDate");

                        while (myReader.Read())
                        {
                            calendarEvents.Add(new Calendar
                            {
                                EventId = myReader.GetInt32(idIndex),
                                EventText = myReader.GetString(textIndex),
                                EventInfo = myReader.GetString(infoIndex),
                                EventDate = myReader.GetDateTime(dateIndex)
                            });

                        }
                    }

                    if (id == null)
                       return calendarEvents;
                    return (from n in calendarEvents where n.EventId == id select n).ToList();

                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Database error", ex);
                }
            }

        }
        public override bool CreateCalendarEntry(CalendarModel calendarEntryToCreate)
        {

            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "INSERT INTO Calendar(EventText, EventInfo, EventDate) VALUES(?,?,?)";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_EventText", calendarEntryToCreate.Calendar.EventText);
                    command.Parameters.AddWithValue("?p_EventInfo", calendarEntryToCreate.Calendar.EventInfo);
                    command.Parameters.AddWithValue("?p_EventDate", calendarEntryToCreate.Calendar.EventDate);
 
                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public override bool UpdateCalendarEntry(CalendarModel calendarEntryToUpdate)
        {

            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "Update Calendar Set EventText=?, EventDate=?, EventInfo=? Where EventID=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_EventText", calendarEntryToUpdate.Calendar.EventText);
                    command.Parameters.AddWithValue("?p_EventDate", calendarEntryToUpdate.Calendar.EventDate);
                    command.Parameters.AddWithValue("?p_EventInfo", calendarEntryToUpdate.Calendar.EventInfo);
                    command.Parameters.AddWithValue("?p_EventID", calendarEntryToUpdate.Calendar.EventId);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public override bool DeleteCalendarEntry(int calendarEntryId)
        {

            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "Delete from Calendar Where EventID=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_id", calendarEntryId);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override IQueryable<News> QueryNewsEntries(int? id)
        {
            OdbcConnection connection = new OdbcConnection(ConnectionString);
            using (connection)
            {

                try
                {
                    string sql = "SELECT * FROM Teaminfo ORDER BY Date Desc";
                    OdbcCommand cmd = new OdbcCommand(sql, connection);

                    connection.Open();

                    List<News> newsEntries = new List<News>();
                    using (OdbcDataReader myReader = cmd.ExecuteReader())
                    {

                        int idIndex = myReader.GetOrdinal("Id");
                        int textIndex = myReader.GetOrdinal("Text");
                        int dateIndex = myReader.GetOrdinal("Date");
                        int writerIndex = myReader.GetOrdinal("Writer");
                        int headingIndex = myReader.GetOrdinal("Heading");
                        int internalIndex = myReader.GetOrdinal("Internal");


                        while (myReader.Read())
                        {
                            newsEntries.Add(new News
                            {
                                Id = myReader.GetInt32(idIndex),
                                Heading = myReader.GetString(headingIndex),
                                Text = myReader.GetString(textIndex),
                                Date = myReader.GetDateTime(dateIndex),
                                Writer = myReader.GetString(writerIndex),
                                IsInternal = myReader.GetInt16(internalIndex) == 0 ? false : true
                            });

                        }
                    }

                    if(id == null)
                        return from n in newsEntries.AsQueryable() select n;
                    return from n in newsEntries.AsQueryable() where n.Id == id select n;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Database error", ex);
                }
            }

        }
        public override bool CreateNewsEntry(NewsModel newsToCreate)
        {
  
            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "INSERT INTO Teaminfo(Heading, Text, Date, Writer, Internal) VALUES(?,?,?,?,?)";
                    OdbcCommand command = new OdbcCommand(sql, Connection);
                    
                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_heading", newsToCreate.News.Heading);
                    command.Parameters.AddWithValue("?p_Text", newsToCreate.News.Text);
                    command.Parameters.AddWithValue("?p_Date", newsToCreate.News.Date);
                    command.Parameters.AddWithValue("?p_Writer", newsToCreate.News.Writer);
                    command.Parameters.AddWithValue("?p_Internal", newsToCreate.News.IsInternal);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public override bool UpdateNewsEntry(NewsModel newsToUpdate)
        {

            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "Update Teaminfo Set Heading=?, Text=?, Date=?, Writer=?, Internal=? Where Id=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_heading", newsToUpdate.News.Heading);
                    command.Parameters.AddWithValue("?p_Text", newsToUpdate.News.Text);
                    command.Parameters.AddWithValue("?p_Date", newsToUpdate.News.Date);
                    command.Parameters.AddWithValue("?p_Writer", newsToUpdate.News.Writer);
                    command.Parameters.AddWithValue("?p_Internal", newsToUpdate.News.IsInternal);
                    command.Parameters.AddWithValue("?p_Id", newsToUpdate.News.Id);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public override bool DeleteNewsEntry(int newsId)
        {

            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "Delete from Teaminfo Where Id=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_id", newsId);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override List<GuestBook> GetGuestBookEntries()
        {
            OdbcConnection connection = new OdbcConnection(ConnectionString);
            using (connection)
            {
                try
                {
                    string sqlStr = "SELECT * FROM Gbok ORDER BY Nr DESC ";
                    OdbcCommand command = new OdbcCommand(sqlStr, connection);
                    connection.Open();

                    List<GuestBook> gbookEntries = new List<GuestBook>();
                    using (OdbcDataReader myReader = command.ExecuteReader())
                    {

                        int idIndex = myReader.GetOrdinal("Nr");
                        int nameIndex = myReader.GetOrdinal("Namn");
                        int textIndex = myReader.GetOrdinal("Text");
                        int dateIndex = myReader.GetOrdinal("Datum");

                        while (myReader.Read())
                        {
                            gbookEntries.Add(new GuestBook
                            {
                                EntryId = myReader.GetInt32(idIndex),
                                Name = myReader.GetString(nameIndex),
                                Text = myReader.GetString(textIndex),
                                Date = myReader.GetDateTime(dateIndex)
                            });

                        }
                    }
                    return gbookEntries;

                }
                catch (Exception e)
                {
                    throw new ApplicationException("Database error", e);
                }
            }

        }
        public override bool CreateGuestBookEntry(GbookModel guestBookToCreate)
        {
            
            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "INSERT INTO Gbok(Namn, Text, Datum) VALUES(?,?,?)";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_namn", guestBookToCreate.Gbook.Name);
                    command.Parameters.AddWithValue("?p_Text", guestBookToCreate.Gbook.Text);
                    command.Parameters.AddWithValue("?p_Datum", guestBookToCreate.Gbook.Date);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public override bool DeleteGuestBookEntry(int gbookId)
        {

            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    string sql = "Delete from Gbok Where Nr=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_nr", gbookId);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public override List<MatchData> GetMatchData()
        {
            List<MatchData> matchData = HttpContext.Current.Cache["resultmatchdata"] as List<MatchData>;
            if (matchData == null)
            {
                OdbcConnection connection = new OdbcConnection(ConnectionString);
                using (connection)
                {
                    try
                    {
                        OdbcCommand cmd = new OdbcCommand("{ CALL `135540-cahoot`.GetSeasonMatchResults() }", connection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        matchData = new List<MatchData>(8);
                        using (OdbcDataReader myReader = cmd.ExecuteReader())
                        {

                            int matchidIndex = myReader.GetOrdinal("MatchID");
                            int roundIndex = myReader.GetOrdinal("Omg");
                            int locationIndex = myReader.GetOrdinal("Plats");
                            int teamIndex = myReader.GetOrdinal("Lag");
                            int opponentIndex = myReader.GetOrdinal("Motst");
                            int ourPointsIndex = myReader.GetOrdinal("Poang_vi");
                            int opponentPointsIndex = myReader.GetOrdinal("Poang_de");
                            int ourScoreIndex = myReader.GetOrdinal("Slag_vi");
                            int opponentScoreIndex = myReader.GetOrdinal("Slag_de");


                            while (myReader.Read())
                            {
                                matchData.Add(new MatchData
                                {
                                    MatchId = myReader.GetInt32(matchidIndex),
                                    Round = myReader.GetInt32(roundIndex),
                                    Location = myReader.GetString(locationIndex),
                                    Team = myReader.GetString(teamIndex),
                                    Opponent = myReader.GetString(opponentIndex),
                                    OurPoints = myReader.GetInt32(ourPointsIndex),
                                    OpponentPoints = myReader.GetInt32(opponentPointsIndex),
                                    OurScore = myReader.GetInt32(ourScoreIndex),
                                    OpponentScore = myReader.GetInt32(opponentScoreIndex)
                                });

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException("Database error", ex);
                    }
                }
                HttpContext.Current.Cache.Insert("resultmatchdata", matchData, null, DateTime.Now.AddDays(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            return matchData;
        }
        public override List<Result> GetScoring(string type)
        {
            List<Result> scoring = HttpContext.Current.Cache[type + "resultscoring"] as List<Result>;
              if (scoring == null)
              {

                  OdbcConnection connection = new OdbcConnection(ConnectionString);
                  using (connection)
                  {
                      try
                      {
                          OdbcCommand cmd;
                          if (type == "men")
                              cmd = new OdbcCommand("{ CALL `135540-cahoot`.GetSeasonTopEight() }", connection);
                          else if (type == "women")
                              cmd = new OdbcCommand("{ CALL `135540-cahoot`.GetFemaleSeasonTopFive() }", connection);
                          else
                              cmd = new OdbcCommand("{ CALL `135540-cahoot`.GetHighestMatchResults() }", connection);

                          cmd.CommandType = CommandType.StoredProcedure;

                          connection.Open();

                          scoring = new List<Result>(8);
                          using (OdbcDataReader myReader = cmd.ExecuteReader())
                          {
                              int nameIndex, resultIndex, dateIndex, locationIndex;
                              if (type != string.Empty)
                              {
                                  nameIndex = myReader.GetOrdinal("Namn");
                                  resultIndex = myReader.GetOrdinal("Resultat");
                                  dateIndex = myReader.GetOrdinal("Datum");
                                  locationIndex = myReader.GetOrdinal("Plats");
                              }
                              else {
                                  nameIndex = myReader.GetOrdinal("Lag");
                                  resultIndex = myReader.GetOrdinal("Slagning");
                                  dateIndex = myReader.GetOrdinal("Datum");
                                  locationIndex = myReader.GetOrdinal("Plats");
                              }

                              while (myReader.Read())
                              {
                                  scoring.Add(new Result
                                  {
                                      Name = myReader.GetString(nameIndex),
                                      Score = myReader.GetInt32(resultIndex),
                                      Date = myReader.GetDateTime(dateIndex),
                                      Location = myReader.GetString(locationIndex)
                                  });

                              }
                          }

                      }
                      catch (Exception ex)
                      {
                          connection.Close();
                          throw new ApplicationException("Database error", ex);
                      }
                  }
                  HttpContext.Current.Cache.Insert(type + "resultscoring", scoring, null, DateTime.Now.AddDays(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
              }
              return scoring;
        }
        public override List<Team> GetTeams()
        {
            OdbcConnection connection = new OdbcConnection(ConnectionString);

            using (connection)
            {
                try
                {
                    string sqlStr = "SELECT * FROM Lag";
                    OdbcCommand command = new OdbcCommand(sqlStr, connection);
                    connection.Open();

                    List<Team> teams = new List<Team>();
                    using (OdbcDataReader myReader = command.ExecuteReader())
                    {

                        int idIndex = myReader.GetOrdinal("LagID");
                        int nameIndex = myReader.GetOrdinal("Lag");
                        while (myReader.Read())
                        {
                            teams.Add(new Team
                            {
                                TeamId = myReader.GetInt32(idIndex),
                                Name = myReader.GetString(nameIndex),
                            });

                        }
                    }
                    return teams;

                }
                catch (Exception e)
                {
                    throw new ApplicationException("Database error", e);
                }
            }

        }

        public override bool InsertMatch(ResultModel matchData)
        {
            OdbcConnection connection = new OdbcConnection(ConnectionString);
            using (connection)
            {
                try
                {

                    OdbcCommand command = new OdbcCommand();
                    command.Connection = connection;
                    command.CommandText = "{ CALL `135540-cahoot`.InsertMatch(?,?,?,?,?,?,?,?,?) }";
                    command.CommandType = CommandType.StoredProcedure;


                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_Omg", matchData.Match.Round);
                    command.Parameters.AddWithValue("?p_Datum", matchData.Match.Date);
                    command.Parameters.AddWithValue("?p_LagID", matchData.Match.TeamId);
                    command.Parameters.AddWithValue("?p_Plats", matchData.Match.Location);
                    command.Parameters.AddWithValue("?p_Motst", matchData.Match.Opponent);
                    command.Parameters.AddWithValue("?p_Poang_vi", matchData.Match.OurPoints);
                    command.Parameters.AddWithValue("?p_Slag_vi", matchData.Match.OurScore);
                    command.Parameters.AddWithValue("?p_Poang_de", matchData.Match.OpponentPoints);
                    command.Parameters.AddWithValue("?p_Slag_de", matchData.Match.OpponentScore);

                    connection.Open();
                    command.ExecuteNonQuery();
                    //Hämtar det MatchID som genererats och returnerar det.
                    string identity = "SELECT LAST_INSERT_ID()";
                    command.CommandText = identity;
                    OdbcDataReader myReader = command.ExecuteReader();
                    int matchno = 0;
                    while (myReader.Read()) {
                        matchno = myReader.GetInt32(0);
                    }
                    myReader.Close();
                    //Sparar resultat på senast inlagda match
                    foreach (var item in matchData.Match.Results)
                    {
                        string sqlStr = "INSERT INTO Matchrad(MatchID, MedlemID, Resultat) VALUES('" +
                        matchno + "','" + item.MemberId + "','" + item.Result + "')";
                        command.CommandText = sqlStr;
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }
                    ClearCache();
                    return true;
                }

                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public override bool DeleteMatchEntry(int matchId)
        {

            OdbcConnection Connection = new OdbcConnection(ConnectionString);

            using (Connection)
            {
                try
                {

                    //Delete match
                    string sql = "DELETE FROM `Match` WHERE MatchID=?";
                    OdbcCommand command = new OdbcCommand(sql, Connection);
                    //Delete matchrows
                    string sql2 = "DELETE FROM Matchrad WHERE MatchID=?";
                    OdbcCommand command2 = new OdbcCommand(sql2, Connection);

                    //Sätt parametrar
                    command.Parameters.AddWithValue("?p_MatchID", matchId); 
                    command2.Parameters.AddWithValue("?p_MatchID", matchId);

                    Connection.Open();
                    command2.ExecuteNonQuery();
                    command.ExecuteNonQuery();

                    ClearCache();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        private void ClearCache() {
            foreach (DictionaryEntry dEntry in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove(dEntry.Key.ToString());
            }        
        }
 
    }
}
