using UnityEngine;

public class EdgeWallsGenerator : MonoBehaviour
{
    public float wallThickness = 0.1f; // Thickness of the wall
    public float offsetDistance = 0.1f; // Offset distance from the edge of the object
    public Color rayColor = Color.red; // Color of the raycast lines

    void Start()
    {
        GenerateEdgeWalls();
    }

    void GenerateEdgeWalls()
    {
        // Loop through each child object of the Level
        foreach (Transform child in transform)
        {
            // Get the bounds of the child object
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null)
            {
                Bounds bounds = renderer.bounds;

                // Get the collision box of the child object
                Collider collider = child.GetComponent<Collider>();
                if (collider != null)
                {
                    // Create walls for each face of the object
                    CreateWallAtFace(child, bounds, Vector3.right, "Right", collider);   // Right face
                    CreateWallAtFace(child, bounds, Vector3.left, "Left", collider);     // Left face
                    //CreateWallAtFace(child, bounds, Vector3.up, "Top", collider);        // Top face
                    //CreateWallAtFace(child, bounds, Vector3.down, "Bottom", collider);   // Bottom face
                    CreateWallAtFace(child, bounds, Vector3.forward, "Front", collider); // Front face
                    CreateWallAtFace(child, bounds, Vector3.back, "Back", collider);     // Back face
                }
            }
        }
    }

    void CreateWallAtFace(Transform child, Bounds bounds, Vector3 faceDirection, string faceName, Collider collider)
    {
        // Calculate the center of the face
        Vector3 faceCenter = bounds.center + bounds.extents.x * faceDirection;
        Vector3 tempColliderCenter = faceCenter + faceDirection * (offsetDistance + 0.1f);

        // Check for collisions with other objects
        bool canCreateWall = !Physics.CheckSphere(tempColliderCenter, offsetDistance);


        if (!canCreateWall)
        {
            Collider[] colliders = Physics.OverlapSphere(tempColliderCenter, offsetDistance);
            foreach (Collider col in colliders)
            {
                if (col != collider)
                {
                    Debug.Log("Collided with: " + col.name);
                }
            }
        }

        // If no obstructions, create the wall
        if (canCreateWall)
        {
            // Create the wall at the original position
            Vector3 wallPosition = faceCenter - faceDirection * offsetDistance + Vector3.up * bounds.extents.y;
            CreateWall(wallPosition, faceDirection, bounds.size, wallThickness, child.name + "_" + faceName);
        }
    }

    void CreateWall(Vector3 position, Vector3 direction, Vector3 size, float thickness, string wallName)
    {
        // Create a wall object
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);



        // Set the wall's name
        wall.name = wallName;

        // Set the wall's position and rotation
        wall.transform.position = position;

        // Set the wall's scale to match the length and thickness
        if (direction == Vector3.up || direction == Vector3.down)
        {
            wall.transform.localScale = new Vector3(size.x, thickness, size.z);
        }
        else if (direction == Vector3.left || direction == Vector3.right)
        {
            wall.transform.localScale = new Vector3(thickness, size.y, size.z);
        }
        else
        {
            wall.transform.localScale = new Vector3(size.x, size.y, thickness);
        }

        // Make the wall invisible
        wall.GetComponent<Renderer>().enabled = false;
    }
}
