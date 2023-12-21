using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Chara_Enemy : BakureiChara
{
    public enum MotionState
    {
        Spawn,
        Run,
        Default,
    }
    [SerializeField] private List<LocusMotion> motionList = new List<LocusMotion>();
    [SerializeField] private MotionState motionState;       // motionListの疑似Indexとしても扱う
    [SerializeField] private LocusMotion spawnMotion = new LocusMotion();
    [SerializeField] private LocusMotion runMotion = new LocusMotion();
    [SerializeField] private List<LocusMotion> defaultMotionList = new List<LocusMotion>();
    [SerializeField] private Vector3 motionVelocity;
    [SerializeField] private SmoothRotate smooth;
    protected override void Start()
    {
        base.Start();
        SetParentTag(Tags.Chara);

        assignSpeed = speed.entity;

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
        base.Update();
        invincible.Update();
        spawnMotion.Update();
        runMotion.Update();
        moveVelocity.plan += motionVelocity;
    }

    private void AliveAction()
    {
        int motionIndex = 0;
        switch(motionState)
        {
            case MotionState.Spawn:
                break;

            case MotionState.Run:
                break;

            case MotionState.Default:
                break;
        }

        motionIndex += (int)motionState;

        AssignMotionVelocity(motionList[motionIndex]);
        AssignEulerAngle(motionList[motionIndex]);
        AssignModelEuler(motionList[motionIndex]);
    }

    private void ThinkNextMotion()
    {
        Debug.Log("Thinking...");
        if(defaultMotionList.Count == 0)       // 通常モーションが無ければ
        {
            ChangeMotion(MotionState.Run);  // 退散モーションに移行する
        }
    }





    private void ChangeMotion(MotionState nextState)
    {
        motionState = nextState;
    }

    private void DeathAction()
    {
        Destroy(transform.parent.gameObject);
    }
}

