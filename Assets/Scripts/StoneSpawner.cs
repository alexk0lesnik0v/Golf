using UnityEngine;

namespace Golf
{
    public class StoneSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_prefabs;
        [SerializeField] private Transform m_spawnPoint;

        public void Spawn()
        {
            var prefab = m_prefabs[Random.Range(0, m_prefabs.Length)];
            Instantiate(prefab, m_spawnPoint.position, m_spawnPoint.rotation);
        }
    }
}

