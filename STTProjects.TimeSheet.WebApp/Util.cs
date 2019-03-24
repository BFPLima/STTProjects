using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STTProjects.TimeSheet.WebApp
{
    internal class Util
    {


        internal static string GetScriptToShowModalDialog(string dialogName)
        {            
            return GetScriptToHideOrShowModalDialog(dialogName, "show");
        }


        internal static string GetScriptToHideModalDialog(string dialogName)
        {
            return GetScriptToHideOrShowModalDialog(dialogName, "hide");
        }


        private static string GetScriptToHideOrShowModalDialog(string dialogName, string option)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");

            sb.Append(string.Format("$('#{0}').modal('{1}');", dialogName, option));

            sb.Append(@"</script>");

            return sb.ToString();
        }
    }
}