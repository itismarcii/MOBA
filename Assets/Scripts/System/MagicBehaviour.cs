using UnityEngine;

namespace System
{
    public abstract class MagicBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            UpdateManager.Subscription.Add(this);
            UpdateManager._Subscriber = UpdateManager.Subscription.ToArray();
        }

        public virtual void _Awake_() {}
        public virtual void _Start_(){}
        public virtual void _Update_(){}
        public virtual void _FixedUpdate_(){}
        public virtual void _LateUpdate_(){}

        public void OnDestroy()
        {
            UpdateManager.Subscription.Remove(this);
            UpdateManager._Subscriber = UpdateManager.Subscription.ToArray();
            Destroy(gameObject);
        }
    }
}
