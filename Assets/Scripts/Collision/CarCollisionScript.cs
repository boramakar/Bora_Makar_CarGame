using System.Collections;
using UnityEngine;

public class CarCollisionScript : MonoBehaviour
{
    PlayerScript playerScript;
    GameControllerScript gameControllerScript;
    private void Awake()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        gameControllerScript = GameObject.Find("GameSettings").GetComponent<GameControllerScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Collision"))
        {
            StartCoroutine(WaitAndReset(collision));
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Marker")) {
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
        GameObject.Find("CollisionMarker").gameObject.transform.position = new Vector3(collision.contacts[0].point.x, 1, collision.contacts[0].point.z);
        yield return new WaitForSeconds(1);
        GameObject.Find("CollisionMarker").gameObject.transform.position = new Vector3(0, -1, 0);
        gameControllerScript.ResetPath();
    }

    IEnumerator WaitAndComplete(Collision collision)
    {
        playerScript.Pause();
        //This should change to something else in the future for better feedback, maybe a time rewind animation
        //Even just a simple checkmark in the middle of the screen would be better than a collision marker display
        GameObject.Find("CollisionMarker").gameObject.transform.position = new Vector3(collision.contacts[0].point.x, 1, collision.contacts[0].point.z);
        yield return new WaitForSeconds(1);
        GameObject.Find("CollisionMarker").gameObject.transform.position = new Vector3(0, -1, 0);
        gameControllerScript.CompletePath();
    }
}
