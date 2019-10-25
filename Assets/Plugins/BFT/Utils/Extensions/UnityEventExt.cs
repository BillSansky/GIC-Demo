#define UNITY_EVENTS_DEBUG
using System;
using UnityEngine;
using UnityEngine.Events;

namespace BFT
{
    [Serializable]
    public class IntEvent : UnityEvent<int>
    {
    }

    [Serializable]
    public class LayerMaskEvent : UnityEvent<LayerMask>
    {
    }

    [Serializable]
    public class FloatEvent : UnityEvent<float>
    {
    }

    [Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }

    [Serializable]
    public class StringEvent : UnityEvent<string>
    {
    }

    [Serializable]
    public class ColliderEvent : UnityEvent<Collider>
    {
    }

    [Serializable]
    public class TransformEvent : UnityEvent<UnityEngine.Transform>
    {
    }

    [Serializable]
    public class Vector2Event : UnityEvent<Vector2>
    {
    }

    [Serializable]
    public class Vector3Event : UnityEvent<Vector3>
    {
    }

    [Serializable]
    public class SpriteEvent : UnityEvent<Sprite>
    {
    }

    [Serializable]
    public class QuaternionEvent : UnityEvent<Quaternion>
    {
    }

    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject>
    {
    }

    public class GenericEvent<T> : UnityEvent<T>
    {
    }

    public static class UnityEventExt
    {
        public static bool LogDebug;

        public static void AddClean(this UnityEvent @event, UnityAction action)
        {
            @event.RemoveListener(action);
            @event.AddListener(action);
        }

        public static void AddListenerClean<T>(this UnityEvent<T> @event, UnityAction<T> action)
        {
            @event.RemoveListener(action);
            @event.AddListener(action);
        }

        public static void AddCleanIntListener(this IntEvent @event, UnityAction<int> action)
        {
            @event.RemoveListener(action);
            @event.AddListener(action);
        }

        public static void AddCleanGenericListener<T>(this GenericEvent<T> @event, UnityAction<T> action)
        {
            @event.RemoveListener(action);
            @event.AddListener(action);
        }

        public static void ExtendedInvoke(this UnityEvent unityEvent, UnityEngine.Object owner = null)
        {
#if UNITY_EVENTS_DEBUG
            if (LogDebug)
            {
                int listenersCount = unityEvent.GetPersistentEventCount();
                if (listenersCount > 0)
                {
                    UnityEngine.Debug.Log("Event invoked info (listeners: " + listenersCount.ToString() + ")", owner);
                    for (int i = 0; i < listenersCount; i++)
                    {
                        UnityEngine.Debug.Log(
                            unityEvent.GetPersistentTarget(i).ToString() + " : " + unityEvent.GetPersistentMethodName(i),
                            unityEvent.GetPersistentTarget(i));
                    }
                }
            }
#endif
            unityEvent.Invoke();
        }
    }
}
