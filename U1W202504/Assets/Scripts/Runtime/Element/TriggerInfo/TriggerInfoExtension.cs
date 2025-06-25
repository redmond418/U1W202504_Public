using System.Collections.Generic;
using System.Linq;
using U1W;

public static class TriggerInfoExtension
{
    public static bool TryFirstOfType<T>(this IEnumerable<ITriggerInfo> infos, out T firstTrigger) where T : ITriggerInfo
    {
        firstTrigger = infos.OfType<T>().FirstOrDefault();
        return firstTrigger != null;
    }
}
