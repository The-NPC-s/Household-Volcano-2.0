using Mono.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    HashSet<Transform> Obstructions = new HashSet<Transform>();

    GameObject Player;
    private float DistanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
    }

    void LateUpdate()
    {
        ViewObstructed();
    }

    void ViewObstructed()
    {
        // check if there is anything obstructing the view of the player
        if (Physics.Raycast(transform.position, (Player.transform.position - transform.position) + new Vector3(0,1,0), out RaycastHit hit, DistanceToPlayer + 0.1f))
        {
            if (!hit.collider.gameObject.CompareTag("Player"))
            {
                //If the object has not already been bloking the view then hide the object and add it to the list of obstructions
                if (!Obstructions.Contains(hit.transform))
                {
                    Obstructions.Add(hit.transform);
                    hit.transform.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
            }
            // Once line of sight has been restored to the player unhide all previously hidden objects and clear the list
            else
            {
                foreach (Transform Obstruction in Obstructions)
                {
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                }
                Obstructions.Clear();
            }
        }
    }
}
