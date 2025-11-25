using UnityEngine;

namespace Golf
{
    public class TargetBox : MonoBehaviour
    {
        [SerializeField] private ScoreManager m_scoreManager;

        private void Start()
        {
            this.gameObject.SetActive(true);
        }
        
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<Stone>(out var stone))
            {
                this.gameObject.SetActive(false);
                m_scoreManager.TargetBoxIncrease();
            }
        }
    }
}
