using GenericChara;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;
using System;

public class Bullet : BakureiChara
{
    [SerializeField] private BulletLocusOperator bulletLocus;
    [SerializeField] private Interval lifeTime;
    [SerializeField] private MotionCollider attackBox;
    private bool AttackBoxPassingFunc(bool passing, Collider you)
    {
        if (you.tag != Tags.Body)
        {
            passing = false;
            return passing;
        }

        BakureiChara youChara = you.GetComponent<BakureiChara>();
        if (youChara.camp.plan != this.camp.plan)
        {
            passing = false;
            return passing;
        }

        return passing;
    }
    protected override void Start()
    {
        base.Start();
        SetParentTag(Tags.Bullet);
        
        assignSpeed = speed.entity;
        bulletLocus.Initialize();
        lifeTime.activeAction += () => StateChange(CharaState.Death);   // �������Ԍ��E���ɔj�󂳂��
        aliveAction += AliveAction;
        deathAction += DeathAction;

        attackBox.passJudgeFunc += AttackBoxPassingFunc;
        attackBox.Launch(pow.entity);
    }

    protected override void Spawn()
    {
        base.Spawn();
        lifeTime.Initialize(false, false);
    }

    /// <summary>
    /// aliveAction�Ɋ܂܂��
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
        Destroy(gameObject);
    }

    /// <summary>
    /// �e�̋O��(�p�x)��ύX����
    /// </summary>
    /// <returns></returns>
    public Vector3 SolutionChangeRot()
    {
        Vector3 newEuler = transform.eulerAngles;
        newEuler += bulletLocus.rotEva;

        transform.eulerAngles = newEuler;

        return newEuler;
    }

    /// <summary>
    /// �e�̋O��(�x�N�g���E���x)��ύX����
    /// </summary>
    public void SolutionAddPos()
    {

        Vector3 surface = Vector3.zero;
        surface.x = bulletLocus.posEva.x;
        surface.z = bulletLocus.posEva.z;

        Vector3 newVelo = surface.normalized;   // Y���ȊO�Ő��K��

        newVelo.y = bulletLocus.posEva.y;       // Y���̃C�[�W���O��t�^
        newVelo = transform.rotation * newVelo; // �����Ă�������ɐi��

        AddAssignedMoveVelocity(newVelo);
    }


    public void OnTriggerStay(Collider you)
    {
        
    }
}
