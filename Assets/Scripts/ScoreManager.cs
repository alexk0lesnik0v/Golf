using UnityEngine;

namespace Golf
{
    public class ScoreManager : MonoBehaviour
    {
        public int score { get; private set; }
      
        public void Increase()
        {
            score++;
            Debug.Log($"Score: {score}");
        }
        
        public void Reset()
        {
            score = 0;
        }
    }
}