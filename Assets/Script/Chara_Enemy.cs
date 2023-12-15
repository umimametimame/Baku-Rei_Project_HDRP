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
        Default,

    }
    [SerializeField] private MotionState motionState;
    [SerializeField] private LocusMotion spawnMotion = new LocusMotion();
    
    protected override void Start()
    {
        base.Start();
        SetParentTag(Tags.Chara);

        deathAction += DeathAction;
        spawnMotion.Initialize();
        spawnMotion.reachAction += ()=> ChangeMotion(MotionState.Default);
        ChangeMotion(MotionState.Spawn);
    }

    protected override void Update()
    {
        base.Update();
        invincible.Update();
        spawnMotion.Update();
        engine.velocityPlan += spawnMotion.velocity;
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

[Serializable] public class LocusMotion
{
    [SerializeField] private Traffic traffic = new Traffic();
    [SerializeField] private LocusOperator motionLocus = new LocusOperator();   // motionTimeÇÃäÑçáÇéûä‘Ç…ë„ì¸Ç∑ÇÈ
    [SerializeField] private Interval motionTime = new Interval();
    public LocusMotion()
    {

    }

    public void Initialize()
    {
        motionLocus.Initialize();
        motionTime.Initialize(false, true);
        motionTime.reachAction += OverMotionTimeAction;
    }

    public void Update()
    {
        traffic.Update();
    }

    public void EnableAction()
    {
        motionLocus.Update(motionTime.ratio);
        motionTime.Update();
    }

    public void DisableAction()
    {
        Reset();
    }

    public void Reset()
    {

        motionTime.Reset();
        motionLocus.Reset();
    }

    public void OverMotionTimeAction()
    {
        traffic.Close();
        Reset();
    }

    public void Launch()
    {
        traffic.Launch();
        Reset();
    }

    public Vector3 velocity
    {
        get { return motionLocus.posEva; }
    }
    public Vector3 eulerAngle
    {
        get { return motionLocus.rotEva; }
    }
    public Action reachAction
    {
        get { return motionTime.reachAction; }
        set { motionTime.reachAction = value; }
    }
}