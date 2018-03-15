using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Inputs;

namespace Assets.Scripts.Inputs
{
    public abstract class InputGamePadBaseController : MonoBehaviour, IInputController
    {

        protected InputGamePadBaseController prevCtrl;

        abstract public void GetHorizontalL(float horizontal);
        abstract public void GetVerticalL(float vertical);
        abstract public void GetHorizontalR(float horizontal);
        abstract public void GetVerticalR(float vertical);
        abstract public void GetHorizontalC(float horizontal);
        abstract public void GetVerticalC(float vertical);
        abstract public void GetLTrigger(float trigger);
        abstract public void GetRTrigger(float trigger);
        abstract public void GetKey(KeyType type);

        public InputGamePadBaseController ChangeController(InputGamePadBaseController ctrl)
        {
            ctrl.prevCtrl = this;
            return ctrl;
        }
    }
}
