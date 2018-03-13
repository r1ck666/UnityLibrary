using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputs 
{
    public class DebugEventProvider : MonoBehaviour, IInputEventProvider
    {

        #region Built-in Resources

        [SerializeField] private InputGamePadBaseController inputCtrl;

        #endregion

        #region Private Resources

        // 実行されたかどうかのフラグ
        bool[] isUsed;

        /*
        float horizontalL = 0.0f;
        float verticalL = 0.0f;
        float horizontalR = 0.0f;
        float verticalR = 0.0f;
        float horizontalC = 0.0f;
        float verticalC = 0.0f;
        float trigger = 0.0f;
        */

        #endregion

        #region Monobehaviour Functions

        void Start()
        {
            //Enumの個数の取得
            int length = System.Enum.GetNames(typeof(KeyType)).Length;
            isUsed = new bool[length];
        }

        void Update()
        {
            // A Button(Space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isUsed[(int)KeyType.A_DOWN] = true;
            }

            // B Button(Left Click)
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isUsed[(int)KeyType.B_DOWN] = true;
            }

            // X Button(Right Click)
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isUsed[(int)KeyType.X_DOWN] = true;
            }

            // Y Button (E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                isUsed[(int)KeyType.Y_DOWN] = true;
            }
        }

        void FixedUpdate()
        {
            // 各ボタンのフラグが立っていたらControllerのメソッドを実行
            inputCtrl.GetHorizontalL(Input.GetAxis("LeftStickX"));
            inputCtrl.GetVerticalL(Input.GetAxis("LeftStickY"));
            inputCtrl.GetHorizontalR(Input.GetAxis("RightStickX"));
            inputCtrl.GetVerticalR(Input.GetAxis("RightStickY"));

            foreach (KeyType keytype in System.Enum.GetValues(typeof(KeyType)))
            {
                if (isUsed[(int)keytype])
                {
                    inputCtrl.GetKey(keytype);
                    isUsed[(int)keytype] = false;
                }
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Controllerをセットします
        /// </summary>
        /// <param name="inputCtrl"></param>
        /// <returns></returns>
        public bool SetInputController(InputGamePadBaseController inputCtrl)
        {
            if (inputCtrl is InputGamePadBaseController)
            {
                this.inputCtrl = inputCtrl;
                return true;
            }
            return false;
        }

        #endregion

    }
}
