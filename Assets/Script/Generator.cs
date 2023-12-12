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
    [SerializeField] private BakureiChara body;
    [SerializeField] private InputOtherScript inputter;
    [SerializeField] private bool active;
    [SerializeField] private Instancer bullet;
    [SerializeField] private int objLimit;
    [SerializeField] private Interval rate;

    [SerializeField] private GenerateType generateType;
    [SerializeField] private List<Transform> muzzle = new List<Transform>();
    [SerializeField] private int currentMuzzle;
    [SerializeField] private ValueChecker<GenerateType> checker = new ValueChecker<GenerateType>();
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        bullet.Initialize();
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
        bullet.Update();



    }

    private void Generate()
    {
        if (active == false) { return; }

        switch (generateType)
        {
            case GenerateType.Volley:
                for(int i = 0; i < muzzle.Count; ++i)
                {
                    bullet.Instance(muzzle[i]);
                    bullet.lastObj.transform.eulerAngles = muzzle[i].transform.eulerAngles;
                }
                break;
            case GenerateType.Alternate:
                currentMuzzle = currentMuzzle % muzzle.Count;
                bullet.Instance(muzzle[currentMuzzle]); 
                bullet.lastObj.transform.eulerAngles = muzzle[currentMuzzle].transform.eulerAngles;

                currentMuzzle++;
                break;
            case GenerateType.Random:
                break;
        }

        rate.Reset();
    }

}
