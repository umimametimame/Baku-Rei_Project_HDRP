using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletLocus", menuName = "ScriptableObject/BulletLocus")]
public class BulletLocus : ScriptableObject
{
    [field: SerializeField] public Vec3Curve addPos { get; private set; }
    [SerializeField, Button("ZeroFill","ZeroFill", 0)] private int addPosZeroFillButton;
    [SerializeField, Button("Clear", "Clear", 0)] private int addPosClear;
    [field: SerializeField] public Vec3Curve addRot { get; private set; }
    [SerializeField, Button("ZeroFill", "ZeroFill", 1)] private int addRotZeroFillButton;
    [SerializeField, Button("Clear", "Clear", 1)] private int addRotClear;



    public void ZeroFill(int target)
    {
        switch (target)
        {
            case 0:
                addPos.Reset();
                addPos.ZeroFill();
                break;
            case 1:
                addRot.Reset();
                addRot.ZeroFill();
                break;
        }
    }
    public void Clear(int target)
    {
        switch (target)
        {
            case 0:
                addPos.Reset();
                addPos.Clear();
                break;
            case 1:
                addRot.Reset();
                addRot.Clear();
                break;
        }
    }
}

[Serializable] public class BulletLocusOperator
{
    [field: SerializeField] private BulletLocus bulletLocus;
    [field: SerializeField, NonEditable] public Vector3 posEva { get; private set; }
    [field: SerializeField, NonEditable] public Vector3 rotEva { get; private set; }
    [field: SerializeField, NonEditable] public VariedTime currentTime { get; private set; }

    public void Initialize()
    {
        Reset();
    }

    public void Reset()
    {
        currentTime.Initialize();

        posEva = bulletLocus.addPos.Eva(currentTime.value);
        rotEva = bulletLocus.addRot.Eva(currentTime.value);

    }


    /// <summary>
    /// timeÇÃëùâ¡<br/>
    /// EvaluteÇë„ì¸å„Ç…åƒÇ—èoÇ∑
    /// </summary>
    public void Update()
    {
        posEva = bulletLocus.addPos.Eva(currentTime.value);
        rotEva = bulletLocus.addRot.Eva(currentTime.value);

        currentTime.Update();
    }

}