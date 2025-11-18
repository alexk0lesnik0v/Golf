using UnityEngine;

namespace Golf
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private MainMenuState m_mainMenuState;
        [SerializeField] private GamePlayState m_gamePlayState;


        private void Awake()
        {
            m_mainMenuState.Initialize(this);
            m_gamePlayState.Initialize(this);
        }

        private void Start()
        {
            Enter<MainMenuState>();
        }
        
        public void Enter<T>()
        {
            if (typeof(T) == typeof(GamePlayState))
            {
                m_gamePlayState.Enter();
            }
        }
    }
}