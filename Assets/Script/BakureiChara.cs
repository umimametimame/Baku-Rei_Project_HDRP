using AddClass;
using GenericChara;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Chara_Player;

/// <summary>
/// Bodyにアタッチする
/// </summary>
public class BakureiChara : Chara
{
    [SerializeField] protected GameObject model;
    [field: SerializeField] public EntityAndPlan<Camp> camp { get; private set; }
    [SerializeField] private VecRangeOperator thisPosRange = new VecRangeOperator();
    [SerializeField] private Camera playerCam;
    [field: SerializeField, NonEditable] public bool rigor { get; set; }
    [field: SerializeField] public List<Collider> hitBox { get; private set; }
    private Quaternion originRot;

    [SerializeField] protected private MotionState motionState;
    protected override void Start()
    {
        thisPosRange.AssignProfile();
        assignSpeed = speed.entity;
        aliveAction += LimitPos;
        foreach(Collider col in hitBox)
        {
            col.tag = Tags.Body;
        }
        rotatePlan = originRot = transform.rotation;
        
        base.Start();
    }

    protected void SetParentTag(string newTag)
    {
        transform.parent.tag = newTag;
    }

    /// <summary>
    /// HomingTargetsに追加し、当たり判定を有効化(BOSSの出オチ防止)
    /// </summary>
    public void Materialization()
    {
        SceneOperator_GameScene.instance.AddHomingTargets(this);
    }

    public void OverrideCamp(Camp overrideCamp)
    {
        camp.plan = overrideCamp;
    }

    protected override void Update()
    {
        base.Update();

        if(hp.entity <= 0.0f)
        {
            StateChange(CharaState.Death);
        }

        // 移動系のUpdate
        rotatePlan *= originRot;
    }
    public void LimitPos()
    {
        if(thisPosRange.profile == null) { return; }
        transform.position = thisPosRange.Update(Camera.main.transform.position, transform.position); ;
    }

    /// <summary>
    /// Modelのrotateを変更する
    /// </summary>
    /// <returns></returns>
    public Vector3 SolutionAssignModelRot(LocusMotion motion)
    {
        Vector3 newEuler = motion.modelEulerAngle;

        model.transform.eulerAngles = newEuler;

        return newEuler;
    }

    /// <summary>
    /// 軌道(角度)を変更する
    /// </summary>
    /// <returns></returns>
    public Vector3 SolutionAssignRot(LocusMotion motion)
    {
        Quaternion newRotate = motion.rotate;

        rotatePlan *= newRotate;

        return newRotate.eulerAngles;
    }

    /// <summary>
    /// 軌道(ベクトル・速度)を変更する
    /// </summary>
    public void SolutionAddPos(LocusMotion motion)
    {

        Vector3 newVelo = motion.velocity;
        newVelo = transform.rotation * newVelo; // 向いている方向に進む

        AddAssignedMoveVelocity(newVelo);
    }


    protected void ChangeMotion(MotionState nextState)
    {
        motionState = nextState;
    }

    protected void DeathAction()
    {
        Destroy(transform.parent.gameObject);
    }


    public void Damage(float damage)
    {
        this.hp.entity -= damage;
        Debug.Log("Damage");
    }

    public void Attack(BakureiChara you)
    {
        you.Damage(pow.entity);
        Debug.Log("Attack");
    }
}


public static class CampTags
{
    public static string Player = nameof(Player);
    public static string Enemy = nameof(Enemy);

    public static string CampToString(Camp camp)
    {
        string returnStr = "該当なし";
        switch (camp)
        {
            case Camp.Player:
                returnStr = Player;
                break;
            case Camp.Enemy:
                returnStr = Enemy;
                break;

        }

        return returnStr;
    }
} 

public enum Camp
{
    Player,
    Enemy,
    Neutral,
}

public enum MotionState
{
    Default,
    Spawn = -2,
    Run = -1,
}