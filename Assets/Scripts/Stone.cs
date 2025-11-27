using System;
using Golf.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Golf
{
    [RequireComponent(typeof(Rigidbody))]
    public class Stone : MonoBehaviour
    {
        public event Action<Stone> HitStone;
        public event Action<Stone> MissedStone;
        
        public event Action<Stone> HitEnemy;
        public event Action<Stone> MissedEnemy;
        
        ///[SerializeField] private StoneData[] m_data;
        
        private Rigidbody m_rigidbody;
        
        private float m_liveTime = 5;
        
        ///public int score {  get; private set; }

        private void Awake()
        {
           m_rigidbody = GetComponent<Rigidbody>();
           //score = m_data[Random.Range(0, m_data.Length)].score;
        }
        
        private void Update()
        {
            m_liveTime -= Time.deltaTime;

            if (m_liveTime <= 0)
            {
                MissedEnemy?.Invoke(this);
                Destroy(this.gameObject);
            }
        }

        public void OnCollisionEnter(Collision other)
       {
           if (other.gameObject.GetComponent<Stick>())
           {
               HitStone?.Invoke(this);
           }
           else
           {
               MissedStone?.Invoke(this);
           }
           
           if (other.gameObject.GetComponent<Enemy>())
           {
               HitEnemy?.Invoke(this);
           }
       }

       public void AddForce(Vector3 power) => 
           m_rigidbody.AddForce(power, ForceMode.Force);
    }
}
