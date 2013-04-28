namespace Denapoli.Modules.Data
{
    public interface ISettingsService
    {
        string GetDbConnextionParameters();
        string GetDataRepositoryRootPath();
        int GetUpdatePeriod();
        int GetCommandDuration();
        int GetBorneId();
        int GetAdminUpdatePeriod();
    }
}