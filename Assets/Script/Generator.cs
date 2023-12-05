using AddClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public enum GenerateType
    {
        Volley,
        Alternate,
        Random,
    }
    [SerializeField] private InputOtherScript inputter;
    [SerializeField] private bool active;
    [SerializeField] private Instancer instancer;
    [SerializeField] private int objLimit;
    [SerializeField] private Interval rate;

    [SerializeField] private GenerateType generateType;
    [SerializeField] private List<Transform> mullze = new List<Transform>();
    [SerializeField] private int currentMuzzle;
    [SerializeField] private ValueChecker<GenerateType> checker = new ValueChecker<GenerateType>();
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        instancer.Initialize();
        ActionReset();
        checker.Initialize(generateType);
        checker.changedAction = ActionReset;
        Spawn();
    }

    /// <summary>
    /// generateTypeÇ™ïœçXÇ≥ÇÍÇÈÇ∆é©ìÆìIÇ…çsÇÌÇÍÇÈ
    /// </summary>
    private void ActionReset()  
    {
        rate.activeAction = null;
        rate.activeAction += Generate;
        currentMuzzle = 0;
    }

    public void Spawn()
    {
        rate.Initialize(true, false);
        currentMuzzle = 0;
    }
    private void Update()
    {
        active = inputter.inputBool;
        checker.Update(generateType);
        rate.Update();
        instancer.Update();



    }

    private void Generate()
    {
        if (active == false) { return; }

        switch (generateType)
        {
            case GenerateType.Volley:
                for(int i = 0; i < mullze.Count; ++i)
                {
                    instancer.Instance(mullze[i]);
                    instancer.lastObj.transform.eulerAngles = mullze[i].transform.eulerAngles;
                }
                break;
            case GenerateType.Alternate:
                currentMuzzle = currentMuzzle % mullze.Count;
                instancer.Instance(mullze[currentMuzzle]); 
                instancer.lastObj.transform.eulerAngles = mullze[currentMuzzle].transform.eulerAngles;

                currentMuzzle++;
                break;
            case GenerateType.Random:
                break;
        }

        rate.Reset();
    }

}
