using UnityEngine;
using UnityEngine.AI;

public class TestAINav : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogWarning("Agent is not on NavMesh!");
        }
    }
}
