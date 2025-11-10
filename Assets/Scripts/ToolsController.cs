using UnityEditor.SceneManagement;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    [SerializeField] private GameObject[] m_tools;

    private int m_index = 0;

    private MeshFilter[] m_meshFilter;
    /*
    public GameObject _tools1;
    public GameObject _tools2;
    public GameObject _tools3;
    public GameObject _tools4;
    public GameObject _tools5;
    public GameObject _tools6;
    */
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeTools();
        }
    }
  
    public void ChangeTools()
    {
        
        while (m_index <= m_tools.Length)
        {
            m_meshFilter[m_index] = m_tools[m_index].GetComponent<MeshFilter>();
            m_index++;
        }

        m_index = 0;

        while (m_index <= m_meshFilter.Length)
        {
            if (m_index < m_meshFilter.Length)
            {
                m_meshFilter[m_index].mesh = m_meshFilter[m_index + 1].mesh;
                m_index++;
            }
            else if (m_index == m_meshFilter.Length)
            {
                m_meshFilter[m_index].mesh = m_meshFilter[0].mesh;
                m_index++;
            }
            else break;
        }
        
        



        /*
        MeshFilter componentToCopy1 = _tools6.GetComponent<MeshFilter>();
        MeshFilter componentToCopy2 = _tools5.GetComponent<MeshFilter>();
        MeshFilter componentToCopy3 = _tools2.GetComponent<MeshFilter>();
        MeshFilter componentToCopy4 = _tools1.GetComponent<MeshFilter>();
        MeshFilter componentToCopy5 = _tools4.GetComponent<MeshFilter>();
        MeshFilter componentToCopy6 = _tools3.GetComponent<MeshFilter>();

        MeshFilter meshFilter1 = _tools1.GetComponent<MeshFilter>();
        meshFilter1.mesh = componentToCopy1.mesh;
        meshFilter1.mesh = componentToCopy1.mesh;

        MeshFilter meshFilter6 = _tools6.GetComponent<MeshFilter>();
        meshFilter6.mesh = componentToCopy6.mesh;

        MeshFilter meshFilter3 = _tools3.GetComponent<MeshFilter>();
        meshFilter3.mesh = componentToCopy3.mesh;

        MeshFilter meshFilter2 = _tools2.GetComponent<MeshFilter>();
        meshFilter2.mesh = componentToCopy2.mesh;

        MeshFilter meshFilter5 = _tools5.GetComponent<MeshFilter>();
        meshFilter5.mesh = componentToCopy5.mesh;

        MeshFilter meshFilter4 = _tools4.GetComponent<MeshFilter>();
        meshFilter4.mesh = componentToCopy4.mesh;
        */
    }
}
