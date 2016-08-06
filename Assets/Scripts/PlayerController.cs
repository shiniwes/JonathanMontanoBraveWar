using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	private Animator playerAnimator;
	private CapsuleCollider capsule;
	public GameObject particleAttack;
	public GameObject shoot;
	private float initialCapsuleColliderHeight;
	private bool alive = true;
	public Text dieText;
	public float life = 1f;
	private float startTime;
	public Image HPoints;
	private bool finish = false;
	#if UNITY_IOS || UNITY_ANDROID
	private Vector3 accel;
	private Touch finger;
	#endif

	// Use this for initialization
	void Start () {
		playerAnimator = GetComponent<Animator>();
		capsule = GetComponent<CapsuleCollider> ();
		initialCapsuleColliderHeight = capsule.height;
	}
	
	// Update is called once per frame
	void Update () {
        #if UNITY_STANDALONE
		if (!GameManager.instance.finishCurrentGame)
        {
            playerAnimator.SetFloat("Direction", Input.GetAxis("Horizontal"));
            playerAnimator.SetFloat("Speed", Input.GetAxis("Vertical"));
            if (Input.GetKeyDown(KeyCode.Space) && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Locomotion"))
                playerAnimator.SetTrigger("Jump");
            if (Input.GetKeyDown(KeyCode.M))
                playerAnimator.SetTrigger("Wave");
            if (Input.GetKeyDown(KeyCode.N))
            {
                playerAnimator.SetTrigger("Shoot");
				StartCoroutine("Shoot");
            }
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                if (playerAnimator.GetFloat("ColliderHeight") > 0)
                    capsule.height = playerAnimator.GetFloat("ColliderHeight");
                else
                    capsule.height = initialCapsuleColliderHeight;
            //Debug.Log ("alive:" + alive);
        }
        else {
            //dieText.transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(12, 12, 1), (Time.time - startTime) / 2);
            //if ((Time.time - startTime) >= 5f)
            //    SceneManager.LoadScene("Animations");
        }
        #endif
        
    }

	IEnumerator Die(){
		playerAnimator.SetTrigger ("Die");
		yield return new WaitForSeconds (3);
		GameManager.instance.finishCurrentGame = true;
		GameManager.instance.setMenuFinish ();
		Destroy(this.gameObject);
	}
	IEnumerator Shoot(){
		particleAttack.transform.position = transform.position+transform.forward*3;
		particleAttack.SetActive (true);
		yield return new WaitForSeconds (3);
		particleAttack.SetActive (false);
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log ("collision.transform.tag:"+collision.transform.tag);
		if (collision.transform.tag == "DamageItem2") {
			//Debug.Log ("daño");
			Damage ();
		}
	}

	public void Damage(){
		life -= 0.1f;
		HPoints.fillAmount = life;
		if (life <= 0) {
			finish = true;
			alive = false;
			StartCoroutine ("Die");
		}
	}
}
