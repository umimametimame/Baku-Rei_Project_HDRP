using GenericChara;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;
using System;

public class Bullet : BakureiChara
{
    [SerializeField] private LocusOperator bulletLocus;
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
        
        SolutionChangeRot();
        SolutionAddPos();
        

        bulletLocus.Update();
    }

    public void DeathAction()
    {
        Destroy(transform.parent.gameObject);
    }

    /// <summary>
    /// 弾の軌道(角度)を変更する
    /// </summary>
    /// <returns></returns>
    public Vector3 SolutionChangeRot()
    {
        Vector3 newEuler = bulletLocus.rotEva;

        transform.eulerAngles = newEuler;

        return newEuler;
    }

    /// <summary>
    /// 弾の軌道(ベクトル・速度)を変更する
    /// </summary>
    public void SolutionAddPos()
    {

        Vector3 surface = Vector3.zero;
        surface.x = bulletLocus.posEva.x;
        surface.z = bulletLocus.posEva.z;

        Vector3 newVelo = surface.normalized;   // Y軸以外で正規化

        newVelo.y = bulletLocus.posEva.y;       // Y軸のイージングを付与
        newVelo = transform.rotation * newVelo; // 向いている方向に進む

        AddAssignedMoveVelocity(newVelo);
    }


    public void OnTriggerStay(Collider you)
    {
        
    }
}
