using System.Collections;
using UnityEngine;

public class CarCollisionScript : MonoBehaviour
{
    private PlayerScript playerScript;
    private GameControllerScript gameControllerScript;
    private GameObject collisionMarker;
    private void Awake()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
    }

    private void Start()
    {
        collisionMarker = GameObject.Find("CollisionMarker").gameObject;
        gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Collision"))
        {
            StartCoroutine(WaitAndReset(collision));
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Marker"))
        {
            StartCoroutine(WaitAndComplete(collision));
        }
        else
        {
            print("We collided with a " + LayerMask.LayerToName(collision.gameObject.layer));
        }
    }

    IEnumerator WaitAndReset(Collision collision)
    {

        playerScript.Pause();
        collisionMarker.GetComponent<MeshRenderer>().material.SetColor("YellowMarker", new Color(255, 255, 0));
        collisionMarker.transform.position = new Vector3(collision.contacts[0].point.x, 1, collision.contacts[0].point.z);
        yield return new WaitForSeconds(1);
        collisionMarker.transform.position = new Vector3(0, -1, 0);
        gameControllerScript.ResetPath();
    }

    IEnumerator WaitAndComplete(Collision collision)
    {
        playerScript.Pause();
        //This should change to something else in the future for better feedback, maybe a time rewind animation
        //Even just a simple checkmark in the middle of the screen would be better than a collision marker display
        collisionMarker.GetComponent<MeshRenderer>().material.SetColor("GreenMarker", new Color(159,173,78));
        collisionMarker.transform.position = new Vector3(collision.contacts[0].point.x, 1, collision.contacts[0].point.z);
        yield return new WaitForSeconds(1);
        collisionMarker.transform.position = new Vector3(0, -1, 0);
        gameControllerScript.CompletePath();
    }
}
