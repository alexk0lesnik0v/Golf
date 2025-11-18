using UnityEngine;

namespace Golf
{
    public class GameOverState : MonoBehaviour
    {
        [SerializeField] private GameObject m_gameOverPanel;
        
        private GameStateMachine m_gameStateMachine;

        public void Initialize(GameStateMachine gameStateMachine)
        {
            m_gameStateMachine =  gameStateMachine;
            
            m_gameOverPanel.gameObject.SetActive(false);
        }

        public void Enter()
        {
            m_gameOverPanel.SetActive(true);
        }

        public void Exit()
        {
            m_gameOverPanel.gameObject.SetActive(false);
        }
    }
}