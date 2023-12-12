using AddClass;
using GenericChara;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bodyにアタッチする
/// </summary>
public class BakureiChara : Chara
{
    [field: SerializeField] public EntityAndPlan<Camp> camp { get; private set; }
    [SerializeField] private VecRangeOperator thisPosRange = new VecRangeOperator();
    [SerializeField] private Camera playerCam;
    [field: SerializeField, NonEditable] public bool rigor { get; set; }
    [field: SerializeField, NonEditable] public List<Collider> hitBox { get; private set; }
    protected override void Start()
    {
        thisPosRange.AssignProfile();
        assignSpeed = speed.entity;
        aliveAction += LimitPos;
        foreach(Collider col in hitBox)
        {
            col.tag = Tags.Body;
        }

        base.Start();
    }

    protected void SetParentTag(string newTag)
    {
        transform.parent.tag = newTag;
    }

    public void InitialUpdate()
    {
        moveVelocity.plan = Vector3.zero;

    }

    public void OverrideCamp(Camp overrideCamp)
    {
        camp.plan = overrideCamp;
    }

    protected override void Update()
    {
        InitialUpdate();
        base.Update();
    }
    public void LimitPos()
    {
        if(thisPosRange.profile == null) { return; }
        transform.position = thisPosRange.Update(playerCam.transform.position, transform.position); ;
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