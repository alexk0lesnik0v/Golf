using System;
using TMPro;
using UnityEngine;

namespace Golf.UI
{
    public class RecordText : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_text;
        [SerializeField] private ScoreManager m_scoreManager;

        private void OnValidate()
        {
            if (!m_text)
            {
                m_text = GetComponent<TMP_Text>();
            }
        }

        private void OnEnable()
        {
            OnRecordChanged(m_scoreManager.score);
            m_scoreManager.RecordChanged += OnRecordChanged;
        }

        private void OnDisable()
        {
            m_scoreManager.RecordChanged -= OnRecordChanged;
        }

        private void OnRecordChanged(int value)
        {
            m_text.text = value.ToString();
        }
    }
}