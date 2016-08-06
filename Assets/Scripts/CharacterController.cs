using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {

    public Vector2 areaPatrol;
    public float patrolUpdate;
	public float cells;
    public float distancia = 5f;
    public float life = 1f;
    private bool alive = true;
    private Vector3 randomDestination;
    private Vector3 startPosition;
    public Image HPoints;
    public GameObject particles;
	private Vector3 positionTemp;
    private GameObject player;
    private Animator enemyAnimator;
    private NavMeshAgent agent;
    private EnemyState currentState;

    // Use this for initialization
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.PATROL;
        startPosition = transform.position;
        //StartCoroutine("Patrol");
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
		if (Vector3.Distance (transform.position, positionTemp) < 1f) 
		{
			agent.SetDestination(transform.position);
		}
        /*if (false)
        {
            //HPoints.transform.LookAt(camera.transform);
            if (!finish && !CameraManager.instance.finish)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < distancia)
                    currentState = EnemyState.CHASING;
                else
                    currentState = EnemyState.PATROL;
                if (currentState == EnemyState.CHASING)
                {
                    agent.SetDestination(player.transform.position);
                    //enemyAnimator.SetFloat ("Speed", agent.velocity.magnitude);
                }
                enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
                if (Vector3.Distance(transform.position, player.transform.position) < 0.8f)
                    player.SendMessage("Die", this.gameObject);
            }
            else
            {
                if (!finish)
                {
                    finish = true;
                    agent.SetDestination(transform.position);
                    enemyAnimator.SetFloat("Speed", 0);
                    Debug.Log("Vivo:" + alive);
                }
            }
        }*/
    }

    IEnumerator Patrol()
    {
            if (currentState == EnemyState.PATROL)
            {
                randomDestination = startPosition + new Vector3(Random.Range(-areaPatrol.x, areaPatrol.x), 0, Random.Range(-areaPatrol.y, areaPatrol.y));
                agent.SetDestination(randomDestination);
            }
        yield return new WaitForSeconds(patrolUpdate);
        StartCoroutine("Patrol");
    }

    IEnumerator Die()
    {
        particles.SetActive(true);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
	}

	public void Wave()
	{
		enemyAnimator.SetTrigger("Wave");
		agent.SetDestination(transform.position);
		enemyAnimator.SetFloat("Speed", 0);
	}

	public void BeginAttack()
	{
		enemyAnimator.SetFloat("Speed", 0);
		enemyAnimator.SetTrigger("BeginAttack");
	}

    public void Damage()
    {
        life -= 0.1f;
        HPoints.fillAmount = life;
        if (life <= 0)
        {
            agent.SetDestination(transform.position);
            enemyAnimator.SetFloat("Speed", 0);
            alive = false;
            StartCoroutine("Die");
        }
    }
    public void moveTo(GameObject destination)
    {
		positionTemp = destination.transform.position;
		transform.LookAt (positionTemp);
		agent.SetDestination(positionTemp);
    }
}
