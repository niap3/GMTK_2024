using UnityEngine;

public class CapsuleMover : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public GameObject slabGenLeft;
    public GameObject slabGenRight;
    public GameObject bgRockStacker;
    public GameObject creviceGen;

    private RockSlabGenerator slabGenLeftComp;
    private RockSlabGenerator slabGenRightComp;
    private BGTileStacker bgRockStackerComp;
    private CreviceRandomizer creviceGenComp;

    void Start()
    {
        slabGenLeftComp = slabGenLeft.GetComponent<RockSlabGenerator>();
        slabGenRightComp = slabGenRight.GetComponent<RockSlabGenerator>(); 
        bgRockStackerComp = bgRockStacker.GetComponent<BGTileStacker>();
        creviceGenComp = creviceGen.GetComponent<CreviceRandomizer>();

        // Generate initial stacks and slabs
        for (int i = 0; i < 2; i++)
        {
            bgRockStackerComp.GenerateStacksIfNeeded(0f); // Generate 2 stacks initially
        }

        for (int i = 0; i < 5; i++)
        {
            slabGenLeftComp.GenerateSlabsIfNeeded(0f); // Generate 5 slabs initially
            slabGenRightComp.GenerateSlabsIfNeeded(0f); 
        }
    }

    private void Update()
    {
        // Move the capsule upwards when the Up Arrow key is pressed
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // Generate slabs and stacks as the capsule moves upward
            slabGenLeftComp.GenerateSlabsIfNeeded(transform.position.y);
            slabGenRightComp.GenerateSlabsIfNeeded(transform.position.y);
            bgRockStackerComp.GenerateStacksIfNeeded(transform.position.y);
            creviceGenComp.CreateLoop(transform.position.y);
        }
    }
}
