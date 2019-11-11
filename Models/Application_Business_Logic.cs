using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA_Project.Models
{
    // An encapsulating class with associated method access from data access class.
    public class Application_Business_Logic
    {
        IDataAccess dataAccess;

        public Application_Business_Logic(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        // All access end points for database methods here..






    }
}