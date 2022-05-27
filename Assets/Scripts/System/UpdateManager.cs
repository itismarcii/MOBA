using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class UpdateManager : MonoBehaviour
    {
        public static readonly List<MagicBehaviour> Subscription = new List<MagicBehaviour>();
        [SerializeField] public static MagicBehaviour[] _Subscriber;

        private void Start()
        {
            _Subscriber = Subscription.ToArray();
            foreach (var subscriber in _Subscriber) subscriber._Awake_();
            foreach (var subscriber in _Subscriber) subscriber._Start_();
        }

        public void Update()
        {
            foreach (var subscriber in _Subscriber) subscriber._Update_();
        }

        private void FixedUpdate()
        {
            foreach (var subscriber in _Subscriber) subscriber._FixedUpdate_();
        }

        private void LateUpdate()
        {
            foreach (var subscriber in _Subscriber) subscriber._LateUpdate_(); 
        }
    }
}
