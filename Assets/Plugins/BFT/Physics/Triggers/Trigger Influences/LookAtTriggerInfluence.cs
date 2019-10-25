namespace Plugins.BFT.Physics.Triggers.Trigger_Influences
{
    public class LookAtTriggerInfluence : TriggerInfluence
    {
        public UnityEngine.Transform ToLookAt;

        protected override void Influence(InfluenceableObject obj)
        {
            obj.transform.LookAt(ToLookAt);
        }
    }
}