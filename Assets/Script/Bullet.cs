using GenericChara;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;
using System;

public class Bullet : BakureiChara
{
    [SerializeField] private LocusMotion bulletLocus;
    [SerializeField] private Interval lifeTime;
    [SerializeField] private MotionCollider attackBox;
    [SerializeField] private bool penetrate;
    [SerializeField, NonEditable] private bool inductionOver;
    private bool AttackBoxPassingFunc(bool passing, Collider you)
    {
        if (you.tag != Tags.Body)   // 当たり判定を持っていなかったら
        {
            passing = false;
            return passing;
        }

        BakureiChara youChara = you.transform.root.Find(Tags.Body).GetComponent<BakureiChara>();
        if (youChara.camp.plan == this.camp.plan)   // 陣営が同じなら
        {
            passing = false;
            return passing;
        }

        return passing;
    }
    private void HitAction()
    {
        if(penetrate == false)
        {
            StateChange(CharaState.Death);
        }
    }
    protected override void Start()
    {
        base.Start();
        SetParentTag(Tags.Bullet);
        
        assignSpeed = speed.entity;
        bulletLocus.Initialize();
        lifeTime.activeAction += () => StateChange(CharaState.Death);   // 生存時間限界時に破壊される
        aliveAction += AliveAction;
        deathAction += DeathAction;

        attackBox.Initialize();
        attackBox.tag = Tags.Bullet;    
        attackBox.passJudgeFunc += AttackBoxPassingFunc;
        attackBox.hitAction += HitAction;
        attackBox.Launch(pow.entity);

        inductionOver = false;
    }

    protected override void Spawn()
    {
        base.Spawn();
        lifeTime.Initialize(false, false);
    }

    /// <summary>
    /// aliveActionに含まれる
    /// </summary>
    public void AliveAction()
    {

        if(lifeTime.interval > 0) { lifeTime.Update(); }
        
        SolutionAssignRot(bulletLocus);
        SolutionAddPos(bulletLocus);
        

        bulletLocus.Update();
    }

    public void DeathAction()
    {
        Destroy(transform.parent.gameObject);
    }


    public void OnTriggerStay(Collider you)
    {
        
    }
}
