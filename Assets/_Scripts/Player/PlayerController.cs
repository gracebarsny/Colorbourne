/*
 * Attached to Player (empty game object )
 *
 * Instantiates the player model and controls its movement
 *
 * Code for movement taken from Unity's Official Roll-a-Ball tutorial and Survival Shooter tutorial
 *
 * Add force relative to camera:
 * http://answers.unity3d.com/questions/207930/add-force-relative-to-camera-direction.html
 */

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public GameObject playerModel;
    public Vector3 position;
    public float speed = 1;
    public float scale;

    public GameObject cam;

    GameObject player;
    Transform camTransform;
    Vector3 movement;
    Rigidbody rb;

    public GameObject colorPickerObj;

    void Awake()
    {
        camTransform = cam.transform;
    }

    void Start()
    {
        // Instantiate the player
        player = (GameObject)Instantiate(playerModel, position, Quaternion.identity);
        player.transform.localScale = new Vector3(scale, scale, scale);
        player.transform.parent = gameObject.transform;

        // Add behavior to pick up paint buckets
        PickUpBuckets bucketPickup = player.AddComponent<PickUpBuckets>();
        bucketPickup.colorPicker = colorPickerObj.GetComponent<ColorPicker>();

        // Set tag
        player.gameObject.tag = "Player";

        // Set rigidbody properties
        rb = player.AddComponent<Rigidbody>();
        rb.drag = 10;
        rb.angularDrag = 10;
        rb.interpolation = RigidbodyInterpolation.None;
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ;

        MeshCollider collider = player.AddComponent<MeshCollider>();
        collider.convex = true;
    }

    public void Reset()
    {
        // Reset the player to the starting position
        player.transform.position = position;
    }

    void FixedUpdate()
    {
        // Store the input axes
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Move the player around the scene
        Move(moveHorizontal, moveVertical);

        if (movement != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(movement);
        }
    }

    void Move(float moveHorizontal, float moveVertical)
    {
        // Set the movement vector based on the axis input
        movement.Set(moveHorizontal, 0f, moveVertical);

        movement = camTransform.TransformDirection(movement);

        // Normalize the movement vector
        // Make it proportional to the speed per second
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player
        rb.AddForce(movement, ForceMode.Impulse);
    }
}
