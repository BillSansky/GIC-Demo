using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BFT
{
    public static class ObjectExt
    {
        public static void Times(this int count, System.Action action)
        {
            for (var i = 0; i < count; i++)
            {
                action();
            }
        }

        public static void Times(this int count, System.Action<int> action)
        {
            for (var i = 0; i < count; i++)
            {
                action(i);
            }
        }

        public static void SetInactive(this GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public static void SetActive(this GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        ///     Use this to check if Unity MonoBehaviour is null
        /// </summary>
        public static void IfBehaviour<T>(this T behaviour, System.Action<T> action) where T : MonoBehaviour
        {
            if (behaviour)
            {
                action(behaviour);
            }
        }

        /// <summary>
        ///     NOT use it for Unity MonoBehaviour, use IfBehaviour instead
        /// </summary>
        public static T IfNotNull<T>(this T @object, System.Action<T> action)
        {
            if (@object != null) // WARNING: this null check fails for Unity MonoBehaviour!
            {
                action(@object);
            }

            return @object;
        }

        public static void IfNull<T>(this T @object, System.Action action)
        {
            if (@object == null)
            {
                action();
            }
        }

        public static T IfNull<T>(this T @object, Func<T> func)
        {
            if (@object == null)
            {
                @object = func();
            }

            return @object;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        public static GameObject CreateNewChildGameObject(this GameObject obj, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(obj.transform, false);
            return go;
        }

        public static GameObject CreateNewSiblingGameObject(this GameObject obj, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(obj.transform.parent, false);
            return go;
        }
    }
}
