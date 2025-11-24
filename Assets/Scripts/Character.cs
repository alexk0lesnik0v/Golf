using UnityEngine;

namespace Golf
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Transform m_point;
        [SerializeField] private float m_minAngleY = -30;
        [SerializeField] private float m_maxAngleY = 30;
        [SerializeField] [Min(0)] private float m_speed = 100;
        
        private bool m_isRight = false;
        private bool m_isLeft = false;
        private Vector3 m_direction;
        private Vector3 m_LastPointPosition;
       
        private void FixedUpdate()
        {
            var angles =  transform.localEulerAngles;
            
            if (m_isRight)
            {
                angles.y = Rotate(angles.y, m_minAngleY);
            }
            else if (m_isLeft)
            {
                angles.y = Rotate(angles.y, m_maxAngleY);
            }
            
            transform.localEulerAngles = angles;
            
            //m_direction = (m_point.position - m_LastPointPosition).normalized;
            m_LastPointPosition = m_point.position;
        }

        public void ToRight()
        {
            m_isRight = true;
            m_isLeft = false;
        }

        public void ToLeft()
        {
            m_isLeft = true;
            m_isRight = false;
        }

        private float Rotate(float angleY, float target)
        {
            return Mathf.MoveTowardsAngle(angleY, target, m_speed * Time.deltaTime);
        }
    }
}