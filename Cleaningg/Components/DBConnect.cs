using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleaningg.Components
{
    internal class DBConnect
    {
        public static DryCleaningEntities db;
        static DBConnect()
        {
            db = new DryCleaningEntities();
            db.User.Load();
        }
    }
}
