using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KaijuController : MonoBehaviour {

    [SerializeField] float searchRadius;
    [SerializeField] LayerMask searchForLayer;


    [SerializeField] float updateNewPosition_Timer = 10;
    [SerializeField] float updateNewPosition_Radius = 25;


    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject fireBallPrefab;
    [SerializeField] float farballSpeed;
    public GameObject target;
    float updatePositionTimer = 0;
    [SerializeField] float shootFireRate = 3;
    bool updateNewPosition = true;

    NavMeshAgent agent;

    Lookat lookat;

    private void Start()
    {
        lookat = GetComponent<Lookat>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FireBalls());
    }

    private void Update()
    {

        Collider[] hitObjects = Physics.OverlapSphere(transform.position, searchRadius, searchForLayer, QueryTriggerInteraction.Ignore);

        foreach (Collider n in hitObjects)
        {

            if (n.CompareTag("Player"))
            {
                
                updateNewPosition = false;
                target = n.gameObject;
                lookat.LookAt(target.transform);
                agent.SetDestination(n.gameObject.transform.position);
            }
            
            
        }

        Vector3 point;

        if (updateNewPosition)
        {
            if (RandomPoint(transform.position, searchRadius, out point))
            {
                agent.SetDestination(point);
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                updatePositionTimer = agent.remainingDistance + updateNewPosition_Timer;
                if (agent.remainingDistance != 0)
                {
                    updateNewPosition = true;
                }
                updateNewPosition = false;
            }
            else
            {
                updateNewPosition = true;
            }
        }
        if (updatePositionTimer > 0)
        {
            updatePositionTimer -= Time.deltaTime;
        }
        else
        {
            updateNewPosition = true;
        }
        float speedPercent = agent.velocity.magnitude / agent.speed;
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + (Random.insideUnitSphere * range) + (Vector3.one * range / 4);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    IEnumerator FireBalls()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Started");
            if(target != null)
            {
                spawnFireBall();
            }

            yield return new WaitForSeconds(shootFireRate);
        }

    }

    void spawnFireBall()
    {
        Vector3 dir = target.transform.position - spawnPosition.position;
        Quaternion fireAtRotation = Quaternion.LookRotation(dir);
        GameObject train = Instantiate(fireBallPrefab, spawnPosition.position, fireAtRotation);

        train.GetComponent<Rigidbody>().AddForce((dir + (target.GetComponent<Rigidbody>().velocity)) * farballSpeed);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, updateNewPosition_Radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
