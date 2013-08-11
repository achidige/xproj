using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinSpec
{
    public class Messages
    {
       public const string SUCCESS ="SUCCESS";
       public const string ERROR_DOMAIN_EXISTS = "Domain '{0}' is aready in '{1}'. Please remove the domain before adding it agian.";
        public const string CONFIRM_DOMAIN_DETELET = "You are about to delete the Domain {0}. Any changes you have made to the domain will be lost. Do you want to continue.";
    }

}