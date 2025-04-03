public class BossStateMaсhine
{
    public IdleState IdleState { get => _idleState; set => _idleState = value; }
    public MoveState MoveState { get => _moveState; set => _moveState = value; }
    public MeleeAttackState MeleeAttackState { get => _meleeAttackState; set => _meleeAttackState = value; }
    public SummonState SummonState { get => _summonState; set => _summonState = value; }
    public RangeAttackState RangeAttackState { get => _rangeAttackState; set => _rangeAttackState = value; }

    private IdleState _idleState;
    private MoveState _moveState;
    private MeleeAttackState _meleeAttackState;
    private SummonState _summonState;
    private RangeAttackState _rangeAttackState;

    private readonly Boss _boss;
    private IBossState _currentState;

    public BossStateMaсhine(Boss boss)
    {
        _boss = boss;
        _idleState = new IdleState();
        _moveState = new MoveState();
        _meleeAttackState = new MeleeAttackState();
        _summonState = new SummonState();
        _rangeAttackState = new RangeAttackState();
        _currentState = _idleState;
    }

    public void Update()
    {
        _currentState.Execute(_boss);
    }

    public void TransitionToState(IBossState state)
    {
        _currentState.Exit(_boss);
        _currentState = state;
        _currentState.Enter(_boss);
    }
}