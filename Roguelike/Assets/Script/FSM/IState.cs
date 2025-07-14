
public interface IState 
{
    //进入时
    void OnEnter();
    //执行时
    void OnUpdate();
    //退出时
    void OnExit();
}
