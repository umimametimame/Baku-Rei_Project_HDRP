using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ProjectionRangeManager : MonoBehaviour
{
    [SerializeField] private List<MoveWhileCameraInvisible> targets = new List<MoveWhileCameraInvisible>();
    [SerializeField] private Loader loader;
    public bool finished;
    [SerializeField] private Transform center;
    [field: SerializeField, NonEditable] public Transform front { get; private set; }
    [field: SerializeField, NonEditable] public Transform back { get; private set; }
    [field: SerializeField, NonEditable] public Transform right { get; private set; }
    [field: SerializeField, NonEditable] public Transform left { get; private set; }

    protected virtual void Awake()
    {
    }

    private void Start()
    {
        targets.AddRange(GetComponentsInChildren<MoveWhileCameraInvisible>());

        loader.loadFunc += LoadFunc;

        front = targets[0].transform;
        back = targets[1].transform;
        right = targets[2].transform;
        left = targets[3].transform;
    }

    private void Update()
    {
        if(finished == false)
        {
            finished = LoadFunc();
        }
    }

    public bool LoadFunc()
    {

        for (int i = 0; i < targets.Count; ++i)
        {
            if(targets[i].finished == false) { return false; }
        }


        return true;
    }
    public float HorizontalLength
    {
        get
        {
            return Vector3.Distance(center.position, targets[0].transform.position);
        }
    }


}
