using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskViewModels
{
    public class ViewModelUtils
    {
        public static void ErrorRoutine(Exception e, string obj, string method)
        {
            if (e.InnerException != null)
            {
                Trace.WriteLine("Error in ViewModels, objects=" + obj + ", method=" + method + ", inner exception message=" + e.InnerException.Message);
                throw e.InnerException;
            }
            else
            {
                Trace.WriteLine("Error in ViewModels, objects=" + obj + ", method=" + method + ", inner exception message=" + e.Message);
                throw e;
            }
        }

        public bool LoadCollections()
        {
            bool createOk = false;
            try
            {
                DALUtils dalUtil = new DALUtils();
                createOk = dalUtil.LoadCollections();
            }
            catch(Exception ex)
            {
                ErrorRoutine(ex, "ViewModelUtils", "LoadCollections");
            }
            return createOk;
        }
    }
}
