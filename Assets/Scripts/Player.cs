using UnityEngine;

public class Player : MonoBehaviour
{
    // **** Variables **** //
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    public LayerMask layerMask;

    // VARs //
    bool _levantando = false;
    GameObject _objetoRayCast;
    FixedJoint2D _joint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            _rb.AddForce(new Vector2(-1000F * Time.deltaTime, 0));
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKey("d"))
        {
            _rb.AddForce(new Vector2(1000F * Time.deltaTime, 0));
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown("f"))
        {
            Vector2 _paLante_v2 = _spriteRenderer.flipX ? Vector2.left : Vector2.right;

            // soltar delante
            if (_levantando == true)
            {
                _levantando = false;
                if (_joint != null)
                {
                    Destroy(_joint);
                }

                _objetoRayCast.transform.SetParent(null);

                // Posicionar el objeto delante del jugador
                _objetoRayCast.transform.position = new Vector3
                    (
                        transform.position.x + _paLante_v2.x,
                        transform.position.y, 
                        transform.position.z
                    );
                _objetoRayCast = null;
            }
            else
            {

                // Lanzar un rayo hacia adelante desde la posición del jugador
                RaycastHit2D _hit1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), _paLante_v2, 100, ~layerMask);
                RaycastHit2D _hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), _paLante_v2, 100, ~layerMask);
                RaycastHit2D _hit3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), _paLante_v2, 100, ~layerMask);
                RaycastHit2D _hit4 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), _paLante_v2, 100, ~layerMask);

                // Verificar si el rayo ha colisionado con algún objeto
                if (_hit1.collider != null)
                {
                    Debug.Log("El rayo1 ha colisionado con: " + _hit1.collider.name);
                    cogerObjeto(_hit1.collider.gameObject);
                    _objetoRayCast = _hit1.collider.gameObject;
                }
                else if (_hit2.collider != null)
                {
                    Debug.Log("El rayo2 ha colisionado con: " + _hit2.collider.name);
                    cogerObjeto(_hit2.collider.gameObject);
                    _objetoRayCast = _hit2.collider.gameObject;
                }
                else if (_hit3.collider != null)
                {
                    Debug.Log("El rayo3 ha colisionado con: " + _hit3.collider.name);
                    cogerObjeto(_hit3.collider.gameObject);
                    _objetoRayCast = _hit3.collider.gameObject;
                }
                else if (_hit4.collider != null)
                {
                    Debug.Log("El rayo4 ha colisionado con: " + _hit4.collider.name);
                    cogerObjeto(_hit4.collider.gameObject);
                    _objetoRayCast = _hit4.collider.gameObject;
                }
            }
        }
    }

    void cogerObjeto(GameObject _Objeto_go)
    {
        _levantando = true;
        Rigidbody2D _rbObjeto_rb = _Objeto_go.GetComponent<Rigidbody2D>();
        if (_rbObjeto_rb != null)
        {
            _rbObjeto_rb.Sleep();
            _joint = gameObject.AddComponent<FixedJoint2D>();
            _joint.connectedBody = _rbObjeto_rb;
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = new Vector2(0, -1.6f);
        }

        _Objeto_go.transform.SetParent(transform);

        //_Objeto_go.transform.localPosition = new Vector3(0, 0.9f, 0);
    }
}