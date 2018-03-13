using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class EffectContainer : MonoBehaviour
    {
        #region Built-in Resources

        [SerializeField] GameObject[] effects;

        #endregion

        #region Monobehaviour Functions

        void Awake()
        {
            foreach (var effect in effects)
            {
                if (effect.GetComponent<ParticleSystem>() != null)
                {
                    EffectShurikenComponent.Instantiate(this.gameObject, effect);
                }
            }
        }

        #endregion
    }
}
