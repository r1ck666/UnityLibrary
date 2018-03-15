using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Effect用汎用Component
    /// </summary>
    public abstract class EffectComponent : MonoBehaviour
    {

        #region Abstract Functions

        protected abstract void Play();
        protected abstract void Stop();

        #endregion

        #region Public Functions

        /// <summary>
        /// Plaies the effect.
        /// </summary>
        public void PlayEffect()
        {
            Play();
        }

        /// <summary>
        /// Stops the effect.
        /// </summary>
        public void StopEffect()
        {
            Stop();
        }

        #endregion
    }
}

