using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    [field: SerializeField] public List<Slot> slots { get; set; } = new List<Slot>();
    public Dictionary<string, Slot> slotsDic { get; set; } = new Dictionary<string, Slot>();
    private void Start()
    {
        Set();
    }

    public void Set()
    {
        Slot[] child = GetComponentsInChildren<Slot>();
        slots.AddRange(child);
        for(int i = 0; i < child.Length; i++)
        {
            slotsDic.Add(child[i].transform.name, slots[i]);
        }
    }


}
