using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Golf
{
    public class WinnerState : StateBase
    {
        [SerializeField] private GameObject m_winnerPanel;
        
        [SerializeField] private Button m_backMainMenu;
        [SerializeField] private TextMeshProUGUI m_scoreText;
        [SerializeField] private ScoreManager m_scoreManager;
        
        private GameStateMachine m_gameStateMachine;

        public override void Initialize(GameStateMachine gameStateMachine)
        {
            m_gameStateMachine =  gameStateMachine;
            
            m_winnerPanel.gameObject.SetActive(false);
        }

        public override void Enter()
        {
            m_scoreText.text = m_scoreManager.score.ToString();

            m_scoreManager.UpdateRecord();
            
            m_backMainMenu.onClick.AddListener(OnClicked);
            m_winnerPanel.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            m_winnerPanel.gameObject.SetActive(false);
        }
        
        private void OnClicked()
        {
            m_gameStateMachine.Enter<MainMenuState>();
            RestartLevel();
        }
        
        public static void RestartLevel()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }
}