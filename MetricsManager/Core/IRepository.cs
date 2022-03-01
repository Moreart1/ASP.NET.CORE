namespace MetricsAgent.Interface
{

    public interface IRepository<T> where T : class
    {
        IList<T> GetAll(TimeSpan fromTime, TimeSpan toTime);

        T GetById(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);
    }
}
