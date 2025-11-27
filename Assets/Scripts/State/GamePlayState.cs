using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Golf
{
    public class GameplayState : StateBase
    {
        [SerializeField] private TextMeshProUGUI m_scoreText;
        [SerializeField] private TextMeshProUGUI m_comboText;
        [SerializeField] private TextMeshProUGUI m_comboKillText;

        [SerializeField] private GameObject m_gameplayPanel;
        [SerializeField] private ScoreManager m_scoreManager;
        [SerializeField] private LevelController m_levelController;
        [SerializeField] private PlayerController m_playerController;
        
        private GameStateMachine m_gameStateMachine;
        
        public override void Initialize(GameStateMachine gameStateMachine)
        {
            m_gameplayPanel.SetActive(false);
            m_comboText.gameObject.SetActive(false);
            m_comboKillText.gameObject.SetActive(false);
            m_gameStateMachine = gameStateMachine;
        }
        
        public override void Enter()
        {
            m_scoreManager.Reset();
            m_scoreManager.ScoreChanged += OnScoreChanged;
            
            OnScoreChanged(m_scoreManager.score);
            m_gameplayPanel.SetActive(true);
            
            m_levelController.HitStoneChanged += OnHitStoneChanged;
            OnHitStoneChanged(m_levelController.currentHitStoneCount);
            
            m_levelController.HitEnemyChanged += OnHitEnemyChanged;
            OnHitEnemyChanged(m_levelController.currentHitEnemyCount);
            
            m_levelController.enabled = true;
            m_playerController.enabled = true;
            
            m_levelController.Initialize();
            m_levelController.Finished += OnFinished;
            m_levelController.Winner += OnWinner;
        }

        private void OnWinner()
        {
            m_gameStateMachine.Enter<WinnerState>();
        }

        private void OnFinished()
        {
            m_gameStateMachine.Enter<GameOverState>();
        }

        public override void Exit()
        {
            m_levelController.enabled = false;
            m_playerController.enabled = false;
            m_gameplayPanel.SetActive(false);
            m_comboText.gameObject.SetActive(false);
            m_levelController.Finished -= OnFinished;
            m_levelController.Winner -= OnWinner;
        }

        private void OnScoreChanged(int score) => 
            m_scoreText.text = score.ToString();

        private void OnHitStoneChanged(int currentHitStoneCount)
        {
            if (currentHitStoneCount >= 3)
            {
                m_comboText.text = "combo X" + currentHitStoneCount.ToString();
                m_comboText.gameObject.SetActive(true);
            }
            else
            {
                m_comboText.gameObject.SetActive(false);
            }
        } 
        
        private void OnHitEnemyChanged(int currentHitEnemyCount)
        {
            if (currentHitEnemyCount >= 3)
            {
                m_comboKillText.text = "combo kill X" + currentHitEnemyCount.ToString();
                m_comboKillText.gameObject.SetActive(true);
            }
            else
            {
                m_comboKillText.gameObject.SetActive(false);
            }
        }
    }
}