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
        if (you.tag != Tags.Body)   // “–‚½‚è”»’è‚ğ‚Á‚Ä‚¢‚È‚©‚Á‚½‚ç
        {
            passing = false;
            return passing;
        }

        BakureiChara youChara = you.transform.root.Find(Tags.Body).GetComponent<BakureiChara>();
        if (youChara.camp.plan == this.camp.plan)   // w‰c‚ª“¯‚¶‚È‚ç
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
        lifeTime.activeAction += () => StateChange(CharaState.Death);   // ¶‘¶ŠÔŒÀŠE‚É”j‰ó‚³‚ê‚é
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
    /// aliveAction‚ÉŠÜ‚Ü‚ê‚é
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
    /// ’e‚Ì‹O“¹(Šp“x)‚ğ•ÏX‚·‚é
    /// </summary>
    /// <returns></returns>
    public Vector3 SolutionChangeRot()
    {
        Vector3 newEuler = bulletLocus.rotEva;

        transform.eulerAngles = newEuler;

        return newEuler;
    }

    /// <summary>
    /// ’e‚Ì‹O“¹(ƒxƒNƒgƒ‹E‘¬“x)‚ğ•ÏX‚·‚é
    /// </summary>
    public void SolutionAddPos()
    {

        Vector3 surface = Vector3.zero;
        surface.x = bulletLocus.posEva.x;
        surface.z = bulletLocus.posEva.z;

        Vector3 newVelo = surface.normalized;   // Y²ˆÈŠO‚Å³‹K‰»

        newVelo.y = bulletLocus.posEva.y;       // Y²‚ÌƒC[ƒWƒ“ƒO‚ğ•t—^
        newVelo = transform.rotation * newVelo; // Œü‚¢‚Ä‚¢‚é•ûŒü‚Éi‚Ş

        AddAssignedMoveVelocity(newVelo);
    }


    public void OnTriggerStay(Collider you)
    {
        
    }
}
