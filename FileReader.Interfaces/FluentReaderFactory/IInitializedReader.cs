namespace FileReader.Interfaces.FluentReaderFactory
{
    public interface IInitializedReader
    {
        IInitializedAndSecurizedReader WithSecurityAsUser(IAccessManager accessManager, string identity);
        IInitializedAndSecurizedReader WithoutSecurity();
    }
}