using System;
using System.Collections.Generic;
using System.Linq;
using Moosend.Wrappers.CSharpWrapper.Api;
using Moosend.Wrappers.CSharpWrapper.Model;
using Sitecore.Diagnostics;
using Sitecore.Foundation.SitecoreSend.Model;

namespace Sitecore.Foundation.SitecoreSend.Services
{
    public class SitecoreSendService : ISitecoreSendService
    {
        private const string Format = "json";

        public bool AddSubscriber(string emailListId, string email, string name, IDictionary<string, string> customFields = null, bool? hasExternalDoubleOptIn = null)
        {
            var apiKey = GetApiKey();

            if (string.IsNullOrEmpty(apiKey))
            {
                Log.Error($"{nameof(SitecoreSendService)}.{nameof(AddSubscriber)} - No API key has been provided.", this);
                return false;
            }

            var apiInstance = new SubscribersApi();
            var body = new SitecoreSendAddingSubscribersRequest(email)
            {
                Name = name,
                HasExternalDoubleOptIn = hasExternalDoubleOptIn,
                CustomFields = ConvertCustomFields(customFields)
            };

            try
            {
                var result = apiInstance.AddingSubscribers(Format, emailListId, apiKey, body);

                if (result.Error == null)
                {
                    Log.Info($"{nameof(SitecoreSendService)}.{nameof(AddSubscriber)} - Subscriber has been added (or updated) to email list.", this);
                    return true;
                }

                Log.Error(
                    $"{nameof(SitecoreSendService)}.{nameof(AddSubscriber)} - API returned an error {result.Code}: {result.Error}",
                    this);
                return false;
            }
            catch (Exception e)
            {
                Log.Error($"{nameof(SitecoreSendService)}.{nameof(AddSubscriber)} failed.", e, this);
                return false;
            }
        }

        public bool AddMultipleSubscribers(string emailListId, List<Subscribers> subscribers)
        {
            var apiKey = GetApiKey();

            if (string.IsNullOrEmpty(apiKey))
            {
                Log.Error($"{nameof(SitecoreSendService)}.{nameof(AddMultipleSubscribers)} - No API key has been provided.", this);
                return false;
            }

            var apiInstance = new SubscribersApi();
            var body = new AddingMultipleSubscribersRequest(subscribers);

            try
            {
                var result = apiInstance.AddingMultipleSubscribers(Format, apiKey, emailListId, body);

                if (result.Error == null)
                {
                    Log.Info($"{nameof(SitecoreSendService)}.{nameof(AddMultipleSubscribers)} - Subscribers have been added (or updated) to email list.", this);
                    return true;
                }

                Log.Error(
                    $"{nameof(SitecoreSendService)}.{nameof(AddMultipleSubscribers)} - API returned an error {result.Code}: {result.Error}",
                    this);
                return false;
            }
            catch (Exception e)
            {
                Log.Error($"{nameof(SitecoreSendService)}.{nameof(AddMultipleSubscribers)} failed.", e, this);
                return false;
            }
        }

        public Context32 GetSubscribers(string emailListId, SubscriberStatus status, double? page = null, double? pageSize = null)
        {
            var apiKey = GetApiKey();

            if (string.IsNullOrEmpty(apiKey))
            {
                Log.Error($"{nameof(SitecoreSendService)}.{nameof(GetSubscribers)} - No API key has been provided.", this);
                return null;
            }

            var apiInstance = new SubscribersApi();

            try
            {
                var result = apiInstance.GettingSubscribers(Format, emailListId, apiKey, status.ToString(), page, pageSize);

                if (result.Error == null)
                {
                    Log.Info($"{nameof(SitecoreSendService)}.{nameof(GetSubscribers)} - Subscribers have been retrieved.", this);
                    return result.Context;
                }

                Log.Error(
                    $"{nameof(SitecoreSendService)}.{nameof(GetSubscribers)} - API returned an error {result.Code}: {result.Error}",
                    this);
                return null;
            }
            catch (Exception e)
            {
                Log.Error($"{nameof(SitecoreSendService)}.{nameof(GetSubscribers)} failed.", e, this);
                return null;
            }
        }
        
        protected virtual string GetApiKey()
        {
            return Environment.GetEnvironmentVariable("SITECORE_SEND_API_KEY");
        }

        public IEnumerable<string> ConvertCustomFields(IDictionary<string, string> customFields)
        {
            return customFields?.Select(c => c.Key + "=" + c.Value);
        }
    }
}
