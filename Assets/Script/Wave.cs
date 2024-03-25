using AddClass;
using GenericChara;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private Wave linkTarget;
    [SerializeField] private SpawnFlag spawnFlag;
    [SerializeField] private Interval interval = new Interval();    // SpawnFlagがReachTimeの場合に使用
    [SerializeField] private Instancer charaInstancer = new Instancer();
    [field: SerializeField] public Chara instancedChara;
    [field: SerializeField, NonEditable] public bool instanced { get; private set; }
    [field: SerializeField] public Vector3 instanceOffset { get; set; }
    public void Start()
    {
        interval.Initialize(false, false);
        interval.reachAction += Launch;
        charaInstancer.Initialize();
    }

    public void Update()
    {
        TargetLink();
        charaInstancer.Update();

        instanced = Instanced;
    }

    private void TargetLink()
    {
        if(linkTarget != null)  // linkTargetが設定されていれば
        {
            Chara linkChara = linkTarget.instancedChara;
            switch (spawnFlag)
            {
                case SpawnFlag.Spawned:
                    if (linkTarget.instanced) // linkTargetが出現済みなら
                    {
                        interval.Update();
                    }
                    break;
                case SpawnFlag.Died:
                    if (linkChara.charaState == Chara.CharaState.Death || linkChara == null) // linkTargetが居なければ
                    {
                        interval.Update();
                    }
                    break;
            }
        }
        else
        {
            interval.Update();
        }
    }

    /// <summary>
    /// インスタンスする
    /// </summary>
    public void Launch()
    {
        charaInstancer.InstanceOnlyOnce();
        charaInstancer.lastObj.transform.position += instanceOffset + SceneOperator_GameScene.instance.scrollOffset.position;
        instancedChara = charaInstancer.lastObj.GetComponentInChildren<Chara>();
    }

    public bool Instanced
    {
        get
        {
            return charaInstancer.instanced;
        }
    }
}

public enum SpawnFlag
{
    Spawned,
    Died,
}
