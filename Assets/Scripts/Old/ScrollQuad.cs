using UnityEngine;

public class ScrollQuad : MonoBehaviour
{
    [SerializeField] float scrollSpd = 0.5f;
    Material material;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpd);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
