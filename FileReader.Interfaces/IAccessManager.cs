namespace FileReader.Interfaces
{
    public interface IAccessManager
    {
        bool CanAccess(string path, string identity);
    }
}
