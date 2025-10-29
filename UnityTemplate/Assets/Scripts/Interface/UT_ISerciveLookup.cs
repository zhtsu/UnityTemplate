using System;

public interface UT_IServiceLoopup
{
    T GetService<T>() where T : class;
}
