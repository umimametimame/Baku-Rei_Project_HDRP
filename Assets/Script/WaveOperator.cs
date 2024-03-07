using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOperator : MonoBehaviour
{
    [SerializeField] private SceneOperator_GameScene sceneOperator;
    [SerializeField] private List<Wave> waves = new List<Wave>();
    private void Update()
    {
        SetOffset();
    }

    private void SetOffset()
    {

        Vector3 offset = sceneOperator.scrollOffset.position;
        for (int i = 0; i < waves.Count; ++i)
        {
            //waves[i].instanceOffset = offset;
        }
    }
}

