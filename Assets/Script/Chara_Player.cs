using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;
using GenericChara;
using UnityEngine.InputSystem;

public class Chara_Player : BakureiChara
{
    public enum MotionState
    {
        Idle,
        Move,

    }
    [SerializeField] private BakuReiInputter inputter;
    [SerializeField] private MinMax slopeByZAxis = new MinMax();

    [SerializeField] private Easing slopeEasing = new Easing();
    [field: SerializeField] ValueChecker<InputHorizontal> horizontalChecker { get; set; } = new ValueChecker<InputHorizontal>();
    [field: SerializeField] ValueChecker<InputVertical> verticalChecker { get; set; } = new ValueChecker<InputVertical>();

    protected override void Start()
    {
        base.Start();
        SetParentTag(Tags.Chara);
        inputter = GetComponent<BakuReiInputter>();
        aliveAction += AliveAction;

        slopeEasing.Initialize();
        slopeEasing.active = true;

        horizontalChecker.Initialize(inputter.horizontal);
        horizontalChecker.changedAction += () => slopeEasing.Reset();
        verticalChecker.Initialize(inputter.vertical);
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

        slopeEasing.Update();
        inputter.AssignInputDirection();
        InputDirectionCheck();
        HorizontalSlopeObjUpdate();
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

    private void InputDirectionCheck()
    {
        horizontalChecker.Update(inputter.horizontal);
        verticalChecker.Update(inputter.vertical);
    }

    private void HorizontalSlopeObjUpdate()
    {
        Vector3 slope = model.transform.eulerAngles;
        switch (inputter.horizontal)
        {
            case InputHorizontal.None:
                slope.z = 0.0f;
                break;
            case InputHorizontal.Right:
                slope.z = slopeByZAxis.max;
                break;
            case InputHorizontal.Left:
                slope.z = slopeByZAxis.min;
                break;
        }

        
        model.transform.eulerAngles = Vector3.Lerp(AddFunction.GetNormalizedAngles(model.transform.eulerAngles, -180, 180), slope, slopeEasing.evaluteValue);
        
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


public enum InputHorizontal
{
    None,
    Right,
    Left,
}

public enum InputVertical
{
    None,
    Progress,
    Retreat,
}