using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Utility.Editor
{
    public class EditorGUILayoutExtension
    {
        /// <summary>
        /// 編集できないVector3のField
        /// </summary>
        /// <returns>The vector3 field.</returns>
        /// <param name="label">Label.</param>
        /// <param name="value">Value.</param>
        public static Vector3 DisableAllVector3Field(string label, Vector3 value)
        {

            EditorGUI.BeginDisabledGroup(true);

            value = EditorGUILayout.Vector3Field(label, value);

            EditorGUI.EndDisabledGroup();

            return value;
        }

        /// <summary>
        /// Xが編集できないVector3のField
        /// TODO:処理自体はほとんどコピーなので、無駄な処理を減らす
        /// </summary>
        /// <returns>The XV ector3 field.</returns>
        /// <param name="labelText">Label text.</param>
        /// <param name="value">Value.</param>
        public static Vector3 DisableXVector3Field(string labelText, Vector3 value)
        {
            var label = new GUIContent(labelText);
            var position = EditorGUILayout.GetControlRect(true, EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, new GUIContent()), EditorStyles.numberField);


            var indent = EditorGUI.indentLevel * 15f;
            if (EditorGUIUtility.wideMode)
            {
                Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, 16f);

                Rect rect = position;
                rect.xMin += EditorGUIUtility.labelWidth;
                labelPosition.width -= 1f;
                rect.xMin -= 1f;

                EditorGUI.LabelField(labelPosition, label);

                position = rect;
            }
            else
            {
                Rect labelPosition2 = new Rect(position.x, position.y, position.width, 16f);
                Rect rect2 = position;
                rect2.xMin += indent + 15f;
                rect2.yMin += 16f;

                EditorGUI.LabelField(labelPosition2, label);

                position = rect2;
            }

            float num2 = (position.width - 2f * 2f) / 3.0f;
            Rect position2 = new Rect(position);
            position2.width = num2;
            float labelWidth2 = EditorGUIUtility.labelWidth;
            int indentLevel = EditorGUI.indentLevel;
            EditorGUIUtility.labelWidth = 13f;
            EditorGUI.indentLevel = 0;

            EditorGUI.BeginDisabledGroup(true);

            value.x = EditorGUI.FloatField(position2, new GUIContent("X"), value.x);
            position2.x += num2 + 2f;

            EditorGUI.EndDisabledGroup();

            value.y = EditorGUI.FloatField(position2, new GUIContent("Y"), value.y);
            position2.x += num2 + 2f;

            value.z = EditorGUI.FloatField(position2, new GUIContent("Z"), value.z);
            position2.x += num2 + 2f;

            EditorGUIUtility.labelWidth = labelWidth2;
            EditorGUI.indentLevel = indentLevel;

            return value;
        }

        public static Vector3 DisableYVector3Field(string labelText, Vector3 value)
        {
            var label = new GUIContent(labelText);
            var position = EditorGUILayout.GetControlRect(true, EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, new GUIContent()), EditorStyles.numberField);
            var indent = EditorGUI.indentLevel * 15f;

            if (EditorGUIUtility.wideMode)
            {
                Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, 16f);

                Rect rect = position;
                rect.xMin += EditorGUIUtility.labelWidth;
                labelPosition.width -= 1f;
                rect.xMin -= 1f;

                EditorGUI.LabelField(labelPosition, label);

                position = rect;
            }
            else
            {
                Rect labelPosition2 = new Rect(position.x, position.y, position.width, 16f);
                Rect rect2 = position;
                rect2.xMin += indent + 15f;
                rect2.yMin += 16f;

                EditorGUI.LabelField(labelPosition2, label);

                position = rect2;
            }

            float num2 = (position.width - 2f * 2f) / 3.0f;
            Rect position2 = new Rect(position);
            position2.width = num2;
            float labelWidth2 = EditorGUIUtility.labelWidth;
            int indentLevel = EditorGUI.indentLevel;
            EditorGUIUtility.labelWidth = 13f;
            EditorGUI.indentLevel = 0;

            EditorGUI.BeginDisabledGroup(true);

            value.x = EditorGUI.FloatField(position2, new GUIContent("X"), value.x);
            position2.x += num2 + 2f;

            EditorGUI.BeginDisabledGroup(true);

            value.y = EditorGUI.FloatField(position2, new GUIContent("Y"), value.y);
            position2.x += num2 + 2f;

            EditorGUI.EndDisabledGroup();

            value.z = EditorGUI.FloatField(position2, new GUIContent("Z"), value.z);
            position2.x += num2 + 2f;

            EditorGUIUtility.labelWidth = labelWidth2;
            EditorGUI.indentLevel = indentLevel;

            return value;
        }

        public static Vector3 DisableZVector3Field(string labelText, Vector3 value)
        {
            var label = new GUIContent(labelText);
            var position = EditorGUILayout.GetControlRect(true, EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, new GUIContent()), EditorStyles.numberField);
            var indent = EditorGUI.indentLevel * 15f;

            if (EditorGUIUtility.wideMode)
            {
                Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, 16f);

                Rect rect = position;
                rect.xMin += EditorGUIUtility.labelWidth;
                labelPosition.width -= 1f;
                rect.xMin -= 1f;

                EditorGUI.LabelField(labelPosition, label);

                position = rect;
            }
            else
            {
                Rect labelPosition2 = new Rect(position.x, position.y, position.width, 16f);
                Rect rect2 = position;
                rect2.xMin += indent + 15f;
                rect2.yMin += 16f;

                EditorGUI.LabelField(labelPosition2, label);

                position = rect2;
            }

            float num2 = (position.width - 2f * 2f) / 3.0f;
            Rect position2 = new Rect(position);
            position2.width = num2;
            float labelWidth2 = EditorGUIUtility.labelWidth;
            int indentLevel = EditorGUI.indentLevel;
            EditorGUIUtility.labelWidth = 13f;
            EditorGUI.indentLevel = 0;

            value.x = EditorGUI.FloatField(position2, new GUIContent("X"), value.x);
            position2.x += num2 + 2f;

            value.y = EditorGUI.FloatField(position2, new GUIContent("Y"), value.y);
            position2.x += num2 + 2f;

            EditorGUI.BeginDisabledGroup(true);

            value.z = EditorGUI.FloatField(position2, new GUIContent("Z"), value.z);
            position2.x += num2 + 2f;

            EditorGUI.EndDisabledGroup();

            EditorGUIUtility.labelWidth = labelWidth2;
            EditorGUI.indentLevel = indentLevel;

            return value;
        }

        public static Vector3 DisableXYVector3Field(string labelText, Vector3 value)
        {
            var label = new GUIContent(labelText);
            var position = EditorGUILayout.GetControlRect(true, EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, new GUIContent()), EditorStyles.numberField);
            var indent = EditorGUI.indentLevel * 15f;

            if (EditorGUIUtility.wideMode)
            {
                Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, 16f);

                Rect rect = position;
                rect.xMin += EditorGUIUtility.labelWidth;
                labelPosition.width -= 1f;
                rect.xMin -= 1f;

                EditorGUI.LabelField(labelPosition, label);

                position = rect;
            }
            else
            {
                Rect labelPosition2 = new Rect(position.x, position.y, position.width, 16f);
                Rect rect2 = position;
                rect2.xMin += indent + 15f;
                rect2.yMin += 16f;

                EditorGUI.LabelField(labelPosition2, label);

                position = rect2;
            }

            float num2 = (position.width - 2f * 2f) / 3.0f;
            Rect position2 = new Rect(position);
            position2.width = num2;
            float labelWidth2 = EditorGUIUtility.labelWidth;
            int indentLevel = EditorGUI.indentLevel;
            EditorGUIUtility.labelWidth = 13f;
            EditorGUI.indentLevel = 0;

            EditorGUI.BeginDisabledGroup(true);

            value.x = EditorGUI.FloatField(position2, new GUIContent("X"), value.x);
            position2.x += num2 + 2f;

            value.y = EditorGUI.FloatField(position2, new GUIContent("Y"), value.y);
            position2.x += num2 + 2f;

            EditorGUI.EndDisabledGroup();

            value.z = EditorGUI.FloatField(position2, new GUIContent("Z"), value.z);
            position2.x += num2 + 2f;

            EditorGUIUtility.labelWidth = labelWidth2;
            EditorGUI.indentLevel = indentLevel;

            return value;
        }

        public static Vector3 DisableXZVector3Field(string labelText, Vector3 value)
        {
            var label = new GUIContent(labelText);
            var position = EditorGUILayout.GetControlRect(true, EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, new GUIContent()), EditorStyles.numberField);
            var indent = EditorGUI.indentLevel * 15f;

            if (EditorGUIUtility.wideMode)
            {
                Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, 16f);

                Rect rect = position;
                rect.xMin += EditorGUIUtility.labelWidth;
                labelPosition.width -= 1f;
                rect.xMin -= 1f;

                EditorGUI.LabelField(labelPosition, label);

                position = rect;
            }
            else
            {
                Rect labelPosition2 = new Rect(position.x, position.y, position.width, 16f);
                Rect rect2 = position;
                rect2.xMin += indent + 15f;
                rect2.yMin += 16f;

                EditorGUI.LabelField(labelPosition2, label);

                position = rect2;
            }

            float num2 = (position.width - 2f * 2f) / 3.0f;
            Rect position2 = new Rect(position);
            position2.width = num2;
            float labelWidth2 = EditorGUIUtility.labelWidth;
            int indentLevel = EditorGUI.indentLevel;
            EditorGUIUtility.labelWidth = 13f;
            EditorGUI.indentLevel = 0;

            EditorGUI.BeginDisabledGroup(true);

            value.x = EditorGUI.FloatField(position2, new GUIContent("X"), value.x);
            position2.x += num2 + 2f;

            EditorGUI.EndDisabledGroup();

            value.y = EditorGUI.FloatField(position2, new GUIContent("Y"), value.y);
            position2.x += num2 + 2f;

            EditorGUI.BeginDisabledGroup(true);

            value.z = EditorGUI.FloatField(position2, new GUIContent("Z"), value.z);
            position2.x += num2 + 2f;

            EditorGUI.EndDisabledGroup();

            EditorGUIUtility.labelWidth = labelWidth2;
            EditorGUI.indentLevel = indentLevel;

            return value;
        }

        public static Vector3 DisableYZVector3Field(string labelText, Vector3 value)
        {
            var label = new GUIContent(labelText);
            var position = EditorGUILayout.GetControlRect(true, EditorGUI.GetPropertyHeight(SerializedPropertyType.Vector3, new GUIContent()), EditorStyles.numberField);
            var indent = EditorGUI.indentLevel * 15f;

            if (EditorGUIUtility.wideMode)
            {
                Rect labelPosition = new Rect(position.x, position.y, EditorGUIUtility.labelWidth, 16f);

                Rect rect = position;
                rect.xMin += EditorGUIUtility.labelWidth;
                labelPosition.width -= 1f;
                rect.xMin -= 1f;

                EditorGUI.LabelField(labelPosition, label);

                position = rect;
            }
            else
            {
                Rect labelPosition2 = new Rect(position.x, position.y, position.width, 16f);
                Rect rect2 = position;
                rect2.xMin += indent + 15f;
                rect2.yMin += 16f;

                EditorGUI.LabelField(labelPosition2, label);

                position = rect2;
            }

            float num2 = (position.width - 2f * 2f) / 3.0f;
            Rect position2 = new Rect(position);
            position2.width = num2;
            float labelWidth2 = EditorGUIUtility.labelWidth;
            int indentLevel = EditorGUI.indentLevel;
            EditorGUIUtility.labelWidth = 13f;
            EditorGUI.indentLevel = 0;


            value.x = EditorGUI.FloatField(position2, new GUIContent("X"), value.x);
            position2.x += num2 + 2f;

            EditorGUI.BeginDisabledGroup(true);

            value.y = EditorGUI.FloatField(position2, new GUIContent("Y"), value.y);
            position2.x += num2 + 2f;

            value.z = EditorGUI.FloatField(position2, new GUIContent("Z"), value.z);
            position2.x += num2 + 2f;

            EditorGUI.EndDisabledGroup();

            EditorGUIUtility.labelWidth = labelWidth2;
            EditorGUI.indentLevel = indentLevel;

            return value;
        }
    }
}
