using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using cahoot.Code;
using cahoot.Models.Repository;

namespace cahoot.Models.ViewModels
{
    public class ViewModelBase
    {
        private readonly IRepository _repository = new Repository.Repository();
        public IRepository Repository {
            get { return _repository; }
        }

        public List<TopFive> TopFive {
            get {
                return _repository.GetTopFiveScores();
            }
        }
        public List<LatestResult> LatestResult {

            get { return _repository.GetLatestResult(); }

        }

        public List<GuestBook> GuestbookEntries {

            get { return _repository.GetGuestBookEntries(); }

        }

        public List<Calendar> CalendarEvents {

            get { return _repository.GetCalendarEvents(null).OrderBy(e => e.EventDate).ToList(); }

        }
        public List<Sponsor> Sponsors {

            get { return XmlUtil.GetSponsorItems(); }

        }

        public List<SyndicationItem> SwebowlRss {
            get { return RssUtil.GetSweBowlRss(); }
        }
    }
}