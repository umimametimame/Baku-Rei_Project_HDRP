using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [field: SerializeField] public List<Generator> generators = new List<Generator>();

    private void Start()
    {
        Generator[] child = GetComponentsInChildren<Generator>();
        generators.AddRange(child);
    }
}
