using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Utility.Editor 
{
    public abstract class EditorListDrawer : UnityEditor.Editor
    {

        #region Private Resources

        bool isInitialized = false;
        bool folding_list = false;
        bool[] foldings;

        #endregion

        #region Abstract Functions

        protected abstract int ListCount();
        protected abstract void AddElement();
        protected abstract void RemoveElement(int i);
        protected abstract void CopyElement(int i);
        protected abstract void PasteElement(int i);
        protected abstract void ShowElement(int i);
        protected abstract string ListName();
        protected abstract string ElementName(int i);
        protected abstract void DeleteList();

        #endregion

        protected void DrawList()
        {
            // 初期化
            if (!isInitialized) InitializeList(ListCount());

            // Listを折りたたみ表示
            if (folding_list = EditorGUILayout.Foldout(folding_list, ListName()))
            {
                // インデントを増やす
                EditorGUI.indentLevel++;

                for (int i = 0; i < ListCount(); i++)
                {
                    // インデントを増やす
                    EditorGUI.indentLevel++;

                    // Listの要素を折りたたみ表示
                    if (foldings[i] = EditorGUILayout.Foldout(foldings[i], ElementName(i)))
                    {
                        ShowElement(i);

                        EditorGUILayout.BeginHorizontal();

                        GUILayout.FlexibleSpace();

                        CopyButton(i);
                        PasteButton(i);
                        RemoveButton(i);

                        EditorGUILayout.EndHorizontal();
                    }

                    // インデントを減らす
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.BeginHorizontal();

                GUILayout.FlexibleSpace();

                AddButton();
                RefreshButton();
                DeleteListButton();

                EditorGUILayout.EndHorizontal();

                // インデントを減らす
                EditorGUI.indentLevel--;
            }
        }

        #region Private Functions

        // Listの長さを初期化
        void InitializeList(int count)
        {
            foldings = new bool[count];
            isInitialized = true;
        }

        // 指定した番号以外をキャッシュして初期化 (i = -1の時は全てキャッシュして初期化)
        void UpdateList(int i, int count)
        {
            bool[] foldings_temp = foldings;
            foldings = new bool[count];

            for (int k = 0, j = 0; k < count; k++)
            {
                if (i == j) j++;
                if (foldings_temp.Length - 1 < j) break;
                foldings[k] = foldings_temp[j++];
            }
        }

        #endregion

        #region Protected Functions

        protected virtual void AddButton()
        {
            // Listの追加
            if (GUILayout.Button("Add"))
            {
                AddElement();

                UpdateList(-1, ListCount());
            }
        }

        protected virtual void RemoveButton(int i)
        {
            // 指定した要素を削除
            if (GUILayout.Button("Remove"))
            {
                RemoveElement(i);

                UpdateList(i, ListCount());
            }
        }

        protected virtual void CopyButton(int i)
        {
            if (GUILayout.Button("Copy"))
            {
                CopyElement(i);
            }
        }

        protected virtual void PasteButton(int i)
        {
            if (GUILayout.Button("Paste"))
            {
                // Paste
                PasteElement(i);

                var count = ListCount();
                bool[] foldings_temp = foldings;
                foldings = new bool[count];

                for (int k = 0, j = 0; k < count; k++)
                {
                    if (foldings_temp.Length - 1 < j) break;

                    if (i == k) foldings[k++] = true;
                    foldings[k] = foldings_temp[j++];
                }
            }
        }

        protected virtual void RefreshButton()
        {
            if (GUILayout.Button("Reflesh"))
            {
                {
                    isInitialized = false;
                }
            }
        }

        protected virtual void DeleteListButton()
        {
            if (GUILayout.Button("DeleteList"))
            {
                DeleteList();

                isInitialized = false;
            }
        }

        #endregion
    }   
}

