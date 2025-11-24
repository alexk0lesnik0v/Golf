using UnityEngine;

namespace Golf
{
    public class Character : MonoBehaviour
    {
        [SerializeField] [Min(0)] private float m_speed = 100;
        
        private bool m_isRight;
        private bool m_isLeft;
        private bool m_isStop;
        
        private void FixedUpdate()
        {
            if (m_isRight)
            {
                transform.Rotate(0, m_speed * Time.deltaTime, 0);
            }
            else if (m_isLeft)
            {
                transform.Rotate(0, - m_speed * Time.deltaTime, 0);
            }
            else if (m_isStop)
            {
                transform.Rotate(0, 0, 0);
            }
        }

        public void ToRight()
        {
            m_isRight = true;
            m_isLeft = false;
            m_isStop = false;
        }

        public void ToLeft()
        {
            m_isLeft = true;
            m_isRight = false;
            m_isStop = false;
        }

        public void ToStop()
        {
            m_isStop = true;
            m_isRight = false;
            m_isLeft = false;
        }
    }
}