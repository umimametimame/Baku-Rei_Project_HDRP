using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Scriptable/Create MotionData")]
public class so_Motion : ScriptableObject
{
    public bool canUse;
    public bool runtime;
    public AnimationCurve acceleration;
    public float doTime;
    public float time;
    public float degree;
    [System.NonSerialized] public Vector2 v;

    public Action action;
    public enum CostType
    {
        HP,
        EN,
        Pow,
        Speed,
        Other
    };
    public CostType costType;
    public float costValue;

    
}
