namespace Ws.StorageCore.Views.ViewRefModels.PluNestings;

public interface IViewPluNestingRepository
{
    public IEnumerable<SqlViewPluNestingModel> GetEnumerable(ushort pluNumber = 0);
}