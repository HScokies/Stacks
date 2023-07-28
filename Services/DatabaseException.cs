using Microsoft.AspNetCore.Mvc;

namespace Stacks_rework.Services
{
    public class DatabaseException : Exception
    {
        public int status { get; }
        public DatabaseException(int status)
        {
            this.status = status;
        }
    }
}
