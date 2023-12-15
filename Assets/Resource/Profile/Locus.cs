using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Locus", menuName = "ScriptableObject/Locus")]
public class Locus : ScriptableObject
{
    [field: SerializeField] public Vec3Curve addPos { get; private set; }
    [SerializeField, Button("ZeroFill","ZeroFill", 0)] private int addPosZeroFillButton;
    [SerializeField, Button("Clear", "Clear", 0)] private int addPosClear;
    [field: SerializeField] public Vec3Curve assignEulerAngle { get; private set; }
    [SerializeField, Button("ZeroFill", "ZeroFill", 1)] private int assignEulerAngleZeroFillButton;
    [SerializeField, Button("Clear", "Clear", 1)] private int assignEulerAngleClear;



    public void ZeroFill(int target)
    {
        switch (target)
        {
            case 0:
                addPos.Reset();
                addPos.ZeroFill();
                break;
            case 1:
                assignEulerAngle.Reset();
                assignEulerAngle.ZeroFill();
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
                assignEulerAngle.Reset();
                assignEulerAngle.Clear();
                break;
        }
    }
}

[Serializable] public class LocusOperator
{
    [field: SerializeField] private Locus bulletLocus;
    [field: SerializeField, NonEditable] public Vector3 posEva { get; private set; }
    [field: SerializeField, NonEditable] public Vector3 rotEva { get; private set; }
    [field: SerializeField, NonEditable] public VariedTime currentTime { get; private set; } = new VariedTime();

    public void Initialize()
    {
        Reset();
    }

    public void Reset()
    {
        currentTime.Initialize();

        posEva = bulletLocus.addPos.Eva(currentTime.value);
        rotEva = bulletLocus.assignEulerAngle.Eva(currentTime.value);

    }


    /// <summary>
    /// timeÇÃëùâ¡<br/>
    /// EvaluteÇë„ì¸å„Ç…åƒÇ—èoÇ∑
    /// </summary>
    public void Update()
    {
        posEva = bulletLocus.addPos.Eva(currentTime.value);
        rotEva = bulletLocus.assignEulerAngle.Eva(currentTime.value);

        currentTime.Update();
    }

    /// <summary>
    /// äÑçáÇ≈EvaluteÇéZèoÇ∑ÇÈ
    /// </summary>
    /// <param name="ratio"></param>
    public void Update(float ratio)
    {
        posEva = bulletLocus.addPos.Eva(currentTime.value);
        rotEva = bulletLocus.assignEulerAngle.Eva(currentTime.value);

        currentTime.Update();

    }

}