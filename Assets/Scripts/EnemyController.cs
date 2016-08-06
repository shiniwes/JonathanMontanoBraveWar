using UnityEngine;
using System.Collections;
using UnityEngine.UI;
enum EnemyState {PATROL, CHASING}

public class EnemyController : MonoBehaviour {
	public Vector2 areaPatrol;
	public float patrolUpdate;
	public float distancia = 5f;
	private bool finish = false;
	public float life = 1f;
	private bool alive = true;
	private Vector3 randomDestination;
	private Vector3 startPosition;
	public Image HPoints;
    public GameObject particles;

	public GameObject player;
	private GameObject camera;
	private Animator enemyAnimator;
	private NavMeshAgent agent;
	private EnemyState currentState;

	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		currentState = EnemyState.PATROL;
		startPosition = transform.position;
		currentState = EnemyState.CHASING;StartCoroutine ("Patrol");
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.finishCurrentGame)
        {
            HPoints.transform.LookAt(camera.transform);
            if (!finish) {
                enemyAnimator.SetFloat("Speed", agent.velocity.magnitude);
                //if (Vector3.Distance(transform.position, player.transform.position) < 0.8f)
                //    player.SendMessage("Die", this.gameObject);
            } 
        }
	}

	IEnumerator Patrol(){
		if (!GameManager.instance.finishCurrentGame)
        {
			if (Vector3.Distance (transform.position, player.transform.position) < 3f) {
				//ataca
				enemyAnimator.SetTrigger("Shoot");
				transform.LookAt (player.transform.position);
				enemyAnimator.SetFloat ("Speed", 0);
				//Debug.Log ("Ataca");
				agent.SetDestination (transform.position);
				yield return new WaitForSeconds (3f);
				randomDestination = startPosition + new Vector3 (Random.Range (-50f, 50f), 0, Random.Range (-10f, 10f));
				agent.SetDestination (randomDestination);
				currentState = EnemyState.CHASING;
				yield return new WaitForSeconds (2f);
			} else {
				//Debug.Log ("persigue");
				agent.SetDestination(player.transform.position);
				yield return new WaitForSeconds (2f);
			}
			StartCoroutine ("Patrol");
        }
	}

	IEnumerator Die(){
		enemyAnimator.SetTrigger ("Die");
        yield return new WaitForSeconds (3);
		GameManager.instance.finishCurrentGame = true;
		GameManager.instance.setMenuFinish ();
		Destroy(this.gameObject);
	}

	public void Wave (){
		enemyAnimator.SetTrigger ("Wave");
		finish = true;
		agent.SetDestination (transform.position);
		enemyAnimator.SetFloat ("Speed", 0);
	}

	public void Damage(){
		life -= 0.2f;
		HPoints.fillAmount = life;
		if (life <= 0) {
			finish = true;
			agent.SetDestination (transform.position);
			enemyAnimator.SetFloat ("Speed", 0);
			alive = false;
			StartCoroutine ("Die");
		}
	}
	void OnCollisionEnter(Collision collision) {
		//Debug.Log ("collision.transform.tag:"+collision.transform.tag);
		if (collision.transform.tag == "DamageItem") {
			//Debug.Log ("daño");
			Damage ();
		}
	}
}
