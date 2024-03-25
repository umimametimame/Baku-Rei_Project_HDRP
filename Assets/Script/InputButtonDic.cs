using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputButtonDic : MonoBehaviour
{
    public InputBUttonTable slots;
}


[Serializable] public class InputButtonPair : KeyAndValue<string, Slot>
{
    public InputButtonPair(string key, Slot value) : base(key, value)
    {

    }
}

[Serializable] public class InputBUttonTable : TableBase<string, Slot, InputButtonPair>
{

}