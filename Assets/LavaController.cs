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
                    // Lava does not rise
                    break;
                case 1:
                    if (transform.position.y < 1.11f)
                    {
                        Rise(1);
                    }
                    
                    break;
                case 2:
                    if (transform.position.y < 2.83f)
                    {
                        Rise(7);
                    } else
                    if (transform.position.y < 4.64f)
                    {
                        Rise(1);
                    }
                    break;
                case 3:
                    break;
                default:
                    break;


            }
        }
    }

    private void Rise(int multiplier)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed*multiplier, transform.position.z);
    }
}
