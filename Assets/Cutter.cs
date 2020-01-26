using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Cutter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setupNewInteractable(GameObject newObject)
    {
        newObject.AddComponent<Throwable>();
        newObject.AddComponent<MeshCollider>();

        newObject.GetComponent<Interactable>().hideHighlight = new GameObject[0];
        newObject.GetComponent<Throwable>().onPickUp = new UnityEngine.Events.UnityEvent();
        newObject.GetComponent<Throwable>().onDetachFromHand = new UnityEngine.Events.UnityEvent();
    }

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Splittable")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Cutting!");
            GameObject cutted = collision.gameObject;
            Transform position = cutted.transform;

            var newPrefab = cutted.GetComponent<Cuttable>().newPrefab;

            if (!newPrefab)
            {
                Debug.Log("No newprefab after cutting set!");
                return;
            }

            // Split into two
            GameObject newObj = Instantiate(newPrefab, position.position  + new Vector3(0, 0.1f, 0), position.rotation);
            GameObject newObj2 = Instantiate(newPrefab, position.position + new Vector3(0.1f,0.1f,0), position.rotation);

            // Destroying casuses error so just yeet it out into outer space
            cutted.transform.position = new Vector3(0,-100.0f,0);

            newObj.transform.localScale = position.localScale;
            newObj2.transform.localScale = position.localScale;

            setupNewInteractable(newObj);
            setupNewInteractable(newObj2);

        }

    }
}
