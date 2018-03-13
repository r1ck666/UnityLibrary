using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class GamePadEventProvider : MonoBehaviour, IInputEventProvider
    {

        enum PlatForm
        {
            MAC,
            WINDOWS,
            KEYBOARD,
            MAC_KEYBOARD,
            WINDOWS_KEYBOAD,
        }

        #region Built-in Resources

        [SerializeField] private PlatForm platform = PlatForm.MAC;
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
        */
        float trigger = 0.0f;

        #endregion

        #region Monobehaviour Functions

        void Start()
        {
            if (inputCtrl == null) inputCtrl = GetComponent<InputGamePadBaseController>();

            //Enumの個数の取得
            int length = System.Enum.GetNames(typeof(KeyType)).Length;
            isUsed = new bool[length];

        }

        void Update()
        {
            switch(platform)
            {
                case PlatForm.MAC:
                    CheckKeyDownForMac();
                    break;
                case PlatForm.WINDOWS:
                    CheckKeyDownForWindows();
                    break;
                case PlatForm.KEYBOARD:
                    CheckKeyDownForKeyborad();
                    break;
                case PlatForm.MAC_KEYBOARD:
                    CheckKeyDownForMacAndKeyBoard();
                    break;
                case PlatForm.WINDOWS_KEYBOAD:
                    CheckKeyDownForWindowsAndKeyBoard();
                    break;
                default:
                    break;
            }
        }

        void FixedUpdate()
        {
            // 各ボタンのフラグが立っていたらControllerのメソッドを実行
            inputCtrl.GetHorizontalL(Input.GetAxis("LeftStickX"));
            inputCtrl.GetVerticalL(Input.GetAxis("LeftStickY"));
            inputCtrl.GetHorizontalR(Input.GetAxis("RightStickX"));
            inputCtrl.GetVerticalR(Input.GetAxis("RightStickY"));
            inputCtrl.GetHorizontalC(Input.GetAxis(("CrossKeyX")));
            inputCtrl.GetVerticalC(Input.GetAxis(("CrossKeyY")));

            switch(platform)
            {
                case PlatForm.MAC:
                    inputCtrl.GetLTrigger(Input.GetAxis("LeftTrigger"));
                    inputCtrl.GetRTrigger(Input.GetAxis("RightTrigger"));
                    break;
                case PlatForm.WINDOWS:
                    trigger = Input.GetAxis("L/R Trigger");
                    if (trigger < 0) inputCtrl.GetLTrigger(trigger);
                    if (trigger > 0) inputCtrl.GetRTrigger(trigger);
                    break;
                default:
                    break;
            }

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

        #region Private Functions

        /// <summary>
        /// Checks the key down for windows.
        /// </summary>
        void CheckKeyDownForWindows()
        {
            // A Button
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                isUsed[(int)KeyType.A_DOWN] = true;
            }

            // B Button
            if (Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                isUsed[(int)KeyType.B_DOWN] = true;
            }

            // X Button
            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                isUsed[(int)KeyType.X_DOWN] = true;
            }

            // Y Button
            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                isUsed[(int)KeyType.Y_DOWN] = true;
            }

            // LB Button
            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                isUsed[(int)KeyType.LB_DOWN] = true;
            }

            // RB Button
            if (Input.GetKeyDown(KeyCode.JoystickButton5))
            {
                isUsed[(int)KeyType.RB_DOWN] = true;
            }

            // SELECT Button
            if (Input.GetKeyDown(KeyCode.JoystickButton6))
            {
                isUsed[(int)KeyType.SELECT_DOWN] = true;
            }

            // START Button
            if (Input.GetKeyDown(KeyCode.JoystickButton7))
            {
                isUsed[(int)KeyType.START_DOWN] = true;
            }

            // LStick Button
            if (Input.GetKeyDown(KeyCode.JoystickButton8))
            {
                isUsed[(int)KeyType.LS_BUTTON_DOWN] = true;
            }

            // RStick Button
            if (Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                isUsed[(int)KeyType.RS_BUTTON_DOWN] = true;
            }
        }

        /// <summary>
        /// Checks the key down for mac.
        /// </summary>
        void CheckKeyDownForMac()
        {
            // A Button
            if (Input.GetKeyDown(KeyCode.JoystickButton16))
            {
                isUsed[(int)KeyType.A_DOWN] = true;
            }

            // B Button
            if (Input.GetKeyDown(KeyCode.JoystickButton17))
            {
                isUsed[(int)KeyType.B_DOWN] = true;
            }

            // X Button
            if (Input.GetKeyDown(KeyCode.JoystickButton18))
            {
                isUsed[(int)KeyType.X_DOWN] = true;
            }

            // Y Button
            if (Input.GetKeyDown(KeyCode.JoystickButton19))
            {
                isUsed[(int)KeyType.Y_DOWN] = true;
            }

            // LB Button
            if (Input.GetKeyDown(KeyCode.JoystickButton13))
            {
                isUsed[(int)KeyType.LB_DOWN] = true;
            }

            // RB Button
            if (Input.GetKeyDown(KeyCode.JoystickButton14))
            {
                isUsed[(int)KeyType.RB_DOWN] = true;
            }

            // SELECT Button
            if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                isUsed[(int)KeyType.SELECT_DOWN] = true;
            }

            // START Button
            if (Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                isUsed[(int)KeyType.START_DOWN] = true;
            }

            // LStick Button
            if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                isUsed[(int)KeyType.LS_BUTTON_DOWN] = true;
            }

            // RStick Button
            if (Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                isUsed[(int)KeyType.RS_BUTTON_DOWN] = true;
            }
        }

        void CheckKeyDownForKeyborad()
        {
            /*
            // A Button (Space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isUsed[(int)KeyType.A_DOWN] = true;
            }
            */

            // B Button (F)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isUsed[(int)KeyType.B_DOWN] = true;
            }

            /*
            // X Button (E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                isUsed[(int)KeyType.X_DOWN] = true;
            }

            // Y Button (Q)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isUsed[(int)KeyType.Y_DOWN] = true;
            }
            */
            /*
            // LB Button
            if (Input.GetKeyDown(KeyCode.JoystickButton13))
            {
                isUsed[(int)KeyType.LB_DOWN] = true;
            }
            */

            // RB Button
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isUsed[(int)KeyType.RB_DOWN] = true;
            }

            /*
            // SELECT Button
            if (Input.GetKeyDown(KeyCode.JoystickButton10))
            {
                isUsed[(int)KeyType.SELECT_DOWN] = true;
            }

            // START Button
            if (Input.GetKeyDown(KeyCode.JoystickButton9))
            {
                isUsed[(int)KeyType.START_DOWN] = true;
            }

            // LStick Button
            if (Input.GetKeyDown(KeyCode.JoystickButton11))
            {
                isUsed[(int)KeyType.LS_BUTTON_DOWN] = true;
            }

            // RStick Button
            if (Input.GetKeyDown(KeyCode.JoystickButton12))
            {
                isUsed[(int)KeyType.RS_BUTTON_DOWN] = true;
            }
            */
        }

        void CheckKeyDownForMacAndKeyBoard()
        {
            CheckKeyDownForMac();
            CheckKeyDownForKeyborad();
        }

        void CheckKeyDownForWindowsAndKeyBoard()
        {
            CheckKeyDownForWindows();
            CheckKeyDownForKeyborad();
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Controllerをセット
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