using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Begemotik.SQL.Jobs
{
    public class JobRepositoryFactory
    {
        public static IJobRepository GetRepository()
        {
            return new JobRepository_EF();
        }
    }
}
