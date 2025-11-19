using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Golf
{
    public class GameplayState : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_scoreText;
        [SerializeField] private ScoreManager m_scoreManager;
        [SerializeField] private LevelController m_levelController;
        [SerializeField] private PlayerController m_playerController;
        
        private GameStateMachine m_gameStateMachine;
        
        public void Initialize(GameStateMachine gameStateMachine)
        {
            m_scoreText.gameObject.SetActive(false);
            m_gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            m_scoreManager.Reset();
            m_scoreManager.ScoreChanged += OnScoreChanged;
            
            m_scoreText.gameObject.SetActive(true);
            OnScoreChanged(m_scoreManager.score);
            
            m_levelController.enabled = true;
            m_playerController.enabled = true;
            
            m_levelController.Initialize();
            m_levelController.Finished += OnFinished;
        }

        private void OnFinished() => 
            m_gameStateMachine.Enter<GameOverState>();

        public void Exit()
        {
            m_levelController.enabled = false;
            m_playerController.enabled = false;
            m_scoreText.gameObject.SetActive(false);
            m_levelController.Finished -= OnFinished;
        }
        
        private void OnScoreChanged(int score) => 
            m_scoreText.text = score.ToString();
    }
}