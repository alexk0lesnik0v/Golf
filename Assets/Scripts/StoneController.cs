using UnityEngine;

namespace Golf
{
    public class StoneController : MonoBehaviour
    {
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
    }
}

