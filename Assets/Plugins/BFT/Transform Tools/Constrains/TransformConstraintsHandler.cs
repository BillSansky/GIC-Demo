using System.Collections.Generic;

namespace BFT
{
    public class TransformConstraintsHandler : SingletonMB<TransformConstraintsHandler>
    {
        public List<AbstractTransformConstraint> ActiveConstrains = new List<AbstractTransformConstraint>();

        private int maxConstrainPriority = int.MinValue;
        private int minConstrainPriority = int.MaxValue;

        public void AddConstraint(AbstractTransformConstraint newConstraint)
        {
            if (ActiveConstrains.Count == 0)
            {
                ActiveConstrains.Add(newConstraint);
                maxConstrainPriority = newConstraint.ConstrainOrder;
                minConstrainPriority = newConstraint.ConstrainOrder;
                return;
            }

            if (newConstraint.ConstrainOrder >= maxConstrainPriority)
            {
                ActiveConstrains.Add(newConstraint);
                maxConstrainPriority = newConstraint.ConstrainOrder;
                return;
            }

            if (newConstraint.ConstrainOrder <= minConstrainPriority)
            {
                ActiveConstrains.Insert(0, newConstraint);
                minConstrainPriority = newConstraint.ConstrainOrder;
                return;
            }

            int i = 0;

            foreach (var constr in ActiveConstrains)
            {
                if (constr.ConstrainOrder >= newConstraint.ConstrainOrder)
                {
                    break;
                }

                i++;
            }

            ActiveConstrains.Insert(i, newConstraint);
        }

        public void RemoveConstraint(AbstractTransformConstraint constraint)
        {
            ActiveConstrains.Remove(constraint);
        }

        public void LateUpdate()
        {
            foreach (var constraint in ActiveConstrains)
            {
                constraint.Constrain();
            }
        }
    }
}
