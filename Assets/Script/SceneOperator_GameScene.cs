using AddClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneOperator_GameScene : SceneOperator
{
    public static SceneOperator_GameScene instance;
    [field: SerializeField, NonEditable] public GameState gameState { get; private set; }
    private LoaderManager loaderManager;
    [SerializeField] private GravityProfile scrollGravity;
    [SerializeField] private Instancer playerInstancer = new Instancer();
    [field: SerializeField] public Camera mainCamera { get; private set; }
    [field: SerializeField] public Transform scrollOffset { get; private set; }
    [field: SerializeField] public ProjectionRangeManager projectionRange { get; private set; }
    [field: SerializeField] public List<BakureiChara> homingTargets { get; private set; } = new List<BakureiChara>();
    protected override void Awake()
    {
        Singleton();
        base.Awake();
    }

    private void Singleton()
    {
        if (instance == null)
        {
            instance = (SceneOperator_GameScene)FindObjectOfType(typeof(SceneOperator_GameScene));
            DontDestroyOnLoad(gameObject); // 追加
        }
        else
        {
            Destroy(gameObject);

        }
    }

    protected override void Start()
    {
        base.Start();
        loaderManager = GetComponent<LoaderManager>();
        playerInstancer.Initialize();
        playerInstancer.Instance();
        playerInstancer.lastObj.transform.position = scrollOffset.position;
    }
    protected override void Update()
    {
        base.Update();
        playerInstancer.Update();

        HomingTargetsUpdate();

        SwitchGameState();
    }


    private void HomingTargetsUpdate()
    {


        for (int i = homingTargets.Count - 1; i >= 0; --i)
        {
            if (homingTargets[i].charaState == GenericChara.Chara.CharaState.Death) // 死亡状態なら
            {
                homingTargets.RemoveAt(i);                                          // 除外する
            }
        }
    }

    private void SwitchGameState()
    {
        switch(gameState)
        {
            case GameState.Loading:

                TimeStopManager.instance.TimeStop(Time.timeScale);
                if (loadFinished == true)
                {
                    gameState = GameState.Playing;
                    TimeStopManager.instance.TimeStart();
                }
                break;

            case GameState.Playing:
                break;

            case GameState.Pause:
                TimeStopManager.instance.TimeStop(1.0f);
                break;

        }
    }

    private void SwitchPause()
    {
        if(gameState == GameState.Playing)
        {
            gameState = GameState.Pause;
        }
        else if(gameState == GameState.Pause)
        {
            gameState = GameState.Playing;
        }
    }


    /// <summary>
    /// ロードが完了しているか
    /// </summary>
    public bool loadFinished
    {
        get
        {
            return loaderManager.finished;
        }
    }

    #region InputSystemの自動追加Event


    public void OnPositive(InputValue value)
    {
        Debug.Log("Positive");

    }
    public void OnNegative(InputValue value)
    {
        Debug.Log("Negative");

    }

    public void OnPause(InputValue value)
    {
        Debug.Log("Pause");
        SwitchPause();
    }
    #endregion


    #region 被実行メソッド
    public void AddHomingTargets(BakureiChara chara)
    {

        homingTargets.Add(chara);
    }
    #endregion
}

public enum GameState
{
    Loading,
    Playing,
    Pause,
}