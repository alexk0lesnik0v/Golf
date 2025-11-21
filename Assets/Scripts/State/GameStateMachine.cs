using UnityEngine;

namespace Golf
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private StateBase m_stateBase;
        
        [SerializeField] private MainMenuState m_mainMenuState;
        [SerializeField] private GameplayState m_gameplayState;
        [SerializeField] private BootstrapState m_bootStrapState;
        [SerializeField] private GameOverState m_gameOverState;
        
        private StateBase[] m_states;

        private StateBase m_currentState;

        private void Awake()
        {
            m_mainMenuState.Initialize(this);
            m_gameplayState.Initialize(this);
            m_bootStrapState.Initialize(this);
            m_gameOverState.Initialize(this);
        }

        private void Start() => Enter<BootstrapState>();

        public void Enter<T>()
        {
            m_currentState?.Exit();

            foreach (StateBase state in m_states)
            {
                if (state.GetType() == typeof(T))
                {
                    m_currentState = state;
                    state.Enter();
                    
                   break;
                }
            }
            
            /*if (typeof(T) == typeof(BootstrapState))
            {
                m_bootStrapState.Enter();
            }
            else if (typeof(T) == typeof(MainMenuState))
            {
                m_gameOverState.Exit();
                m_bootStrapState.Exit();
                
                m_mainMenuState.Enter();
            }
            else if (typeof(T) == typeof(GameplayState))
            {
                m_mainMenuState.Exit();
                m_gameplayState.Enter();
            }
            else if (typeof(T) == typeof(GameOverState))
            {
                m_gameplayState.Exit();
                m_gameOverState.Enter();
            }
            */
        }
    }
}