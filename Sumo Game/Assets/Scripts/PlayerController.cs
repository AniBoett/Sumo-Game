using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
 	
	public float speed = 5.0f;
    private GameObject focalPoint;
	public bool hasPowerup;
	private float powerupStrength = 15.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        //player moves forwards relative to the direction of the camera
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Powerup")) {
			hasPowerup = true;
			Destroy(other.gameObject);
			StartCoroutine(PowerupCountdownRoutine());
		}
	}
	
	IEnumerator PowerupCountdownRoutine() 
	{
		yield return new WaitForSeconds(7);
		hasPowerup = false;
	}

	private void OnCollisionEnter(Collision collision) 
	{
		//when the player has a powerup and collides with the enemy, the enemy goes flying!
		if (collision.gameObject.CompareTag("Enemy") && hasPowerup) 
		{
			Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
			Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
			enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
		}
	}
}
