using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerSpeed = 10;
    public Rigidbody bullet;

    void Update()
    {
        float moveBy = (Input.GetAxis("Horizontal") * playerSpeed) * Time.deltaTime;
        transform.Translate(Vector3.right * moveBy);

        if (Input.GetKeyDown("space"))
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }
}
