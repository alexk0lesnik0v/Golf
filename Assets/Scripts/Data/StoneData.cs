using UnityEngine;

namespace Golf.Data
{
    [CreateAssetMenu(fileName = "New StoneData", menuName = "StoneData")]
    public class StoneData : ScriptableObject
    {
        [SerializeField] private int m_score;
        
        public int score => m_score;
    }
}