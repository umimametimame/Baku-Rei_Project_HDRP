using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BakureiChara
{
    [SerializeField] LocusMotion mainLocus = new LocusMotion();
    protected override void Start()
    {
        base.Start();


        mainLocus.Initialize();
    }

    protected override void Update()
    {
        base.Update();

        mainLocus.Update();
        SolutionAssignRot(mainLocus);
    }

}
