using Moosend.Wrappers.CSharpWrapper.Model;
using Sitecore.Foundation.SitecoreSend.Model;
using System.Collections.Generic;

namespace Sitecore.Foundation.SitecoreSend.Services
{
    public interface ISitecoreSendService
    {
        bool AddSubscriber(string emailListId, string email, string name,
            IDictionary<string, string> customFields = null, bool? hasExternalDoubleOptIn = null);

        bool AddMultipleSubscribers(string emailListId, List<Subscribers> subscribers);

        Context32 GetSubscribers(string emailListId, SubscriberStatus status, double? page = null,
            double? pageSize = null);

        IEnumerable<string> ConvertCustomFields(IDictionary<string, string> customFields);
    }
}
