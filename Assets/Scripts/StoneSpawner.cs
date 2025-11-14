using UnityEngine;

namespace Golf
{
    public class StoneSpawner : MonoBehaviour
    {
        [SerializeField] private StoneController[] m_prefabs;
        [SerializeField] private Transform m_spawnPoint;

        public StoneController Spawn()
        {
            var prefab = m_prefabs[Random.Range(0, m_prefabs.Length)];
            return Instantiate(prefab, m_spawnPoint.position, m_spawnPoint.rotation);
        }
    }
}

