namespace BFT
{
    public interface IContainer<T>
    {
        void Add(T toAdd);
        void Remove(T toRemove);
    }
}
