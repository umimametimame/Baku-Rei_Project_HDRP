using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class BakuReiInputter : InputOperator
{
    [field: SerializeField, NonEditable] public InputVecOrFloat<float> shiftInput { get; private set; } = new InputVecOrFloat<float>();
    [field: SerializeField, NonEditable] public InputVecOrFloat<float> attack01Input { get; private set; } = new InputVecOrFloat<float>();
    public void Start()
    {
        SetList();
        vInputs.Add(moveInput);
        Initialize();
    }



    #region PlayerInputEvent
    public void OnShift(InputValue value)
    {
        shiftInput.entity = value.Get<float>();
    }
    public void OnAttack01(InputValue value)
    {
        attack01Input.entity = value.Get<float>();
    }
    #endregion

    #region Property
    public bool moving
    {
        get { return moveInput.inputting; }
    }
    #endregion
}

[Serializable] public class InputOtherScript
{
    public enum InputType
    {
        Move,
        Shift,
        Attack01,
    }

    [SerializeField] private BakuReiInputter inputter;
    [SerializeField] private InputType type;

    public bool inputBool
    {
        get
        {
            switch (type)
            {
                case InputType.Shift:
                    return inputter.shiftInput.inputting;

                case InputType.Attack01:
                    return inputter.attack01Input.inputting;
                default:
                    Debug.Log("InputëŒè€Ç™à·Ç¢Ç‹Ç∑");
                    return false;
            }

        }
    }

    public Vector2 inputVec
    {
        get
        {
            switch (type)
            {
                case InputType.Move:
                    return inputter.moveInput.plan;
                default:
                    Debug.Log("InputëŒè€Ç™à·Ç¢Ç‹Ç∑");
                    return Vector2.zero;
            }
        }
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InputOtherScript))]
public class InputOtherDrawer : MyPropertyDrawer
{
    LabelAndproperty inputter = new LabelAndproperty("inputter");
    LabelAndproperty type = new LabelAndproperty("type");

    protected override void Update(Rect _pos, SerializedProperty _prop, GUIContent _label)
    {
        List<LabelAndproperty> list = new List<LabelAndproperty>() { inputter, type };

        inputter.Set(pos, prop);
        inputter.label = "Inputter";
        type.Set(pos, prop);
        type.label = "Type";

        labelWidthAve = 77.0f / 2.0f;

        inputter.InitialPosSet(pos.x, 44, UniformedRatio(2, 0.4f));
        type.neighbor = inputter;
        type.NeighborPosSet(30, UniformedRatio(2, 0.6f));

        inputter.Draw();
        type.Draw();
    }
}
#endif