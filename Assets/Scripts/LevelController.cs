using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Golf
{
    public class LevelController : MonoBehaviour
    {
        public event Action Finished;
        public event Action Winner;
        public event Action<int> HitStoneChanged;
        public event Action<int> HitEnemyChanged;
        
        [SerializeField] private GameObject m_trainingOver;
        [SerializeField] private Button m_playButton;
        
        [SerializeField] private int m_missedCount = 10;
        [SerializeField] [Min(0)] private float m_spawnStoneRate = 1.5f;
        [SerializeField] [Min(0)] private float m_spawnEnemyRate = 5f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        [SerializeField] private EnemySpawner[] m_enemySpawner;
        [SerializeField] [Min(1)] private int m_maxEnemies = 5;
        [SerializeField] private ScoreManager m_scoreManager;
        [SerializeField] private TargetBox[] m_targetBox;
        
        private float m_currentStoneSpawnRate;
        private float m_currentEnemySpawnRate;
        private float m_stoneTime;
        private float m_enemyTime;
        private int m_enemySpawnController;
        private int m_currentMissedStoneCount;
        private int m_currentHitStoneCount;
        private int m_currentHitEnemyCount;
        private List<Stone> m_stones;
        private List<Enemy> m_enemies;
        private int m_targetBoxesCount;
        private int m_enemiesCount;
        
        private bool m_isEnemySpawn =  false;

        private bool m_isStoneSpawn = true;
        
        public int currentHitStoneCount
        {
            get  => m_currentHitStoneCount;
            private set
            {
                m_currentHitStoneCount = value;
                HitStoneChanged?.Invoke(value);
            }
        }
        
        public int currentHitEnemyCount
        {
            get  => m_currentHitEnemyCount;
            private set
            {
                m_currentHitEnemyCount = value;
                HitEnemyChanged?.Invoke(value);
            }
        }
       
        private void Awake()
        {
            m_stones = new List<Stone>();
            m_enemies = new List<Enemy>();
        }

        public void Initialize()
        {
            m_trainingOver.SetActive(false);
            
            m_currentMissedStoneCount = m_missedCount;
            m_currentStoneSpawnRate = m_spawnStoneRate;
            m_currentEnemySpawnRate = m_spawnEnemyRate;
            
            m_targetBoxesCount = m_targetBox.Length;

            foreach (TargetBox targetBox in m_targetBox)
            {
                targetBox.TargetDestroyed += OnTargetDestroyed;
            }
        }
        
        private void Update()
        {
            m_stoneTime += Time.deltaTime;
            m_enemyTime  += Time.deltaTime;
           
            if (m_stoneTime >= m_currentStoneSpawnRate && m_isStoneSpawn)
            {
                Stone stone = m_stoneSpawner.Spawn();
                if (!stone.CompareTag("DecreaseStone"))
                {
                    m_stones.Add(stone); 
                }
                
                stone.HitStone += OnHitStone;
                stone.MissedStone += OnMissedStone;
                
                stone.HitEnemy += OnHitEnemy;
                stone.MissedEnemy += OnMissedEnemy;
                
                m_stoneTime = 0;
            }
            
            if (m_enemyTime >= m_currentEnemySpawnRate  && m_isEnemySpawn)
            {
                foreach (EnemySpawner enemySpawner in m_enemySpawner)
                {
                    Enemy enemy = enemySpawner.Spawn();
                    m_enemies.Add(enemy);
                    m_enemiesCount++;
                    m_enemySpawnController++;
                    if (m_enemySpawnController == m_maxEnemies)
                    {
                        m_isEnemySpawn = false;
                    }
                }
                
                m_enemyTime = 0;
            }
        }

        private void OnHitEnemy(Stone stone)
        {
            UnsubscribeStoneEnemy(stone);
            m_currentHitEnemyCount++;
            m_enemiesCount--;
            
            m_scoreManager.EnemyIncrease();
            
            WinningController();
        }

        private void UnsubscribeStoneEnemy(Stone stone)
        {
            stone.HitEnemy -= OnHitEnemy;
            stone.MissedEnemy -= OnMissedEnemy;
        }

        private void OnMissedEnemy(Stone stone)
        {
            UnsubscribeStoneEnemy(stone);
            
            currentHitEnemyCount = 0;
        }
        
        private void OnHitStone(Stone stone)
        {
           UnsubscribeStone(stone);
           currentHitStoneCount++;
           
           if (stone.CompareTag("GoldStone"))
           {
               if (currentHitStoneCount >= 3)
               {
                   m_scoreManager.GoldComboIncrease();
               }
               else m_scoreManager.BonusIncrease();
           }
           else if (stone.CompareTag("DecreaseStone"))
           {
               m_scoreManager.Decrease();
               currentHitStoneCount = 0;
               m_currentMissedStoneCount--;
               FinishedController();
           }
           else if (currentHitStoneCount >= 3)
           {
               m_scoreManager.ComboIncrease();
           }
           else m_scoreManager.Increase();
           
           if (m_stones.Count % 5 == 0)
           {
               if (m_currentStoneSpawnRate > 0.1f)
               {
                   m_currentStoneSpawnRate -= 0.1f;
               }
               else m_currentStoneSpawnRate = 0.1f;
           }
        }
        
        private void OnMissedStone(Stone stone)
        {
            UnsubscribeStone(stone);
            
            if (stone.CompareTag("DecreaseStone"))
            {
                Destroy(stone.gameObject);
            }
            else
            {
                m_currentMissedStoneCount--;
                currentHitStoneCount = 0;
            }
            
            FinishedController();
        }
        
        private void OnTargetDestroyed(TargetBox targetBox)
        {
            m_targetBoxesCount--;
            
            targetBox.TargetDestroyed -= OnTargetDestroyed;
            
            TreiningController();
        }
        
        private void UnsubscribeStone(Stone stone)
        {
            stone.HitStone -= OnHitStone;
            stone.MissedStone -= OnMissedStone;
        }
        
        private void FinishedController()
        {
            if (m_currentMissedStoneCount <= 0)
            {
                GameOver();
            }
        }
        
        private void TreiningController()
        {
            if (m_targetBoxesCount == 0)
            {
                Debug.Log("Training is over!");
                EnemyAttack();
            }
        }
        
        private void WinningController()
        {
            if (m_enemiesCount == 0)
            {
                Victory();
            }
        }
        
        public void GameOver()
        {
            Debug.Log("Game Over");
            Finished?.Invoke();
        }

        private void Victory()
        {
            Debug.Log("YOU WON!");
            Winner?.Invoke();
        }
        
        private void EnemyAttack()
        {
            m_isStoneSpawn = false;
            m_trainingOver.SetActive(true);
            m_playButton.onClick.AddListener(onClicked);
        }
        
        private void onClicked()
        {
            m_trainingOver.SetActive(false);
            m_playButton.onClick.RemoveListener(onClicked);

            m_missedCount = 1000;
            
            m_isStoneSpawn = true;
            m_isEnemySpawn = true;
        }
    }
}