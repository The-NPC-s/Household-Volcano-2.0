using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public bool lava_enabled = true;
    public float speed = 0.01f;
    public float current_stage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lava_enabled)
        {
            switch (current_stage)
            {
                case 0:
                    if (transform.position.y < 1.11f) 
                    {
                        Rise();
                    }
                    break;
                case 1:
                    if (transform.position.y < 4.64f)
                    {
                        Rise();
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;


            }
        }
    }

    private void Rise()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
    }
}
