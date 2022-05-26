using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.StateMachine
{
    public interface ICondition
    {
        bool IsMet();
    }

    public static class ConditionHandler
    {
        public static bool AreMet(List<ICondition> conditions) => conditions.All(x => x.IsMet());
    }
}