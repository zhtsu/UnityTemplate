using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewUnitData", menuName = "FB Data/UnitData")]
public class FB_UnitData : FB_IData
{
    public string Serialize()
    {
        return "return {}";
    }

    public bool Deserialize(string LuaCode)
    {

        return true;
    }
}
