namespace Denapoli.Modules.Data
{
    public interface ISettingsService
    {
        string GetDbConnextionParameters();
        string GetDataRepositoryRootPath();
    }
}