public interface IPoolObject<T>
{
    IObjectPool<T> pool { get; set; }
}

public interface IObjectPool<T>
{
    void Return(T obj);
    void Return(T obj, float delay);
}