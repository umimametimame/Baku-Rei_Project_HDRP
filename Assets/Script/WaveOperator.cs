using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOperator : MonoBehaviour
{
    [SerializeField] private SceneOperator_GameScene sceneOperator;
    [field: SerializeField, NonEditable] private List<Wave> childWaves = new List<Wave>();
    private void Start()
    {
        Wave[] waves = GetComponentsInChildren<Wave>();
        childWaves.AddRange(waves);
    }
    private void Update()
    {
        SetOffset();
    }

    private void SetOffset()
    {

        Vector3 offset = sceneOperator.scrollOffset.position;
        for (int i = 0; i < childWaves.Count; ++i)
        {
            //waves[i].instanceOffset = offset;
        }
    }
}

