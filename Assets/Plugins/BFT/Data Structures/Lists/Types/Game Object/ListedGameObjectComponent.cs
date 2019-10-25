using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public class ListedGameObjectComponent : ListedComponent<GameObject, List<GameObject>>
    {
        public override GameObject Data => gameObject;
    }
}
