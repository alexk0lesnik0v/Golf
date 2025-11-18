using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GamePlayState : MonoBehaviour
    {
        [SerializeField] private Text m_scoreText;
        [SerializeField] private LavelController m_lavelController;
        [SerializeField] private PlayerController m_playerController;
        [SerializeField] private ScoreManager m_scoreManager;
        
        private GameStateMachine m_gameStateMachine;
        
        public void Initialize(GameStateMachine gameStateMachine)
        {
            
            m_gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            m_scoreManager.Reset();
            
        }
        
        public void Exit()
        {
            
        }
    }
}