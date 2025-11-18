using UnityEngine;

namespace Golf
{
    public class BootStrapState : MonoBehaviour
    {
        [SerializeField] private LavelController m_lavelController;
        [SerializeField] private PlayerController m_playerController;
        
        private GameStateMachine m_gameStateMachine;

        public void Initialize(GameStateMachine gameStateMachine)
        {
            m_playerController.enabled = false;
            m_lavelController.enabled = false;
            
            m_gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            m_gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
            
        }
    }
}