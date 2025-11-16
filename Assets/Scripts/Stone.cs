using System;
using UnityEngine;

namespace Golf
{
    public class Stone : MonoBehaviour
    {
        public event Action<Stone> Hit;
        public event Action<Stone> Missed;
        
       public void OnCollisionEnter(Collision other)
       {
           if (other.gameObject.GetComponent<Stick>())
           {
               Hit?.Invoke(this);
           }
           else
           {
               Missed?.Invoke(this);
           }
       }
    }
}
