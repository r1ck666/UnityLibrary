using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Utility
{
    /// <summary>
    /// Mesh generator.
    /// </summary>
    public abstract class MeshGenerator : MonoBehaviour
    {

        #region Built-in Resources

        [SerializeField] protected string meshName = "";
        [SerializeField] string saveFolderPath = "Assets/";
        [SerializeField] bool isReplaced = true;

        #endregion

        #region Protected Functions

        protected bool SaveMeshAsAsset(Mesh mesh)
        {
            if (meshName == "" || saveFolderPath == "") return false;

            string saveAssetPath = saveFolderPath + meshName + ".asset";

            AssetDatabase.Refresh();

            if (!isReplaced && System.IO.File.Exists (saveAssetPath)) 
            {
                Debug.Log("Mesh already Exsists.");
                return false;
            }

            AssetDatabase.CreateAsset(mesh, saveAssetPath);
            AssetDatabase.SaveAssets();

            return true;
        }

        #endregion
    }
}
