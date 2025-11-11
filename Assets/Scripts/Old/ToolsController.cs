using UnityEngine;

namespace Old
{
    public class ToolsController : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_tools;

        private GameObject m_tool;

        private int m_index = 0;

        private MeshFilter m_mesh;

        private MeshFilter[] m_meshFilter;
    
        public void ChangeTools()
        {
            m_tool = m_tools[0];
        
            if (m_meshFilter == null)
            {
                m_meshFilter = new MeshFilter[m_tools.Length];

                while (m_index <= m_tools.Length - 1)
                {
                    m_meshFilter[m_index] = m_tools[m_index].GetComponent<MeshFilter>();
                    m_index++;
                }
                m_index = 0;
                m_mesh = m_meshFilter[0];
            }
      
            while (m_index <= m_meshFilter.Length)
            {
                if (m_index < m_meshFilter.Length - 1)
                {
                    m_meshFilter[m_index].mesh = m_meshFilter[m_index + 1].mesh;
                    m_tools[m_index] = m_tools[m_index + 1];
                    m_index++;
                }
                else if (m_index == m_meshFilter.Length - 1)
                {
                    m_meshFilter[m_index].mesh = m_mesh.mesh;
                    m_tools[m_index] = m_tool;
                    break;
                }
            }

            m_index = 0;
        }
    }

}

