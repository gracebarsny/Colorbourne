// NO LONGER USED

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Room : MonoBehaviour
{
    /*
    public PhysicMaterial bouncy;

    // Array of Instantiated Objects
    public Paintable[] focalObjects;
    public NonFocalObject[] nonFocalObjects;
    public NonPaintable[] structuralObjects;

    // Arrays of Prefabs
    private GameObject[] focalPrefabs;
    private GameObject[] nonFocalPrefabs;
    private GameObject[] structuralPrefabs; // walls, floor, background, etc.

    // Arrays of Object Locations
    private Vector3[] focalObjectLocations;
    private Vector3[] nonFocalObjectLocations;
    private Vector3[] structuralObjectLocations;

    // Arrays of Object Rotations
    private Vector3[] focalObjectRotations;
    private Vector3[] nonFocalObjectRotations;
    private Vector3[] structuralObjectRotations;

    // Arrays of Object Scales
    private Vector3[] focalObjectScales;
    private Vector3[] nonFocalObjectScales;
    private Vector3[] structuralObjectScales;

    // Get object variables (alphabetical)
    public GameObject background;
    public Vector3 backgroundLocation;
    public Vector3 backgroundRotation;
    public Vector3 backgroundScale;

    public GameObject bed;
    public Vector3 bedLocation;
    public Vector3 bedRotation;
    public Vector3 bedScale;

    public GameObject book1;
    public Vector3 book1Location;
    public Vector3 book1Rotation;
    public Vector3 book1Scale;

    public GameObject book2;
    public Vector3 book2Location;
    public Vector3 book2Rotation;
    public Vector3 book2Scale;

    public GameObject carpet;
    public Vector3 carpetLocation;
    public Vector3 carpetRotation;
    public Vector3 carpetScale;

    public GameObject ceiling;
    public Vector3 ceilingLocation;
    public Vector3 ceilingRotation;
    public Vector3 ceilingScale;

    public GameObject desk;
    public Vector3 deskLocation;
    public Vector3 deskRotation;
    public Vector3 deskScale;

    public GameObject door;
    public Vector3 doorLocation;
    public Vector3 doorRotation;
    public Vector3 doorScale;

    public GameObject drawer;
    public Vector3 drawerLocation;
    public Vector3 drawerRotation;
    public Vector3 drawerScale;

    public GameObject floor;
    public Vector3 floorLocation;
    public Vector3 floorRotation;
    public Vector3 floorScale;

    public GameObject lamp;
    public Vector3 lampLocation;
    public Vector3 lampRotation;
    public Vector3 lampScale;

    public GameObject leftWall;
    public Vector3 leftWallLocation;
    public Vector3 leftWallRotation;
    public Vector3 leftWallScale;

    public GameObject moon;
    public Vector3 moonLocation;
    public Vector3 moonRotation;
    public Vector3 moonScale;

    public GameObject poster;
    public Vector3 posterLocation;
    public Vector3 posterRotation;
    public Vector3 posterScale;

    public GameObject rightWall;
    public Vector3 rightWallLocation;
    public Vector3 rightWallRotation;
    public Vector3 rightWallScale;

    public GameObject trashcan;
    public Vector3 trashcanLocation;
    public Vector3 trashcanRotation;
    public Vector3 trashcanScale;

    public GameObject window;
    public Vector3 windowLocation;
    public Vector3 windowRotation;
    public Vector3 windowScale;

    void Awake()
    {
        // Set up focal object arrays
        focalObjects = new Paintable[6];
        focalPrefabs = new GameObject[6] { bed, drawer, trashcan, desk, lamp, door };
        focalObjectLocations = new Vector3[6] { bedLocation, drawerLocation, trashcanLocation, deskLocation, lampLocation, doorLocation };
        focalObjectRotations = new Vector3[6] { bedRotation, drawerRotation, trashcanRotation, deskRotation, lampRotation, doorRotation };
        focalObjectScales = new Vector3[6] { bedScale, drawerScale, trashcanScale, deskScale, lampScale, doorScale };

        // Set up non-focal object arrays
        nonFocalObjects = new NonFocalObject[5];
        nonFocalPrefabs = new GameObject[5] { window, carpet, book1, book2, poster };
        nonFocalObjectLocations = new Vector3[5] { windowLocation, carpetLocation, book1Location, book2Location, posterLocation };
        nonFocalObjectRotations = new Vector3[5] { windowRotation, carpetRotation, book1Rotation, book2Rotation, posterRotation };
        nonFocalObjectScales = new Vector3[5] { windowScale, carpetScale, book1Scale, book2Scale, posterScale };

        // Set up structural object arrays
        structuralObjects = new NonPaintable[6];
        structuralPrefabs = new GameObject[6] { leftWall, rightWall, floor, background, moon, ceiling };
        structuralObjectLocations = new Vector3[6] { leftWallLocation, rightWallLocation, floorLocation, backgroundLocation, moonLocation, ceilingLocation };
        structuralObjectRotations = new Vector3[6] { leftWallRotation, rightWallRotation, floorRotation, backgroundRotation, moonRotation, ceilingRotation };
        structuralObjectScales = new Vector3[6] { leftWallScale, rightWallScale, floorScale, backgroundScale, moonScale, ceilingScale };
    }

    void Start()
    {
        // Spawn objects
        spawnFocalObjects();
        spawnNonFocalObjects();
        spawnStructuralObjects();
    }

    void spawnFocalObjects()
    {
        for (int i = 0; i < focalPrefabs.Length; i++)
        {
            GameObject focalObj = (GameObject)Instantiate(focalPrefabs[i], focalObjectLocations[i], Quaternion.Euler(focalObjectRotations[i]));
            focalObj.transform.parent = gameObject.transform;
            focalObj.transform.localScale = focalObjectScales[i];
            focalObjects[i] = focalObj.AddComponent<Paintable>();
            focalObj.gameObject.tag = "RoomObject";

            MeshCollider meshCollider = focalObj.AddComponent<MeshCollider>();
            meshCollider.material = bouncy;
        }
    }

    void spawnNonFocalObjects()
    {
        for (int i = 0; i < nonFocalPrefabs.Length; i++)
        {
            GameObject nonFocalObj = (GameObject)Instantiate(nonFocalPrefabs[i], nonFocalObjectLocations[i], Quaternion.Euler(nonFocalObjectRotations[i]));
            nonFocalObj.transform.localScale = nonFocalObjectScales[i];
            nonFocalObjects[i] = nonFocalObj.AddComponent<NonFocalObject>();
            nonFocalObj.gameObject.tag = "RoomObject";

            if (nonFocalPrefabs[i] == window)
            {
                nonFocalObj.gameObject.layer = 10;
            }
            //nonFocalObj.AddComponent<MeshCollider>();
        }
    }


    void spawnStructuralObjects()
    {
        /*
        for (int i = 0; i < structuralPrefabs.Length; i++)
        {
            GameObject s = structuralPrefabs[i];
            if ((s != leftWall && s != rightWall) && (s != floor && s != ceiling))
            {
                GameObject structuralObj = (GameObject)Instantiate(structuralPrefabs[i], structuralObjectLocations[i], Quaternion.Euler(structuralObjectRotations[i]));
                structuralObj.transform.localScale = structuralObjectScales[i];
                structuralObjects[i] = structuralObj.AddComponent<NonPaintable>();
                structuralObj.AddComponent<MeshCollider>();
            }

            else if ((structuralPrefabs[i] == leftWall) || (structuralPrefabs[i] == rightWall))
            {
                Rigidbody rb = structuralObj.AddComponent<Rigidbody>();
                rb.angularDrag = 0;
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;

            }            
        }
    }
*/

}
