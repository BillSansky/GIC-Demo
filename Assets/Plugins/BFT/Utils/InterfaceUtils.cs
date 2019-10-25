using UnityEngine;

namespace BFT
{
    public static class InterfaceUtils
    {
        public static void GetObjectAfterTypeCheck<T>(ref UnityEngine.Object referenceObject,
            bool tellTypeNeededIfError = false, UnityEngine.Object owner = null)
            where T : class
        {
            if ((referenceObject as T) == null)
            {
                Component[] components;
                var gameObject = referenceObject as GameObject;

                if (gameObject)
                {
                    components = gameObject.GetComponents<Component>();
                }
                else
                {
                    var comp = referenceObject as Component;
                    if (!comp)
                    {
                        LogWarning(referenceObject);

                        referenceObject = null;

                        return;
                    }
                    else
                    {
                        components = comp.gameObject.GetComponents<Component>();
                    }
                }

                //then check all the components of that gameobject to check if any of them isnt of the requiredType;
                foreach (var component in components)
                {
                    if ((component as T) != null && component != owner)
                    {
                        referenceObject = component;
                        return;
                    }
                }

                LogWarning(referenceObject);
                referenceObject = null;

                void LogWarning(UnityEngine.Object obj)
                {
                    if (tellTypeNeededIfError)
                        UnityEngine.Debug.LogWarning(
                            $"No component of the required type {typeof(T)} were found on the game object linked",
                            obj);
                    else
                    {
                        UnityEngine.Debug.LogWarning(
                            $"No component of the required type were found on the game object linked, the reference was set to null",
                            obj);
                    }
                }
            }
        }
    }
}
