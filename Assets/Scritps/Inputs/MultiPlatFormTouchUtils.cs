using UnityEngine;
using Assets.Scripts.Utility;


namespace Assets.Scripts.Inputs
{
    public class MultiPlatformTouchUtils
    {
        private static bool isEditor = Application.isEditor;

        private static Vector3 TouchPosition = Vector3.zero;
        private static Vector3 PreviousPosition = Vector3.zero;
        private static float PreviousTime = 0.0f;

        public static int touchCount
        {
            get
            {
                if (isEditor)
                {
                    if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return Input.touchCount;
                }
            }
        }

        /// <summary>
        /// タッチ情報を取得(エディタと実機を考慮)
        /// </summary>
        /// <returns>タッチ情報。タッチされていない場合は null</returns>
        public static TouchInfo GetTouch(int i)
        {
            if (isEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    return TouchInfo.Began;
                }
                if (Input.GetMouseButton(0))
                {
                    return TouchInfo.Moved;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    return TouchInfo.Ended;
                }
            }
            else
            {
                if (Input.touchCount >= i)
                {
                    return (TouchInfo)((int)Input.GetTouch(i).phase);
                }
            }
            return TouchInfo.None;
        }

        /// <summary>
        /// タッチポジションを取得(エディタと実機を考慮)
        /// </summary>
        /// <returns>タッチポジション。タッチされていない場合は (0, 0, 0)</returns>
        public static Vector3 GetTouchPosition(int i)
        {
            if (isEditor)
            {
                TouchInfo touch = MultiPlatformTouchUtils.GetTouch(i);
                if (touch != TouchInfo.None)
                {
                    PreviousPosition = Input.mousePosition;
                    return PreviousPosition;
                }
            }
            else
            {
                if (Input.touchCount >= i)
                {
                    Touch touch = Input.GetTouch(i);
                    TouchPosition.x = touch.position.x;
                    TouchPosition.y = touch.position.y;
                    return TouchPosition;
                }
            }
            return Vector3.zero;
        }

        /// <summary>
        /// 1フレーム間のdeltaPositionを返す
        /// タッチしてから離すまでのdeltaを計算する場合は、Dictionary<int fingerId Vector3 sum>で毎フレーム加算すると良い
        /// </summary>
        /// <returns>The delta position</returns>
        /// <param name="i">touch index</param>
        public static Vector3 GetDeltaPosition(int i)
        {
            if (isEditor)
            {
                TouchInfo info = MultiPlatformTouchUtils.GetTouch(i);
                if (info != TouchInfo.None)
                {
                    Vector3 currentPosition = Input.mousePosition;
                    Vector3 delta = currentPosition - PreviousPosition;
                    PreviousPosition = currentPosition;
                    return delta;
                }
            }
            else
            {
                if (Input.touchCount >= i)
                {
                    Touch touch = Input.GetTouch(i);
                    PreviousPosition.x = touch.deltaPosition.x;
                    PreviousPosition.y = touch.deltaPosition.y;
                    return PreviousPosition;
                }
            }
            return Vector3.zero;
        }

        /// <summary>
        /// 前回の呼び出しからのdeltaTimeを返します。(実機は1フレーム間）
        /// </summary>
        /// <returns>The delta time.</returns>
        /// <param name="i">The index.</param>
        public static float GetDeltaTime(int i)
        {
            if (isEditor)
            {
                TouchInfo info = MultiPlatformTouchUtils.GetTouch(i);
                if (info != TouchInfo.None)
                {
                    float currentTime = Time.time;
                    float deltaTime = currentTime - PreviousTime;
                    PreviousTime = currentTime;
                    return deltaTime;
                }
            }
            else
            {
                if (Input.touchCount >= i)
                {
                    Touch touch = Input.GetTouch(i);
                    float deltaTime = touch.deltaTime;
                    return deltaTime;
                }
            }
            return 0.0f;
        }

        public static int getFingerId(int i)
        {
            if (isEditor)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return Input.GetTouch(i).fingerId;
            }
        }

        /// <summary>
        /// タッチワールドポジションを取得(エディタと実機を考慮)
        /// </summary>
        /// <param name='camera'>カメラ</param>
        /// <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0)</returns>
        public static Vector3 GetTouchWorldPosition(UnityEngine.Camera camera, int i)
        {
            return camera.ScreenToWorldPoint(GetTouchPosition(i));
        }
    }

    /// <summary>
    /// タッチ情報。UnityEngine.TouchPhase に None の情報を追加拡張。
    /// </summary>
    public enum TouchInfo
    {
        /// <summary>
        /// タッチなし
        /// </summary>
        None = -1,

        // 以下は UnityEngine.TouchPhase の値に対応
        /// <summary>
        /// タッチ開始
        /// </summary>
        Began = 0,
        /// <summary>
        /// タッチ移動
        /// </summary>
        Moved = 1,
        /// <summary>
        /// タッチ静止
        /// </summary>
        Stationary = 2,
        /// <summary>
        /// タッチ終了
        /// </summary>
        Ended = 3,
        /// <summary>
        /// タッチキャンセル
        /// </summary>
        Canceled = 4,
    }   
}