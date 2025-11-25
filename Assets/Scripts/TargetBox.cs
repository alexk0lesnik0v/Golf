using UnityEngine;

namespace Golf
{
    public class TargetBox : MonoBehaviour
    {
        [SerializeField] private ScoreManager m_scoreManager;
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                Destroy(this.gameObject);
                m_scoreManager.TargetBoxIncrease();
            }
        }
    }
}
