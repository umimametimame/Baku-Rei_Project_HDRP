using AddClass;
using GenericChara;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara_Bakurei : Chara
{
    [SerializeField] private PosLimitInRange posLimitterProfile;
    [SerializeField] private PosRange thisPosRange = new PosRange();
    [SerializeField] private Camera playerCam;
    [field: SerializeField, NonEditable] public bool rigor { get; set; }

    protected override void Start()
    {
        thisPosRange = posLimitterProfile.range;
        assignSpeed = speed.entity;

        aliveAction += LimitPos;

        base.Start();
    }

    public void InitialUpdate()
    {
        moveVelocity.plan = Vector3.zero;

    }

    protected override void Update()
    {
        InitialUpdate();
        base.Update();
    }
    public void LimitPos()
    {
        transform.position = thisPosRange.Update(playerCam.transform.position, transform.position); ;
    }


}
