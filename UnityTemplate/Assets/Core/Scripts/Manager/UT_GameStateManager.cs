using System.Threading.Tasks;

public enum UT_EGameState
{
    None,
    Loading,
    InMainMenu,
    Playing,
    Paused,
}
public class UT_GameStateManager : UT_Manager
{
    public override string ManagerName => "GameState Manager";

    public UT_GameStateManager()
    {
    }

    public override void Destroy()
    {

    }

    public override Task Initialize()
    {
        return Task.CompletedTask;
    }
}
