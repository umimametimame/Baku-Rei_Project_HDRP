using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.Users;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class BakuReiInputter : InputOperator
{
    [field: SerializeField, NonEditable] public InputVecOrFloat<float> shiftInput { get; private set; } = new InputVecOrFloat<float>();
    [field: SerializeField, NonEditable] public InputVecOrFloat<float> attack01Input { get; private set; } = new InputVecOrFloat<float>();
    public Dictionary<string, InputVecOrFloat<float>> slotsDic { get; private set; } = new Dictionary<string, InputVecOrFloat<float>>();
    [field: SerializeField, NonEditable] public List<InputVecOrFloat<float>> slotInputs { get; private set; } = new List<InputVecOrFloat<float>>();
    [field: SerializeField, NonEditable] public InputHorizontal horizontal { get; private set; }
    [field: SerializeField, NonEditable] public InputVertical vertical { get; private set; }
    public void Start()
    {
        SetInputsList();
        vInputs.Add(moveInput);

        for (int i = 0; i < InputButtons.buttons.Count; i++)
        {
            InputVecOrFloat<float> newInput = new InputVecOrFloat<float>();
            newInput.Initialize();

            slotsDic.Add(InputButtons.buttons[i], newInput);
            slotInputs.Add(slotsDic[InputButtons.buttons[i]]);
        }


        Initialize();
    }

    protected override void Update()
    {
        base.Update();
        for(int i = 0; i < slotInputs.Count; ++i)
        {
            slotInputs[i].Update();
            Debug.Log(slotInputs[i].floatRange);
        }
    }


    public void AssignInputDirection()
    {
        if (MoveInput.plan.x > 0)
        {
            horizontal = InputHorizontal.Right;
        }
        else if (MoveInput.plan.x < 0)
        {
            horizontal = InputHorizontal.Left;
        }
        else
        {
            horizontal = InputHorizontal.None;
        }

        if (MoveInput.plan.z > 0)
        {
            vertical = InputVertical.Progress;
        }
        else if (MoveInput.plan.z < 0)
        {
            vertical = InputVertical.Retreat;
        }
        else if(MoveInput.plan.z == 0)
        {
            vertical = InputVertical.None;
        }

    }

    #region PlayerInputEvent
    public void OnShift(InputValue value)
    {
        shiftInput.entity = value.Get<float>();
    }
    public void OnAttack01(InputValue value)
    {
        slotsDic[InputButtons.Attack01].entity = value.Get<float>();
    }
    public void OnAttack02(InputValue value)
    {
        slotsDic[InputButtons.Attack02].entity = value.Get<float>();
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

    //public bool inputBool
    //{
    //    get
    //    {
    //        switch (type)
    //        {
    //            case InputType.Shift:
    //                return inputter.shiftInput.inputting;

    //            case InputType.Attack01:
    //                return inputter.attack01Input.inputting;
    //            default:
    //                Debug.Log("Input対象が違います");
    //                return false;
    //        }

    //    }
    //}

    //public Vector2 inputVec
    //{
    //    get
    //    {
    //        switch (type)
    //        {
    //            case InputType.Move:
    //                return inputter.moveInput.plan;
    //            default:
    //                Debug.Log("Input対象が違います");
    //                return Vector2.zero;
    //        }
    //    }
    //}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InputOtherScript))]
public class InputOtherDrawer : MyPropertyDrawer
{
    LabelAndproperty inputter = new LabelAndproperty("inputter");
    LabelAndproperty type = new LabelAndproperty("type");

    protected override void Update(Rect _pos, SerializedProperty _prop, GUIContent _label)
    {

        inputter.label = "Inputter";
        type.label = "Type";

        List<LabelAndproperty> list = new List<LabelAndproperty>() { inputter };
        List<LabelAndproperty> list2 = new List<LabelAndproperty>() { type };
        verticalProps = new List<List<LabelAndproperty>> { list, list2 };
        SetElements();
        SetPropsList(verticalProps);

        inputter.labelRect.Width = 46;
        type.labelRect.Width = 30;
        DrawPropsList();

        //// 各変数の位置と高さを計算
        //Rect variable1Rect = new Rect(pos.x, pos.y, pos.width, EditorGUIUtility.singleLineHeight);
        //Rect variable2Rect = new Rect(pos.x, pos.y + EditorGUIUtility.singleLineHeight, pos.width, EditorGUIUtility.singleLineHeight);

        //// 各変数のラベルとプロパティを表示
        //EditorGUI.LabelField(variable1Rect, "Variable 1");
        //EditorGUI.PropertyField(new Rect(variable1Rect.x + EditorGUIUtility.labelWidth, variable1Rect.y, variable1Rect.width - EditorGUIUtility.labelWidth, variable1Rect.height), prop.FindPropertyRelative("inputter"), GUIContent.none);

        //EditorGUI.LabelField(variable2Rect, "Variable 2");
        //EditorGUI.PropertyField(new Rect(variable2Rect.x + EditorGUIUtility.labelWidth, variable2Rect.y, variable2Rect.width - EditorGUIUtility.labelWidth, variable2Rect.height), prop.FindPropertyRelative("type"), GUIContent.none);

    }
}
#endif


public static class InputButtons
{
    public static string Attack01 = nameof(Attack01);
    public static string Attack02 = nameof(Attack02);

    public static List<string> buttons = new List<string>() { Attack01, Attack02 };

}