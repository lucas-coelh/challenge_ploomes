namespace Domain.Interfaces.Generics
{
    public interface IGenerics<T> where T : class
    {
        Task Add(T Object);
        Task Update(T Object);
        Task Delete(T Object);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
