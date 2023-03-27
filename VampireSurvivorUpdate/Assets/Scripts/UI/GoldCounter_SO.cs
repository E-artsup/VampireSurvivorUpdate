using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "GoldCounter", menuName = "ScriptableObjects/GoldCounterScriptableObject", order = 1)]
    public class GoldCounter_SO : ScriptableObject
    {
        public int goldAccumulated;
    }
}
