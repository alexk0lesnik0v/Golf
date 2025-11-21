using System;
using UnityEngine;

namespace Golf
{
    public class ScoreManager : MonoBehaviour
    {
        public event Action<int> ScoreChanged;

        private int m_score;

        public int score
        {
            get  => m_score;
            private set
            {
                m_score = value;
                Debug.Log($"Score: {value}");
                ScoreChanged?.Invoke(value);
            }
        }

        public int record
        {
            get  => PlayerPrefs.GetInt(GlobalConstance.Record, 0);
            private set
            {
                var temp = PlayerPrefs.GetInt(GlobalConstance.Record, 0);

                if (temp < value)
                {
                    PlayerPrefs.SetInt(GlobalConstance.Record, value);
                    RecordChanged?.Invoke(value);
                }
            }
        }
      
        public void Increase() => score++;
        
        public void BonusIncrease() => score += 2;
        
        public void ComboIncrease() => score += 3;
        
        public void GoldComboIncrease() => score += 6;

        public void Decrease()
        {
            if (score > 0)
            {
                score--;
            }
            else score = 0;
        }

        public void UpdateRecord()
        {
            
        }
        
        public void Reset() => score = 0;
    }
}