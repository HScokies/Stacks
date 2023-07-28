using Microsoft.AspNetCore.Mvc;

namespace Stacks_rework.Services
{
    public class DatabaseException : Exception
    {
        public ActionResult status { get; }
        public DatabaseException(ActionResult status)
        {
            this.status = status;
        }
    }
}
