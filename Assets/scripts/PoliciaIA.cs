using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PoliciaIA : MonoBehaviour
{
    public float detectionRange = 15f;
    public float pursuitTime = 20f;
    public float patrolSpeed = 8f;
    public float pursuitSpeed = 20f;
    public float wanderRadius = 40f; // radio para moverse aleatoriamente

    private NavMeshAgent agent;
    private Transform player;
    private float pursuitTimer = 0f;
    private bool isPursuing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent.speed = patrolSpeed;
        MoveToRandomPosition(); // empieza moviéndose aleatoriamente
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Detecta jugador
        if (distanceToPlayer < detectionRange)
        {
            isPursuing = true;
            pursuitTimer = pursuitTime;
        }

        if (isPursuing)
        {
            agent.speed = pursuitSpeed;
            agent.SetDestination(player.position);
            pursuitTimer -= Time.deltaTime;

            if (pursuitTimer <= 0f || distanceToPlayer > detectionRange * 1.5f)
            {
                isPursuing = false;
                agent.speed = patrolSpeed;
                MoveToRandomPosition(); // volver a patrullar aleatoriamente
            }
        }
        else
        {
            // Si llegó al destino o se quedó quieto, ir a otro punto aleatorio
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                MoveToRandomPosition();
            }

            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
            {
                MoveToRandomPosition();
            }
        }
    }

    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Jugador atrapado!");
            PlayerPrefs.SetInt("Score", ScoreManager.Instance.score);
            SceneManager.LoadScene("GameOver");
        }
    }

}
