using System.Linq;
using UnityEngine;

namespace System
{
    public abstract class MagicBehaviour : MonoBehaviour
    {
        [SerializeField] protected uint LoadingPriority = 3;
        internal uint GetLoadingPriority() => LoadingPriority;
        
        private void OnEnable()
        {
            UpdateManager.Subscription.Add(this);
            UpdateManager.Subscriber = UpdateManager.Subscription.ToArray();
        }

        public virtual void _Awake_() {}
        public virtual void _Start_(){}
        public virtual void _Update_(){}
        public virtual void _FixedUpdate_(){}
        public virtual void _LateUpdate_(){}

        public void OnDestroy()
        {
            UpdateManager.Subscription.Remove(this);
            UpdateManager.Subscriber = UpdateManager.Subscription.OrderByDescending(subscriber => LoadingPriority).ToArray();
            Destroy(gameObject);
        }
    }
}
