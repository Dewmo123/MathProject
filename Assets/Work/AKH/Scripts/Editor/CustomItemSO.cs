using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ItemSO))]
public class CustomItemSO : Editor
{
    private SerializedProperty itemCntProp;
    private SerializedProperty itemNameProp;
    private SerializedProperty itemSpriteProp;
    private SerializedProperty itemCanUseProp;
    private SerializedProperty itemRestoreHpProp;
    private SerializedProperty itemRestoreHungryProp;
    private SerializedProperty itemRestoreWaterProp;
    private SerializedProperty itemCanCookProp;
    private SerializedProperty itemCompleteProp;
    private void OnEnable()
    {
        itemCntProp = serializedObject.FindProperty("cnt");
        itemNameProp = serializedObject.FindProperty("itemName");
        itemSpriteProp = serializedObject.FindProperty("sprite");
        itemCanUseProp = serializedObject.FindProperty("canUse");
        itemRestoreHpProp = serializedObject.FindProperty("restoreHp");
        itemRestoreHungryProp = serializedObject.FindProperty("restoreHungry");
        itemRestoreWaterProp = serializedObject.FindProperty("restoreWater");
        itemCanCookProp = serializedObject.FindProperty("canCook");
        itemCompleteProp = serializedObject.FindProperty("complete");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(itemCntProp);
        EditorGUILayout.PropertyField(itemNameProp);
        EditorGUILayout.PropertyField(itemSpriteProp);
        EditorGUILayout.PropertyField(itemCanUseProp);
        if (itemCanUseProp.boolValue)
        {
            EditorGUILayout.PropertyField(itemRestoreHpProp);
            EditorGUILayout.PropertyField(itemRestoreHungryProp);
            EditorGUILayout.PropertyField(itemRestoreWaterProp);
        }
        EditorGUILayout.PropertyField(itemCanCookProp);
        if (itemCanCookProp.boolValue)
        {
            EditorGUILayout.PropertyField(itemCompleteProp);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
