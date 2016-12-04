using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.DAL
{
    public class ChatInitializer : System.Data.Entity.
    DropCreateDatabaseIfModelChanges<UserContext>
    {
    }

}