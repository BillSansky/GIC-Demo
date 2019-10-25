namespace BFT
{
    public class ParametricActionListComponent<T> : ListComponent<Action<T>>
    {
        public void Execute(T value)
        {
            foreach (var func in this)
            {
                func.Invoke(value);
            }
        }
    }
}
