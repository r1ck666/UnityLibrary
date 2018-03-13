#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Assets.Scripts.Utility.Editor
{
    /// <summary>
    /// InputManagerを自動的に設定してくれるクラス
    /// </summary>
    public class InputManagerSetter
    {
        /// <summary>
        /// インプットマネージャーを再設定します。
        /// </summary>
        //[MenuItem("Utility/Reset InputManager")]
        public static void ResetInputManager()
        {
            Debug.Log("インプットマネージャーの設定を開始します。");
            InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

            Debug.Log("設定を全てクリアします。");
            inputManagerGenerator.Clear();

            Debug.Log("プレイヤーごとの設定を追加します。");

            for (int i = 0; i < 4; i++)
            {
                AddPlayerInputSettings(inputManagerGenerator, i);
            }


            Debug.Log("グローバル設定を追加します。");
            AddGlobalInputSettings(inputManagerGenerator);

            Debug.Log("インプットマネージャーの設定が完了しました。");
        }

        /// <summary>
        /// XBOX360 Controller(Windows)の入力設定を追加
        /// </summary>
        [MenuItem("Utility/Add XBOX360 Controller Settings to Inputmanager for Windows")]
        private static void AddXBOXControllerInputSettingsForWindows()
        {
            InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

            // LeftStickX
            {
                var name = "LeftStickX";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
            }

            // LeftStickY
            {
                var name = "LeftStickY";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 2));
            }

            // RightStickX
            {
                var name = "RightStickX";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 4));
            }

            // RightStickY
            {
                var name = "RightStickY";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 5));
            }

            // CrossKeyX
            {
                var name = "CrossKeyX";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 6));
            }

            // CrossKeyY
            {
                var name = "CrossKeyY";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 7));
            }

            // L/R Trigger
            {
                var name = "L/R Trigger";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 3));
            }
        }

        /// <summary>
        /// XBOX360 Controller(MacOSX)の入力設定を追加
        /// </summary>
        [MenuItem("Utility/Add XBOX360 Controller Settings to InputManager for MacOSX")]
        private static void AddXBOXControllerInputSettingsForMacOSX()
        {
            InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

            // LeftStickX
            {
                var name = "LeftStickX";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
            }

            // LeftStickY
            {
                var name = "LeftStickY";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 2, false, true));
            }

            // RightStickX
            {
                var name = "RightStickX";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 3));
            }

            // RightStickY
            {
                var name = "RightStickY";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 4));
            }

            // LeftTrigger
            {
                var name = "LeftTrigger";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 5));
            }

            // RightTrigger
            {
                var name = "RightTrigger";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 6));
            }

            // CrossKeyX
            {
                var name = "CrossKeyX";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "joystick button 7", "joystick button 8", "", ""));
            }

            // CrossKeyY
            {
                var name = "CrossKeyY";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "joystick button 6", "joystick button 5", "", ""));
            }

        }

        /// <summary>
        /// XBOX360 Controller(MacOSX)の入力設定を追加
        /// </summary>
        [MenuItem("Utility/Add XBOX360 Controller Settings to InputManager for KeyBoard")]
        private static void AddXBOXControllerInputSettingsForKeyBorad()
        {
            InputManagerGenerator inputManagerGenerator = new InputManagerGenerator();

            // LeftStickX
            {
                var name = "LeftStickX";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "", ""));
            }

            // LeftStickY
            {
                var name = "LeftStickY";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "s", "w", "", ""));
            }

            // RightStickX
            {
                var name = "RightStickX";
                inputManagerGenerator.AddAxis(InputAxis.CreateMouseAxis(name, 1));
            }

            // RightStickX
            {
                var name = "RightStickX";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "left", "right", "", ""));
            }

            // RightStickY
            {
                var name = "RightStickY";
                inputManagerGenerator.AddAxis(InputAxis.CreateMouseAxis(name, 2));
            }

            // RightStickY
            {
                var name = "RightStickY";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "down", "up", "", ""));
            }

            /*

            // LeftTrigger
            {
                var name = "LeftTrigger";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 5));
            }

            // RightTrigger
            {
                var name = "RightTrigger";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 6));
            }

            // CrossKeyX
            {
                var name = "CrossKeyX";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "joystick button 7", "joystick button 8", "", ""));
            }

            // CrossKeyY
            {
                var name = "CrossKeyY";
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "joystick button 6", "joystick button 5", "", ""));
            }

            */

        }

        /// <summary>
        /// グローバルな入力設定を追加する（OK、キャンセルなど）
        /// </summary>
        /// <param name="inputManagerGenerator">Input manager generator.</param>
        private static void AddGlobalInputSettings(InputManagerGenerator inputManagerGenerator)
        {

            // 横方向
            {
                var name = "Horizontal";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 1));
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "a", "d", "left", "right"));
            }

            // 縦方向
            {
                var name = "Vertical";
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, 0, 2));
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, "s", "w", "down", "up"));
            }

            // 決定
            {
                var name = "OK";
                inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "z", "joystick button 0"));
            }

            // キャンセル
            {
                var name = "Cancel";
                inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "x", "joystick button 1"));
            }

            // ポーズ
            {
                var name = "Pause";
                inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, "escape", "joystick button 7"));
            }
        }

        /// <summary>
        /// プレイヤーごとの入力設定を追加する
        /// </summary>
        /// <param name="inputManagerGenerator">Input manager generator.</param>
        /// <param name="playerIndex">Player index.</param>
        private static void AddPlayerInputSettings(InputManagerGenerator inputManagerGenerator, int playerIndex)
        {
            if (playerIndex < 0 || playerIndex > 3) Debug.LogError("プレイヤーインデックスの値が不正です。");
            string upKey = "", downKey = "", leftKey = "", rightKey = "", attackKey = "";
            GetAxisKey(out upKey, out downKey, out leftKey, out rightKey, out attackKey, playerIndex);

            int joystickNum = playerIndex + 1;

            // 横方向
            {
                var name = string.Format("Player{0} Horizontal", playerIndex);
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 1));
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, leftKey, rightKey, "", ""));
            }

            // 縦方向
            {
                var name = string.Format("Player{0} Vertical", playerIndex);
                inputManagerGenerator.AddAxis(InputAxis.CreatePadAxis(name, joystickNum, 2));
                inputManagerGenerator.AddAxis(InputAxis.CreateKeyAxis(name, downKey, upKey, "", ""));
            }


            // 攻撃
            {
                //var axis = new InputAxis();
                var name = string.Format("Player{0} Attack", playerIndex);
                var button = string.Format("joystick {0} button 0", joystickNum);
                inputManagerGenerator.AddAxis(InputAxis.CreateButton(name, button, attackKey));
            }
        }

        /// <summary>
        /// キーボードでプレイした場合、割り当たっているキーを取得する
        /// </summary>
        /// <param name="upKey">Up key.</param>
        /// <param name="downKey">Down key.</param>
        /// <param name="leftKey">Left key.</param>
        /// <param name="rightKey">Right key.</param>
        /// <param name="attackKey">Attack key.</param>
        /// <param name="playerIndex">Player index.</param>
        private static void GetAxisKey(out string upKey, out string downKey, out string leftKey, out string rightKey, out string attackKey, int playerIndex)
        {
            upKey = "";
            downKey = "";
            leftKey = "";
            rightKey = "";
            attackKey = "";

            switch (playerIndex)
            {
                case 0:
                    upKey = "w";
                    downKey = "s";
                    leftKey = "a";
                    rightKey = "d";
                    attackKey = "e";
                    break;
                case 1:
                    upKey = "i";
                    downKey = "k";
                    leftKey = "j";
                    rightKey = "l";
                    attackKey = "o";
                    break;
                case 2:
                    upKey = "up";
                    downKey = "down";
                    leftKey = "left";
                    rightKey = "right";
                    attackKey = "[0]";
                    break;
                case 3:
                    upKey = "[8]";
                    downKey = "[5]";
                    leftKey = "[4]";
                    rightKey = "[6]";
                    attackKey = "[9]";
                    break;
                default:
                    Debug.LogError("プレイヤーインデックスの値が不正です。");
                    upKey = "";
                    downKey = "";
                    leftKey = "";
                    rightKey = "";
                    attackKey = "";
                    break;
            }
        }
    }
}

#endif
