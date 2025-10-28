using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_Event
{
    private long _Id;
    private static long _IdCount = 0;

    public long Id
    {
        get { return _Id; }
    }

    public FB_Event()
    {
        _Id = ++_IdCount;
    }
}
