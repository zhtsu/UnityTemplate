using Cysharp.Threading.Tasks;
using UnityEngine;

public class CG_Boot : UT_Boot
{
    public override void Initialize(UT_IServiceContainer ServiceContainer)
    {
        UT_IUIService IUIService = ServiceContainer.GetService<UT_IUIService>();
        UT_IEventService IEventService = ServiceContainer.GetService<UT_IEventService>();
        CG_IPostProcessService IPostProcessService = ServiceContainer.GetService<CG_IPostProcessService>();
    }
}
