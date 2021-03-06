﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cosmos.Mono;
namespace Cosmos
{
    [DisallowMultipleComponent]
public class Timer : MonoBehaviour {
        [Header("勾选AutoStart则使用StartDelay")]
        [SerializeField] bool autoStart;
        public bool AutoStart { get { return autoStart; } set { autoStart = value; } }
        [SerializeField] float startDelay=1;
        public float StartDelay { get { if (startDelay <= 0) startDelay = 0.1f;return startDelay; } }
        [Header("ExecuteTimerAction默认使用Interval")]
        [SerializeField] float interval=1;
        public float Interval { get { return interval; } set { if (value <= 0.1) interval = 0.1f; else interval = value; } }
        [SerializeField] bool loop=false;
        public bool Loop { get { return loop; } set { loop = value; } }
        [Header("使用随机间隔，属性数值小于Interval")]
        [SerializeField] bool randomInterval=false;
        public bool RandomInterval { get { return randomInterval; } set { randomInterval = value; } }
        [SerializeField] float randomRange;
        public float RandomRange { get { return randomRange; }set { if (randomRange >= Interval) randomRange = Interval;
                else if (randomRange <= 0) randomRange = 0; else randomRange = value; } }
        [SerializeField] UnityEvent action;
        public UnityEvent Action { get { return action; } set { action = value; } }
        Coroutine  tempRoutine;
        private void Start()
        {
            if (autoStart)
                tempRoutine = Facade.Instance.StartCoroutine(EnumAction(StartDelay, () => action.Invoke()));
        }
        /// <summary>
        /// 执行的时候可以有延迟
        /// </summary>
        public virtual void ExecuteTimerAction()
        {
            if(!RandomInterval)
            tempRoutine = Facade.Instance.StartCoroutine(EnumAction(Interval, ()=>action.Invoke()));
            else
                tempRoutine = Facade.Instance.StartCoroutine(EnumAction(Interval+ Utility.Random(-RandomRange, RandomRange), () => action.Invoke()));
        }
        /// <summary>
        /// 立即停止
        /// </summary>
        public virtual void StopTimerAction()
        {
            Facade.Instance.StopCoroutine(tempRoutine);
        }
        IEnumerator EnumAction(object arg,CFAction handler)
        {
            yield return new WaitForSeconds(Utility.Float(arg));
            handler?.Invoke();
            if(loop)
                tempRoutine = Facade.Instance.StartCoroutine(EnumAction(Interval, () => action.Invoke()));
        }
    }
}