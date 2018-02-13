using FileReader.Interfaces;
using System.IO;

namespace FileReader.Core
{
    public class SimpleAccessManager : IAccessManager
    {
        public static string adminIdentity = "admin";

        public bool CanAccess(string path, string identity)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            if (identity == adminIdentity) return true;

            var userIdentityCanAccess = Path.GetFileName(path).Contains(identity);
            return userIdentityCanAccess;
        }
    }
}
