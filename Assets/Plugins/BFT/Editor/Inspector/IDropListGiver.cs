using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

public interface IDropListGiver<T>
{
    ValueDropdownList<T> GetDropDownList();
}