using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServerApiUpload.DataBaseFolder
{
    public static class InstanceDB
    {
        private static IRepository repositoryDBA;
        public static IRepository getInstance()
        {
            if (repositoryDBA == null)
                repositoryDBA = new RepositoryMSSQL();

            return repositoryDBA;
        }
    }
}