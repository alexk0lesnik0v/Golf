using UnityEngine;
using UnityEngine.Serialization;

namespace Golf
{
    public class PlayerController : MonoBehaviour

    {
       [SerializeField] private StoneSpawner m_stoneSpawner;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_stoneSpawner.Spawn();
            }
        }
    }
}