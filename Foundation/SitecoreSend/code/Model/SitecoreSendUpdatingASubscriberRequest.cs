using System.IO;
using System.Runtime.Serialization;
using Moosend.Wrappers.CSharpWrapper.Model;

namespace Sitecore.Foundation.SitecoreSend.Model
{
    internal class SitecoreSendUpdatingASubscriberRequest : UpdatingASubscriberRequest
    {
        public SitecoreSendUpdatingASubscriberRequest(string email, string name = null, object customFields = null, bool? hasExternalDoubleOptIn = null) : base(email, name)
        {
            Email = email ?? throw new InvalidDataException("Email is a required property for UpdatingASubscriberRequest and cannot be null");
            Name = name;
            CustomFields = customFields;
            HasExternalDoubleOptIn = hasExternalDoubleOptIn;
        }

        [DataMember(EmitDefaultValue = false, Name = "HasExternalDoubleOptIn")]
        public bool? HasExternalDoubleOptIn { get; set; }

        [DataMember(EmitDefaultValue = false, Name = "CustomFields")]
        public new object CustomFields { get; set; }
    }
}
