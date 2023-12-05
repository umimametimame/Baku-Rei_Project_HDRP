using GenericChara;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;
public class Bullet : Chara
{
    [SerializeField] private BulletLocusOperator bulletLocus;
    [SerializeField] private Interval lifeTime;
    protected override void Start()
    {
        base.Start();
        bulletLocus.Initialize();
        lifeTime.activeAction += () => StateChange(CharaState.Death);   // �������Ԍ��E���ɔj�󂳂��
        aliveAction += AliveAction;
        deathAction += DeathAction;
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
        assignSpeed = speed.entity;

        if(lifeTime.interval > 0) { lifeTime.Update(); }
        
        SolutionChangeRot();
        SolutionAddPos();

        bulletLocus.Update();
    }

    public void DeathAction()
    {
        Destroy(gameObject);
    }

    public Vector3 SolutionChangeRot()
    {
        Vector3 newEuler = transform.eulerAngles;
        newEuler += bulletLocus.rotEva;

        transform.eulerAngles = newEuler;

        return newEuler;
    }

    public void SolutionAddPos()
    {

        Vector3 surface = Vector3.zero;
        surface.x = bulletLocus.posEva.x;
        surface.z = bulletLocus.posEva.z;

        Vector3 newVelo = surface.normalized;   // Y���ȊO�Ő��K��

        newVelo.y = bulletLocus.posEva.y;       // Y���̃C�[�W���O��t�^
        newVelo = transform.rotation * newVelo; // �����Ă�������ɐi��
        moveVelocity.plan = newVelo;
    }

}
