using System;
using UnityEngine;

namespace Golf
{
    public class ScoreManager : MonoBehaviour
    {
        public event Action<int> ScoreChanged;
        public event Action<int> RecordChanged;

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
            get
            {
                //PlayerPrefs.DeleteKey(GlobalConstants.Record);
                return PlayerPrefs.GetInt(GlobalConstants.Record, 0);
            }
            private set
            {
                if (record < value)
                {
                    PlayerPrefs.SetInt(GlobalConstants.Record, value);
                    RecordChanged?.Invoke(value);
                }
            }
        }

        public void Increase() => score ++;
        
        public void BonusIncrease() => score += 2;
        
        public void ComboIncrease() => score += 3;
        
        public void GoldComboIncrease() => score += 6;

        public void UpdateRecord() => record = score;
        
        public void TargetBoxIncrease() => score += 10;
        
        public void EnemyIncrease() => score += 20;

        public void Decrease()
        {
            if (score > 0)
            {
                score--;
            }
            else score = 0;
        }

        public void Reset() => score = 0;
    }
}