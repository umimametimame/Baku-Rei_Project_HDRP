using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene_Operator : SceneOperator
{
    [SerializeField] private GravityProfile scrollGravity;
    [SerializeField] private Instancer playerInstancer = new Instancer();
    [field: SerializeField] public List<BakureiChara> homingTargets { get; private set; } = new List<BakureiChara>();
    protected override void Start()
    {
        base.Start();
        playerInstancer.Initialize();
        playerInstancer.Instance();
    }
    protected override void Update()
    {
        base.Update();
        playerInstancer.Update();

        for (int i = homingTargets.Count - 1; i >= 0; --i)
        {
            if (homingTargets[i].charaState == GenericChara.Chara.CharaState.Death) // €–Só‘Ô‚È‚ç
            {
                homingTargets.RemoveAt(i);                                          // œŠO‚·‚é
            }
        }
    }

    public void AddChara(BakureiChara chara)
    {
        homingTargets.Add(chara);
    }
}
