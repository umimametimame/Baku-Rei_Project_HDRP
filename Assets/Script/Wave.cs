using AddClass;
using GenericChara;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private Wave linkTarget;
    [SerializeField] private SpawnFlag spawnFlag;
    [SerializeField] private Interval interval = new Interval();    // SpawnFlag��ReachTime�̏ꍇ�Ɏg�p
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
        if(linkTarget != null)  // linkTarget���ݒ肳��Ă����
        {
            Chara linkChara = linkTarget.instancedChara;
            switch (spawnFlag)
            {
                case SpawnFlag.Spawned:
                    if (linkTarget.instanced) // linkTarget���o���ς݂Ȃ�
                    {
                        interval.Update();
                    }
                    break;
                case SpawnFlag.Died:
                    if (linkChara.charaState == Chara.CharaState.Death || linkChara == null) // linkTarget�����Ȃ����
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
    /// �C���X�^���X����
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
