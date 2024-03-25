using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Chara_Enemy : BakureiChara
{
    [SerializeField] private List<LocusMotion> motionList = new List<LocusMotion>();
    [SerializeField] private LocusMotion spawnMotion = new LocusMotion();
    [SerializeField] private LocusMotion runMotion = new LocusMotion();
    [SerializeField] private List<LocusMotion> defaultMotionList = new List<LocusMotion>();
    [SerializeField] private int currentMotionIndex;
    [SerializeField] private Vector3 motionVelocity;
    protected override void Start()
    {
        base.Start();
        SetParentTag(Tags.Chara);

        assignSpeed = speed.entity;

        spawnAction += Materialization;
        spawnMotion.Initialize();
        spawnMotion.reachAction += ThinkNextMotion;
        spawnAction += spawnMotion.Launch;

        runMotion.Initialize();

        AssignMotionList();

        aliveAction += AliveAction;
        deathAction += DeathAction;

        ChangeMotion(MotionState.Spawn);
    }

    private void AssignMotionList()
    {
        motionList = new List<LocusMotion>() { spawnMotion, runMotion };
        foreach (LocusMotion motion in defaultMotionList)
        {
            motionList.Add(motion);
        }

    }

    protected override void Update()
    {
        invincible.Update();
        spawnMotion.Update();
        runMotion.Update();
        base.Update();
        moveVelocity.plan += motionVelocity;
    }

    private void AliveAction()
    {
        currentMotionIndex = 2;
        switch(motionState)
        {
            case MotionState.Default:
                break;
            case MotionState.Spawn:
                break;

            case MotionState.Run:
                break;

        }

        currentMotionIndex += (int)motionState;
        SolutionAssignModelRot(motionList[currentMotionIndex]);
        //SolutionAssignRot(motionList[currentMotionIndex]);
        SolutionAddPos(motionList[currentMotionIndex]);
    }

    private void ThinkNextMotion()
    {
        Debug.Log("Thinking...");
        if(defaultMotionList.Count == 0)       // 通常モーションが無ければ
        {
            ChangeMotion(MotionState.Run);  // 退散モーションに移行する
        }
    }





}
