using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace BFT
{
    public class MultiEventLink : SerializedMonoBehaviour
    {
        private IReadOnlyDictionary<UnityEvent> cachedEventReferencer;

        [BoxGroup("Referencing")] public IValue<IReadOnlyDictionary<UnityEvent>> EventReferencer;

        [BoxGroup("Referencing")] public Dictionary<int, UnityEvent> EventsPerID = new Dictionary<int, UnityEvent>();

        [FoldoutGroup("Tools/ Edit")] [ValueDropdown("IDDropDown")]
        private int idToEdit;

        private ValueDropdownList<int> IDDropDown
        {
            get
            {
                if (EventReferencer == null || EventReferencer.Value == null)
                    return new ValueDropdownList<int>();
                return EventReferencer.Value.ContentIDs;
            }
        }

        private bool IDExists => EventsPerID.ContainsKey(idToEdit);

        [ShowIf("IDExists")]
        [FoldoutGroup("Tools/ Edit")]
        public UnityEvent eventToEdit
        {
            get
            {
                if (EventsPerID.ContainsKey(idToEdit))
                    return EventsPerID[idToEdit];
                return new UnityEvent();
            }
            set
            {
                if (EventsPerID.ContainsKey(idToEdit))
                    EventsPerID[idToEdit] = value;
            }
        }

        [BoxGroup("Tools")]
        [Button(ButtonSizes.Medium)]
        public void BindEvents()
        {
            if (EventsPerID == null)
                return;

            if (cachedEventReferencer != null)
            {
                foreach (var unityEvent in EventsPerID)
                {
                    EventReferencer.Value.Get(unityEvent.Key).RemoveListener(unityEvent.Value.Invoke);
                }
            }

            cachedEventReferencer = EventReferencer.Value;

            foreach (var unityEvent in EventsPerID)
            {
                EventReferencer.Value.Get(unityEvent.Key).AddListener(unityEvent.Value.Invoke);
            }
        }

        [FoldoutGroup("Tools/ Edit")]
        [HideIf("IDExists")]
        public void AddNewEvent()
        {
            EventsPerID.Add(idToEdit, new UnityEvent());
        }
    }
}
