public interface IBossState
{
    void Enter(Boss boss);
    void Execute(Boss boss);
    void Exit(Boss boss);
}