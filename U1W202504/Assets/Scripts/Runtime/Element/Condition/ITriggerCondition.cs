namespace U1W
{
    public interface ITriggerCondition
    {
        public bool ValidateCondition(params ITriggerInfo[] infos);
    }
}
