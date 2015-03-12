using System.Collections.Generic;
using cahoot.Models;
using cahoot.Models.ViewModels;
using cahoot.Models.ViewModels;

namespace cahoot.Models.Repository
{
    public interface IRepository
    {
        string ValidatePassWord(string username, string password);
        List<Member> GetMembers(int? id);
        bool CreateMemberEntry(MemberModel memberEntryToCreate);
        bool UpdateMemberEntry(MemberModel memberEntryToUpdate);
        bool DeleteMemberEntry(int memberEntryId);

        List<TopFive> GetTopFiveScores();
        List<LatestResult> GetLatestResult();

        List<Calendar> GetCalendarEvents(int? id);
        bool CreateCalendarEntry(CalendarModel calendarEntryToCreate);
        bool UpdateCalendarEntry(CalendarModel calendarEntryToUpdate);
        bool DeleteCalendarEntry(int calendarEntryId);
        
        List<News> GetNewsEntries(int? id);
        bool CreateNewsEntry(NewsModel newsToCreate);
        bool UpdateNewsEntry(NewsModel newsToUpdate);
        bool DeleteNewsEntry(int newsId);

        List<GuestBook> GetGuestBookEntries();
        bool CreateGuestBookEntry(GbookModel guestbookEntryToCreate);
        bool DeleteGuestBookEntry(int gbookId);

        List<MatchData> GetMatchData();
        List<Result> GetScoring(string type);
        bool InsertMatch(ResultModel matchData);
        bool DeleteMatchEntry(int matchId);
        List<Team> GetTeams();
    }
}
