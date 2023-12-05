using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;
using GenericChara;
using UnityEngine.InputSystem;

public class Chara_Player : Chara
{
    [SerializeField] private PosRange posRange = new PosRange();
    [SerializeField] private BakuReiInputter inputter;
    [SerializeField] private bool rigor;

    [SerializeField] VecT<float> newPlan = new VecT<float>();
    protected override void Start()
    {
        inputter = GetComponent<BakuReiInputter>();
        base.Start();
    }
    public void InitialUpdate()
    {
        AssignSpeed();

    }

    protected override void Update()
    {
        InitialUpdate();
        RigorOperator();
        base.Update();
        LimitPos(); 
    }

    public void AssignSpeed()
    {
        if (inputter.shiftInput.inputting == true)
        {
            assignSpeed = speed.entity / 2.0f;
        }
        else
        {
            assignSpeed = speed.entity;
        }
    }

    public void LimitPos()
    {
        moveVelocity.plan = inputter.moveInput.plan;
        transform.position = posRange.Update(transform.position);
    }

    public void RigorOperator()
    {
        foreach(var i in inputter.vInputs)
        {
            if (rigor == false) { i.Assign(); }
            else { i.plan = Vector3.zero; }
        }
        foreach(var i in inputter.fInputs)
        {
            if (rigor == false) { i.Assign(); }
            else { i.plan = 0.0f; }
        }

        
    }
}
