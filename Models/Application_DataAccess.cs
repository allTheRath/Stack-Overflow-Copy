using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA_Project.Models
{
    // All method signatures here.
    public interface IDataAccess
    {
        
    }

    // All database accessible methods here.
    public class Application_DataAccess : IDataAccess
    {
        ApplicationDbContext db = new ApplicationDbContext();



    }
}