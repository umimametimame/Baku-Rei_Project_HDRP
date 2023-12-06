using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;
using GenericChara;
using UnityEngine.InputSystem;

public class Chara_Player : Chara_Bakurei
{
    [SerializeField] private BakuReiInputter inputter;

    protected override void Start()
    {
        base.Start();
        inputter = GetComponent<BakuReiInputter>();
        aliveAction += AliveAction;
    }
    protected override void Update()
    {
        RigorOperator();

        base.Update();
    }

    public void AliveAction()
    {
        AssignSpeed();
        AddAssignedMoveVelocity(inputter.moveInput.plan);
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
