using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Golf
{
    public class GameplayState : StateBase
    {
        [SerializeField] private TextMeshProUGUI m_scoreText;
        
        [SerializeField]  private GameObject m_gameplayPanel;
        [SerializeField] private TextMeshProUGUI m_comboText;
        [SerializeField] private ScoreManager m_scoreManager;
        [SerializeField] private LevelController m_levelController;
        [SerializeField] private PlayerController m_playerController;
        
        private GameStateMachine m_gameStateMachine;
        
        public override void Initialize(GameStateMachine gameStateMachine)
        {
            m_scoreText.gameObject.SetActive(false);
            m_comboText.gameObject.SetActive(false);
            m_gameStateMachine = gameStateMachine;
        }
        
        public override void Enter()
        {
            m_scoreManager.Reset();
            m_scoreManager.ScoreChanged += OnScoreChanged;
            
            m_scoreText.gameObject.SetActive(true);
            OnScoreChanged(m_scoreManager.score);
            
            m_levelController.HitChanged += OnHitChanged;
            OnHitChanged(m_levelController.currentHitCount);
            
            m_levelController.enabled = true;
            m_playerController.enabled = true;
            
            m_levelController.Initialize();
            m_levelController.Finished += OnFinished;
        }

        private void OnFinished() => 
            m_gameStateMachine.Enter<GameOverState>();

        public override void Exit()
        {
            m_levelController.enabled = false;
            m_playerController.enabled = false;
            m_scoreText.gameObject.SetActive(false);
            m_comboText.gameObject.SetActive(false);
            m_levelController.Finished -= OnFinished;
        }

        private void OnScoreChanged(int score) => 
            m_scoreText.text = score.ToString();

        private void OnHitChanged(int currentHitCount)
        {
            if (currentHitCount >= 3)
            {
                m_comboText.text = "combo X" + currentHitCount.ToString();
                m_comboText.gameObject.SetActive(true);
            }
            else
            {
                m_comboText.gameObject.SetActive(false);
            }
        } 
    }
}