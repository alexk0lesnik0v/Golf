using System;
using UnityEngine;

namespace Golf
{
    public class TargetBox : MonoBehaviour
    {
        public event Action<TargetBox> TargetDestroyed;
        
        [SerializeField] private ScoreManager m_scoreManager;
        
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                TargetDestroyed?.Invoke(this);
                Destroy(this.gameObject);
                m_scoreManager.TargetBoxIncrease();
            }
        }
    }
}
