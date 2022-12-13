using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace YamahaApp.Common
{
   
        [ServiceContract]
        public interface ILDAPAuthenticationServices
        {
            [OperationContract]
            ADSVCUserResultInfo ValidateLDAPUser(string userid, string password, string reqMode);
        }

        [DataContract]
        public class ADSVCUserResultInfo
        {
            [DataMember]
            public string Mode { get; set; }
            [DataMember]
            public string FirstName { get; set; }
            [DataMember]
            public string LastName { get; set; }
            [DataMember]
            public string Email { get; set; }
            [DataMember]
            public string Group { get; set; }
            [DataMember]
            public string Department { get; set; }
            [DataMember]
            public string Company { get; set; }
            [DataMember]
            public string ErrorMessage { get; set; }
            [DataMember]
            public string Message { get; set; }
            [DataMember]
            public string Status { get; set; }
            [DataMember]
            public string[] Grouplist { get; set; }
            
        }
   
}