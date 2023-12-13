using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara_Enemy : BakureiChara
{
    protected override void Start()
    {
        base.Start();
        SetParentTag(Tags.Chara);

        deathAction += DeathAction;
    }

    protected override void Update()
    {
        base.Update();
        invincible.Update();
    }

    private void DeathAction()
    {
        Destroy(transform.parent.gameObject);
    }
}
