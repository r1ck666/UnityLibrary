using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Assets.Scripts.Inputs
{
    public class InputTouchBaseProvider : MonoBehaviour
    {
        #region Paramator

        /// <summary>
        /// フリック判定の最低距離
        /// </summary>
        [SerializeField] protected float flickLowestDistance = 50.0f;

        /// <summary>
        /// フリック判定の最大時間[sec]
        /// </summary>
        [SerializeField] protected float flickMaximumTime = 0.5f;

        /// <summary>
        /// 引っ張り判定の最低距離
        /// </summary>
        [SerializeField] protected float pullingLowestDistance = 30.0f;

        #endregion

        #region Property

        /// <summary>
        /// 指をフリックした時のコールバック
        /// </summary>
        /// Vector3: フリックした方向
        protected Subject<Vector3> m_FlickSubject = new Subject<Vector3>();
        public IObservable<Vector3> OnFlickFinger
        {
            get { return m_FlickSubject; }    
        }

        /// <summary>
        /// 指を引っ張った時のコールバック
        /// </summary>
        /// Vector3: 引っ張っている方向
        protected Subject<Vector3> m_PullingSubject = new Subject<Vector3>();
        public IObservable<Vector3> OnPullingFinger
        {
            get { return m_PullingSubject; }
        }

        #endregion

        #region Protected Resources

        // 長押ししている間の変化量
        protected Dictionary<int, Vector3> m_deltaPosSumMap = new Dictionary<int, Vector3>();
        // 長押ししている時間
        protected Dictionary<int, float> m_deltaTimeSumMap = new Dictionary<int, float>();

        #endregion

        #region MonoBehaviour Functions

        private void Update()
        {

            Debug.Log("親");
            UpdateTouchCheck();
        }

        #endregion

        #region Protected Functions

        /// <summary>
        /// FingerId毎に計算しているので、マルチタッチ対応
        /// タッチしてから離すまでのdeltaPosとdeltaTimeを計算
        /// </summary>
        protected void UpdateTouchCheck()
        {
            var touchCount = MultiPlatformTouchUtils.touchCount;

            if (touchCount > 0)
            {
                for (int i = 0; i < touchCount; i++)
                {
                    var fingerId = MultiPlatformTouchUtils.getFingerId(i);

                    switch (MultiPlatformTouchUtils.GetTouch(i))
                    {
                        case TouchInfo.Began:
                            // Mapの初期化
                            m_deltaPosSumMap[fingerId] = Vector3.zero;
                            m_deltaTimeSumMap[fingerId] = 0.0f;


                            break;
                        case TouchInfo.Moved:
                            m_deltaPosSumMap[fingerId] += MultiPlatformTouchUtils.GetDeltaPosition(i);
                            m_deltaTimeSumMap[fingerId] += MultiPlatformTouchUtils.GetDeltaTime(i);


                            break;
                        case TouchInfo.Ended:
                            m_deltaPosSumMap[fingerId] += MultiPlatformTouchUtils.GetDeltaPosition(i);
                            m_deltaTimeSumMap[fingerId] += MultiPlatformTouchUtils.GetDeltaTime(i);

                            // 指を話した時の処理
                            ReleaseFinger(fingerId);
                            // 

                            // Mapの削除
                            m_deltaPosSumMap.Remove(fingerId);
                            m_deltaTimeSumMap.Remove(fingerId);

                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 指を離した時の処理
        /// </summary>
        /// <param name="fingerId">Finger identifier.</param>
        protected void ReleaseFinger(int fingerId)
        {
            var deltaPos = m_deltaPosSumMap[fingerId];
            var deltaTime = m_deltaTimeSumMap[fingerId];

            // Flick処理
            if (deltaPos.magnitude > flickLowestDistance && deltaTime < flickMaximumTime)
            {
                
                m_FlickSubject.OnNext(deltaPos);

            // 引っ張る処理
            } else if (deltaPos.magnitude > pullingLowestDistance) {


                m_PullingSubject.OnNext(deltaPos);
            }
        }

        #endregion

    }
}