using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ClinSpec
{

    public static class ModelDisplayExtension
    {
        public static string CleanInvalidFileChars(this string strIn)
        {
            // Replace invalid characters with empty strings. 
            try
            {
                
                
                return Regex.Replace(strIn, @"[" + Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars())) + "]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,  
            // we should return Empty. 
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }


        public static string NodeText(this DataAccess.Domain e)
        {
            return string.Format("{0} [DomainId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.Domain e)
        {
            return string.Format("Domain:{0}", e.Id);
        }

        public static string NodeText(this DataAccess.Variable e)
        {
            return string.Format("{0} {1}", e.Name + string.Format(" - {0}", e.LableText), (e.CodeList == null) ? "" : ("  <i style=\"color:green\">CodeList: " + e.CodeList.Name + "</i>"));
        }

        public static string NodeId(this DataAccess.Variable e)
        {
            return string.Format("Variable:{0}", e.Id);
        }

        public static string NodeText(this DataAccess.CodeListValues e)
        {
            return string.Format("{0} [CodeListValuesId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.CodeListValues e)
        {
            return string.Format("CodeListValues:{0}", e.Id);
        }


        public static string NodeText(this DataAccess.Compound e)
        {
            return string.Format("{0} [CompoundId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.Compound e)
        {
            return string.Format("Compound:{0}", e.Id);
        }

        public static string NodeText(this DataAccess.Study e)
        {
            return string.Format("{0} [StudyId:{1}]", e.Name, e.Id);
        }

        public static string NodeId(this DataAccess.Study e)
        {
            return string.Format("Study:{0}", e.Id);
        }
    }
    
}