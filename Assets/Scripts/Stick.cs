using UnityEngine;

namespace Golf
{
    public class Stick : MonoBehaviour
    {
        [SerializeField] private float m_minAnglesZ = -30;
        [SerializeField] private float m_maxAnglesZ = 30;
        [SerializeField] [Min(0)] private float m_speed;
        
        private void Update()
        {
            
            
            var angles =  transform.localEulerAngles;
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                angles.z = Rotate(angles.z, m_minAnglesZ);
            }
            else
            {
                angles.z = Rotate(angles.z, m_maxAnglesZ);
            }
            transform.localEulerAngles = angles;
        }

        private float Rotate(float angleZ, float target)
        {
            return Mathf.MoveTowardsAngle(angleZ, target, m_speed * Time.deltaTime);
        }
    }

}
