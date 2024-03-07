using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ProjectionRangeManager : MonoBehaviour
{
    [SerializeField] private List<MoveWhileCameraInvisible> targets = new List<MoveWhileCameraInvisible>();
    public bool finished;
    public static ProjectionRangeManager instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (ProjectionRangeManager)FindObjectOfType(typeof(ProjectionRangeManager));
            DontDestroyOnLoad(gameObject); // ’Ç‰Á
        }
        else Destroy(gameObject);

    }

    private void Start()
    {
        targets.AddRange(GetComponentsInChildren<MoveWhileCameraInvisible>());

    }

    private void Update()
    {
        if(finished == false)
        {
            finished = CheckLoaded();
        }
    }

    public bool CheckLoaded()
    {

        for (int i = 0; i < targets.Count; ++i)
        {
            if(targets[i].finished == false) { return false; }
        }

        return true;
    }

}
