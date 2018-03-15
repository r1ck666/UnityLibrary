using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    /// <summary>
    /// Shuriken用のコンポーネント
    /// </summary>
    public class EffectShurikenComponent : EffectComponent
    {
        #region Private Variables

        ParticleSystem ps;

        #endregion

        #region Protected Functions

        /// <summary>
        /// ParticleSystemの再生処理
        /// </summary>
        protected override void Play()
        {
            this.ps.Play();
        }

        /// <summary>
        /// ParticleSystemの停止処理
        /// </summary>
        protected override void Stop()
        {
            this.ps.Stop();
        }

        #endregion

        #region Public Static Functions

        /// <summary>
        /// Effectを生成し、EffectShurikenComponentをアタッチする
        /// </summary>
        /// <returns>The instantiate.</returns>
        /// <param name="root">EffectContainerのGameObject</param>
        /// <param name="effect">ParticleSystemがアタッチされたGameObject</param>
        public static GameObject Instantiate(GameObject root, GameObject effect)
        {
            // effectを生成
            var obj = GameObject.Instantiate(effect);
            // 親オブジェクトに登録
            obj.transform.parent = root.transform;
            var effectComponent = obj.AddComponent<EffectShurikenComponent>();
            effectComponent.ps = effect.GetComponent<ParticleSystem>();

            return obj;
        }

        #endregion


    }
}

