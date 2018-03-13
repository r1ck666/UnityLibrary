using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class FollowCamera : MonoBehaviour
    {

        enum InputState{
            Auto,
            Manual,
        }

        #region Parameter

        [SerializeField] float distance = 5.0f; // プレイヤーとカメラの距離
        [SerializeField] float sensitivity_x = 10.0f; // マウス感度X
        [SerializeField] float sensitivity_y = 10.0f; // マウス感度Y
        [SerializeField] Vector3 offset = Vector3.zero;
        [SerializeField] InputState inputState = InputState.Auto;


        #endregion

        #region Built-in Resources

        [SerializeField] Transform lookTarget;

        #endregion

        #region public Properties (SetOnly)

        [SerializeField] float _inputX;
        public float InputX {
            set { _inputX = value; }
        }

        [SerializeField] float _inputY;
        public float InputY {
            set { _inputY = value; }
        }


        #endregion

        #region Private Resources

        float _horizontalAngle = 0.0f;
        float _verticalAngle = 0.0f;

        #endregion

        #region MonoBehaviour Functions

        void Start()
        {
            CameraReset();
        }

        void Update()
        {
            if (inputState != InputState.Auto) return;

            //カメラリセット
            if (Input.GetKeyDown(KeyCode.F))
            {
                CameraReset();
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (inputState == InputState.Auto)
            {
                _inputX = Input.GetAxis("Mouse X");
                _inputY = Input.GetAxis("Mouse Y");
            }
            _horizontalAngle += _inputX * sensitivity_x;
            _horizontalAngle = Mathf.Repeat(_horizontalAngle, 360.0f);
            _verticalAngle += _inputY * sensitivity_y;
            _verticalAngle = Mathf.Clamp(_verticalAngle, -60.0f, 60.0f);

            // カメラを位置と回転を更新する.
            if (lookTarget != null)
            {
                Vector3 lookPosition = lookTarget.position + offset;
                // 注視対象からの相対位置を求める.
                Vector3 relativePos = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0) * new Vector3(0, 0, -distance);

                // 注視対象の位置にオフセット加算した位置に移動させる.
                transform.position = lookPosition + relativePos;

                // 注視対象を注視させる.
                transform.LookAt(lookPosition);

                // 障害物を避ける.
                RaycastHit hitInfo;
                if (Physics.Linecast(lookPosition, transform.position, out hitInfo, 1 << LayerMask.NameToLayer("Ground")))
                    transform.position = hitInfo.point;
            }

        }

        #endregion

        #region Public Functions

        //カメラ位置リセット
        public void CameraReset()
        {
            _verticalAngle = 10.0f;
            _horizontalAngle = lookTarget.eulerAngles.y;
        }

        #endregion

    }
}