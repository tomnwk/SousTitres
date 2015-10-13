namespace Domain.Command
{
    public interface ICommand<in T>
    {
        void Execute(T args);
    }
}
