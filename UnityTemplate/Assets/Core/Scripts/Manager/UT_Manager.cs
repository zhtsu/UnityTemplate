using System.Threading.Tasks;

public abstract class UT_Manager
{
    virtual public string ManagerName => "Default Manager";
    virtual public Task Initialize() { return Task.CompletedTask; }
    virtual public void Destroy() { }
}
