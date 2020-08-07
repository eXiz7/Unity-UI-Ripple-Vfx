/* Copyright (C) 2020 Vadimskyi - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License license.
 */
using System.Collections.Generic;
using UnityEngine;

namespace VadimskyiLab.SimpleUI
{
    /// <summary>
    /// Simple UI animation updater system
    /// </summary>
    public class TweenUpdaterMono : MonoBehaviour
    {
        private IList<ITweenComponentStrategy> _subscribers;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _instance = this;
            _subscribers = new List<ITweenComponentStrategy>();
        }

        private void Update()
        {
            if(_subscribers.Count == 0) return;
            RemoveCompleted();
            foreach (var sub in _subscribers)
            {
                sub.UpdateComponent(Time.deltaTime);
            }
        }

        public void Subscribe(ITweenComponentStrategy com)
        {
            _subscribers.Add(com);
        }

        private void RemoveCompleted()
        {
            for (int i = 0; i < _subscribers.Count; i++)
            {
                var sub = _subscribers[i];
                if (sub.GetState() != TweenComponentState.Completed && sub.GetState() != TweenComponentState.Killed) continue;

                _subscribers.RemoveAt(i);
                --i;
            }
        }

        public static TweenUpdaterMono Instance => _instance = _instance ?? new GameObject(nameof(TweenUpdaterMono),typeof(TweenUpdaterMono)).GetComponent<TweenUpdaterMono>();
        private static TweenUpdaterMono _instance;
    }
}