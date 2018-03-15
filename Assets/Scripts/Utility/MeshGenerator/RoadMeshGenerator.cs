using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO:機能の整理、Math.BazierPointの利用など

namespace Assets.Scripts.Utility
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class RoadMeshGenerator : MeshGenerator
    {
        #region Built-in Resources

        [SerializeField] bool isSave = true;
        [SerializeField] int vertexCount = 11;
        [SerializeField] float width = 10.0f;
        [SerializeField] float length = 50.0f;
        //[SerializeField] float centerOffset = 0.0f;
        [SerializeField] Material material = null;

        [SerializeField] Vector3 point0 = new Vector3(0, 0, 0);
        [SerializeField] Vector3 point1 = new Vector3(0, 0, 0);
        [SerializeField] Vector3 point2 = new Vector3(0, 0, 0);
        [SerializeField] Vector3 point3 = new Vector3(0, 0, 0);

        #endregion

        #region Private Resourses

        MeshFilter m_filter = null;
        MeshRenderer m_Renderer = null;

        #endregion

        #region MonoBehaviour Functions

        // Use this for initialization
        void Start()
        {
            m_filter = GetComponent<MeshFilter>();
            m_Renderer = GetComponent<MeshRenderer>();

            //Meshが存在しない場合は作成する
            if (m_filter.sharedMesh == null)
            {
                if (isSave)
                {
                    //CreateAndSaveRoadMesh(vertexCount, width, length, centerOffset);
                    CreateAndSaveBazierRoadMesh(vertexCount, width, length, point0, point1, point2, point3);
                }
                else
                {
                    //CreateRoadMesh(vertexCount, width, length, centerOffset);
                    CreateBazierRoadMesh(vertexCount, width, length, point0, point1, point2, point3);
                }
            }

            if(material != null) m_Renderer.material = material;
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// RoadMeshを作成します
        /// </summary>
        /// <returns>The road mesh.</returns>
        /// <param name="vCount">頂点数</param>
        /// <param name="w">幅</param>
        /// <param name="len">長さ</param>
        /// <param name="offset">offset = -length / 2 とすると中心が中央になる</param>
        Mesh CreateRoadMesh(int vCount, float w, float len, float offset)
        {
            Mesh mesh = new Mesh();
            mesh.name = base.meshName;

            // 頂点座標
            var vertices = new Vector3[vCount * 2];
            var distance = len / (vCount - 1);
            for (int i = 0; i < vCount; i++)
            {
                vertices[i * 2] = new Vector3(-w / 2, 0, offset + i * distance);
                vertices[i * 2 + 1] = new Vector3(w / 2, 0, offset + i * distance);

            }
            mesh.vertices = vertices;

            // uv座標
            var uvs = new Vector2[vCount * 2];
            for (int i = 0; i < vCount; i++)
            {
                uvs[i * 2] = new Vector2(-1, i / (vCount - 1));
                uvs[i * 2] = new Vector2(1, i / (vCount - 1));
            }
            mesh.uv = uvs;

            // 頂点の順番
            int l = 0, kTL = 2, kTR = 3, kBL = 0, kBR = 1; 
            var triangles = new int[6 * (vCount - 1)];
            for (int i = 0; i < vCount - 1; i++)
            {
                triangles[l++] = kBL++;
                triangles[l++] = kTL;
                triangles[l++] = kBR;
                triangles[l++] = kTR++;
                triangles[l++] = kBR++;
                triangles[l++] = kTL++;
                kTL++; kTR++; kBL++; kBR++;
            }
            mesh.triangles = triangles;

            // 法線計算
            mesh.RecalculateNormals();

            m_filter.sharedMesh = mesh;

            return mesh;
        }

        /// <summary>
        /// 平面のMeshを作成
        /// </summary>
        /// <returns>The plane mesh.</returns>
        /// <param name="w">横幅</param>
        /// <param name="len">長さ</param>
        /// <param name="divX">x軸の分割数.</param>
        /// <param name="divY">y軸の分割数</param>
        Mesh CreatePlaneMesh(float w, float len, int divX, int divY)
        {
            return new Mesh();
        }

        Mesh CreateBazierRoadMesh(int vCount, float w, float len, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Mesh mesh = new Mesh();
            mesh.name = base.meshName;

            // 頂点座標
            var vertices = new Vector3[vCount * 2];
            //var distance = len / (vCount - 1);
            for (int i = 0; i < vCount; i++)
            {
                // 分割数
                var t = (float)i / (vCount - 1);

                // BazierSloap
                var bSloap =  -3.0f * (t - 1.0f) * (t - 1.0f) * p0 +
                3.0f * (3.0f * t - 1.0f) * (t - 1.0f) * p1 +
                -3.0f * (3.0f * t - 2.0f) * t * p2 +
                3.0f * t * t * p3;
                
                // BazierPoint
                var oneMinusT = 1f - t;
                var bPoint = oneMinusT * oneMinusT * oneMinusT * p0 +
                       3f * oneMinusT * oneMinusT * t * p1 +
                       3f * oneMinusT * t * t * p2 +
                       t * t * t * p3;
                
                var cross = Vector3.Normalize(Vector3.Cross(bSloap, Vector3.up));
                if (cross.x < 0) cross *= -1.0f;

                vertices[i * 2] = -w / 2 * cross + bPoint;
                vertices[i * 2 + 1] = w / 2 * cross + bPoint;
            }
            mesh.vertices = vertices;

            // uv座標
            var uvs = new Vector2[vCount * 2];
            for (int i = 0; i < vCount; i++)
            {
                uvs[i * 2] = new Vector2(-1, i / (vCount - 1));
                uvs[i * 2] = new Vector2(1, i / (vCount - 1));
            }
            mesh.uv = uvs;

            // 頂点の順番
            int l = 0, kTL = 2, kTR = 3, kBL = 0, kBR = 1;
            var triangles = new int[6 * (vCount - 1)];
            for (int i = 0; i < vCount - 1; i++)
            {
                triangles[l++] = kBL++;
                triangles[l++] = kTL;
                triangles[l++] = kBR;
                triangles[l++] = kTR++;
                triangles[l++] = kBR++;
                triangles[l++] = kTL++;
                kTL++; kTR++; kBL++; kBR++;
            }
            mesh.triangles = triangles;

            // 法線計算
            mesh.RecalculateNormals();

            m_filter.sharedMesh = mesh;

            return mesh;
        }

        /// <summary>
        /// Meshを作成してAssetとして保存します
        /// </summary>
        Mesh CreateAndSaveRoadMesh(int vCount, float w, float len, float offset)
        {
            var mesh = CreateRoadMesh(vCount, w, len, offset);
            SaveMeshAsAsset(mesh);

            return mesh;
        }

        Mesh CreateAndSaveBazierRoadMesh(int vCount, float w, float len, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var mesh = CreateBazierRoadMesh(vCount, w, len, p0, p1, p2, p3);
            SaveMeshAsAsset(mesh);

            return mesh;
        }

        #endregion

    }
}