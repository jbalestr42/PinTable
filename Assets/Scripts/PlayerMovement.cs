using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _speed = 5f;
    Vector3 _direction;

	void Start()
    {
        _direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * _speed;

        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    void OnCollisionEnter2D(Collision2D p_collision)
    {
        if (p_collision.gameObject.tag == "WallX")
        {
            _direction = new Vector3(-_direction.x, _direction.y, 0f);
        }
        if (p_collision.gameObject.tag == "WallY")
        {
            _direction = new Vector3(_direction.x, -_direction.y, 0f);
        }
    }
}
