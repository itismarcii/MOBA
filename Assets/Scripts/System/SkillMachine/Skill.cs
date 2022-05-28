namespace System.SkillMachine
{
    [Serializable]
    public abstract class Skill : MagicBehaviour
    {
        internal uint[] ID;
        internal string SkillName = "";
        internal byte SkillPointsNeeded = 1;
        private bool Active = false;

        internal abstract bool IsMet();
        internal bool IsActive() => Active;

        internal virtual void Unlock()
        {
            if (IsMet()) Active = true;
        }

        internal bool CheckActive() => Active;

        internal void Lock() => Active = false;
    }
}
