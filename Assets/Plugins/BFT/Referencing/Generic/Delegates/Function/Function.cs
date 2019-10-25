using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BFT
{
    /// <summary>
    ///     A parameter-less function that return its value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Function<T> : IValue<T>
    {
        private Func<T> func;
        [HorizontalGroup(MinWidth = 400)] public UnityEngine.Object Target;

        [HideInInspector] public FunctionTarget TargetFunction = new FunctionTarget();

        [ShowInInspector, ValueDropdown("Actions")]
        [HorizontalGroup()]
        [HideLabel]
        public string FunctionName
        {
            get => TargetFunction.Function;
            set
            {
                string[] vals = value.Split('/');

                if (Target is Component || Target is UnityEngine.GameObject)
                {
                    UnityEngine.GameObject go;
                    if (Target is Component comp)
                    {
                        go = comp.gameObject;
                    }
                    else
                    {
                        go = (UnityEngine.GameObject) Target;
                    }

                    string component = vals[0];
                    int id = 0;
                    if (component.Contains(":"))
                        id = int.Parse(component.Substring(component.IndexOf(".", StringComparison.Ordinal) + 1));

                    Type t = Type.GetType(component);
                    var comps = go.GetComponents(t);
                    var foundComp = comps[id];
                    TargetFunction = new FunctionTarget {ActionName = vals[1], TargetObject = foundComp};
                }
                else
                {
                    TargetFunction = new FunctionTarget {ActionName = vals[1], TargetObject = Target};
                }

                ResetFunc();
            }
        }


        public ValueDropdownList<string> Actions
        {
            get
            {
                var dd = new ValueDropdownList<string>();

                if (!Target)
                    return dd;

                if (Target is UnityEngine.GameObject go)
                {
                    var comps = go.GetComponents<Component>();
                    AddFromComponentList(comps);
                }
                else if (Target is Component component)
                {
                    var comps = component.GetComponents<Component>();
                    AddFromComponentList(comps);
                }
                else
                {
                    var type = Target.GetType();

                    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance |
                                                  BindingFlags.InvokeMethod).Where(_ => _.ReturnType == typeof(T))
                        .Where(_ => _.GetParameters().Length == 0);
                    methods.ForEach(_ => dd.Add(_.Name + "/" + _.Name));
                }

                void AddFromComponentList(Component[] comps)
                {
                    Dictionary<Type, int> countPerType = new Dictionary<Type, int>(comps.Length);
                    foreach (var component in comps)
                    {
                        var type = component.GetType();
                        int count = 0;
                        if (countPerType.ContainsKey(type))
                        {
                            count = countPerType[type];
                            countPerType[type]++;
                        }
                        else
                        {
                            countPerType.Add(type, 1);
                        }


                        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static |
                                                      BindingFlags.Instance |
                                                      BindingFlags.InvokeMethod).Where(_ => _.ReturnType == typeof(T))
                            .Where(_ => _.GetParameters().Length == 0);

                        foreach (MethodInfo method in methods)
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.Append(type.AssemblyQualifiedName);
                            if (count > 0)
                            {
                                builder.Append(":");
                                builder.Append(count.ToString());
                            }

                            builder.Append("/");
                            builder.Append(method.Name);
                            var fullname = builder.ToString();
                            builder.Clear();

                            builder.Append(type.Name);
                            if (count > 0)
                            {
                                builder.Append(":");
                                builder.Append(count.ToString());
                            }

                            builder.Append("/");
                            builder.Append(method.Name);

                            dd.Add(builder.ToString(), fullname);
                        }
                    }
                }

                return dd;
            }
        }

        public Func<T> Func
        {
            get
            {
                if (func == null)
                {
                    var info = TargetFunction.MethodInfo;
                    if (info == null)
                    {
                        UnityEngine.Debug.LogWarning("The method of the specified name could not be found");
                        return default;
                    }

                    func = (Func<T>) info.CreateDelegate(typeof(Func<T>), TargetFunction.TargetObject);
                }

                return func;
            }
        }

        [ShowInInspector]
        public T Value
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    if (!Target || TargetFunction == null || !TargetFunction.TargetObject ||
                        TargetFunction.ActionName == null)
                        return default;
#endif
                return Func();
            }
        }

        private FunctionTarget FunctionDrawer(FunctionTarget value, GUIContent label)
        {
            GUILayout.BeginHorizontal();
            if (label != null)
                GUILayout.Label(label);
            GUILayout.Label(value.Function);
            GUILayout.EndHorizontal();
            return value;
        }

        private void ResetFunc()
        {
            func = null;
            Target = TargetFunction.TargetObject;
        }

        /// <summary>
        ///     only use this for delegate, use Value directly otherwise
        /// </summary>
        /// <returns></returns>
        public T GetValue()
        {
            return Value;
        }
    }
}
