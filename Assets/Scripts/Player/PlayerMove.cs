using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Camera;

namespace Assets.Scripts.Player
{
    public class PlayerMove : MonoBehaviour
    {

        #region Parameters

        [SerializeField] float speed = 100f;
        [SerializeField] Vector3 direction;

        #endregion

        #region Public Properties (SetOnly)

        float _horizontal = 0.0f;
        public float Horizontal
        {
            set { _horizontal = value; }
        }
        float _vertical = 0.0f;
        public float Vertical
        {
            set { _vertical = value; }
        }

        #endregion

        #region Private Variables

        Rigidbody m_rb;
        Animator m_Animator;
        float m_ForwardAmount;
        float m_TurnAmount;

        #endregion

        #region Monobehaviour Functions

        // Use this for initialization
        void Start()
        {
            m_rb = GetComponent<Rigidbody>();
            m_Animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var mps = speed * (1000f / 3600f);

            var cameraForward = Vector3.Scale(UnityEngine.Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            var cameraRight = Vector3.Scale(UnityEngine.Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
            direction = cameraForward * _vertical + cameraRight * _horizontal;

            m_ForwardAmount = _vertical;
            m_TurnAmount = _horizontal;


            // move
            var v = direction * mps;
            var force = (m_rb.mass * m_rb.drag * v) / (1f - m_rb.drag * Time.fixedDeltaTime);
            m_rb.AddForce(force);
            //Debug.LogFormat("velocity={0}km/h", rb.velocity.magnitude * (3600f / 1000));

            // look
            if (direction != Vector3.zero) transform.rotation = Quaternion.LookRotation(direction);

            // update the animator parameters
            m_Animator.SetFloat("Forward", direction.magnitude, 0.1f, Time.deltaTime);


        }

        #endregion

        #region Private Functions


        #endregion
    }

}