using Cysharp.Threading.Tasks;
using UnityEngine;

public class CG_Boot : UT_Boot
{
    public override void Initialize(UT_IServiceLocator ServiceLocator)
    {
        UT_IUIService IUIService = ServiceLocator.GetService<UT_IUIService>();
        UT_IEventService IEventService = ServiceLocator.GetService<UT_IEventService>();
        CG_IPostProcessService IPostProcessService = ServiceLocator.GetService<CG_IPostProcessService>();
    }
}
