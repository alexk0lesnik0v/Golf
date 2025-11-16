using UnityEngine;
using UnityEngine.SceneManagement;


namespace Golf
{
    public class LavelController : MonoBehaviour
    {
        [SerializeField] private int m_maxHitCount;
        [SerializeField] private int m_missedCount;
        [SerializeField] [Min(0)] private float m_spawnRate = 0.5f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        
        private float m_time;
        private int m_currentHitCount = 0;
        private int m_currentMissedCount;
        private string labelText;
        private bool m_showWinScreen = false;
        private bool m_showLossScreen = false;

        private void Awake()
        {
            m_currentMissedCount = m_missedCount;
        }
        
        private void Update()
        {
            labelText = "Score " + m_maxHitCount + " points to win!!!";
            m_time += Time.deltaTime;
            
            if (m_time >= m_spawnRate)
            {
                Stone stone = m_stoneSpawner.Spawn();

                stone.Hit += OnHitStone;
                stone.Missed += OnMissed;
                
                m_time = 0;
            }
        }

        private void OnHitStone(Stone stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;

            m_currentHitCount++;
            if (m_currentHitCount >= m_maxHitCount)
            {
                m_showWinScreen =  true;
                Time.timeScale = 0f;
                Debug.Log("You won!!!");
            }
        }
        
        private void OnMissed(Stone stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;

            m_currentMissedCount--;
            if (m_currentMissedCount <= 0)
            {
                m_showLossScreen = true;
                Time.timeScale = 0f;
                Debug.Log("Game Over");
            }
        }
        
        private void RestartLevel()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1.0f;
        }
        
        private void OnGUI()
        {
            GUI.Box(new Rect(20, 20, 150, 25),
                "Score: " + m_currentHitCount + "/" + m_maxHitCount);
            
            GUI.Box(new Rect(20, 50, 150, 25),
                labelText);
            
            if (m_showWinScreen)
            { 
                if (GUI.Button(new Rect(Screen.width/2 - 100,
                        Screen.height/2 - 50, 200, 100), "YOU WON!"))
                {
                    RestartLevel();
                }
            }

            if (m_showLossScreen)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100,
                        Screen.height /  2 - 50, 200, 100), "You lose... "))
                {
                    RestartLevel();
                }
            }
        }
    }
}