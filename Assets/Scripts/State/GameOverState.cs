using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GameOverState : StateBase
    {
        [SerializeField] private GameObject m_gameOverPanel;
        
        [SerializeField] private Button m_backMainMenu;
        [SerializeField] private TextMeshProUGUI m_scoreText;
        [SerializeField] private ScoreManager m_scoreManager;
        
        private GameStateMachine m_gameStateMachine;

        public override void Initialize(GameStateMachine gameStateMachine)
        {
            m_gameStateMachine =  gameStateMachine;
            
            m_gameOverPanel.gameObject.SetActive(false);
        }

        public override void Enter()
        {
            m_scoreText.text = m_scoreManager.score.ToString();
            m_backMainMenu.onClick.AddListener(OnClicked);
            m_gameOverPanel.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            m_gameOverPanel.gameObject.SetActive(false);
        }
        
        private void OnClicked() => m_gameStateMachine.Enter<MainMenuState>();
    }
}