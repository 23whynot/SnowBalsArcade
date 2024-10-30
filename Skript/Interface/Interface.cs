public class Interface 
{
    
}

public interface IState
{
    public void Enter();
    public void Exit();
}

public interface IPoolableObject
{
    public bool IsActive { get; }
    
    public void Activate();
    
    public void Deactivate();
}

public interface ISnowBall 
{
    
}

public interface IHippo
{
    
}
