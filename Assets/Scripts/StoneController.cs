using System;
using UnityEngine;

namespace Golf
{
    public class StoneController : MonoBehaviour
    {
        public event Action<StoneController> Hit;
        public event Action<StoneController> Missed;
        
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



/*
      public void OnCollisionEnter(Collision collision)
      {
          if (collision.collider != null)
          {
              if (collision.gameObject.name == "Collider 1" || collision.gameObject.name == "Collider 2")
              {
                  Debug.Log("Stone hit Golf Stick");
              }
              else if (collision.gameObject.name != "Stone 01" || collision.gameObject.name != "Stone 02")
              {
                  Debug.Log("Stone hit Ground");
              }
          }
      }
      */
