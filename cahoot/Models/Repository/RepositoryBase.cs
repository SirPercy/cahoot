using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using cahoot.Models;
using cahoot.Models.ViewModels;
using cahoot.Models.ViewModels;

namespace cahoot.Models.Repository
{
    public abstract class RepositoryBase : IRepository
    {
        protected static readonly string ConnectionString = WebConfigurationManager.ConnectionStrings["BowlingConnectionString"].ConnectionString;

        public abstract string ValidatePassWord(string username, string password);
        public abstract List<Member> GetMembers(int? id);
        public abstract bool CreateMemberEntry(MemberModel memberEntryToCreate);
        public abstract bool UpdateMemberEntry(MemberModel memberEntryToUpdate);
        public abstract bool DeleteMemberEntry(int memberEntryId);

        public abstract List<TopFive> GetTopFiveScores();
        public abstract List<LatestResult> GetLatestResult();

        public abstract List<Calendar> GetCalendarEvents(int? id);
        public abstract bool CreateCalendarEntry(CalendarModel calendarEntryToCreate);
        public abstract bool UpdateCalendarEntry(CalendarModel calendarEntryToUpdate);
        public abstract bool DeleteCalendarEntry(int calendarEntryId);
 
        public abstract IQueryable<News> QueryNewsEntries(int? id);

        public virtual List<News> GetNewsEntries(int? id)
        {
            List<News> news = QueryNewsEntries(id).ToList();
            return news;
        }
        public abstract bool CreateNewsEntry(NewsModel newsToCreate); 
        public abstract bool UpdateNewsEntry(NewsModel newsToUpdate); 
        public abstract bool DeleteNewsEntry(int newsId);

        public abstract List<GuestBook> GetGuestBookEntries();
        public abstract bool CreateGuestBookEntry(GbookModel guestBookEntryToCreate);
        public abstract bool DeleteGuestBookEntry(int newsId);

        public abstract List<MatchData> GetMatchData();
        public abstract List<Result> GetScoring(string type);
        public abstract List<Team> GetTeams();

        public abstract bool InsertMatch(ResultModel matchData);
        public abstract bool DeleteMatchEntry(int matchId);


   }
    
}