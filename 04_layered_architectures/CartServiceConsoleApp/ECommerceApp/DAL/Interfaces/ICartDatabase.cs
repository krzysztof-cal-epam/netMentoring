namespace CartServiceConsoleApp.DAL.Interfaces
{
    public interface ICartDatabase<T>
    {
        T FindById(Guid id);
        void Upsert(T item);
        void Delete(Guid id);
        IEnumerable<T> GetAll();
    }
}
