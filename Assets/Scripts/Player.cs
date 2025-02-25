using UnityEngine;

public class Player : MonoBehaviour
{
    // **** Variables **** //
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    public LayerMask layerMask;
    public float jumpForce;
    [SerializeField]
    GameObject _Mano_go;

    // VARs //
    bool _levantando = false;
    bool canJump;
    GameObject _objetoRayCast;
    FixedJoint2D _eje;

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
            _rb.AddForce(new Vector2(-1000f * Time.deltaTime, 0));
            _spriteRenderer.flipX = true;

            Vector3 _manoPosicion_v3 = _Mano_go.transform.localPosition;
            _manoPosicion_v3.x = -Mathf.Abs(_manoPosicion_v3.x);
            _Mano_go.transform.localPosition = _manoPosicion_v3;
        }

        if (Input.GetKey("d"))
        {
            _rb.AddForce(new Vector2(1000f * Time.deltaTime, 0));
            _spriteRenderer.flipX = false;

            Vector3 _manoPosicion_v3 = _Mano_go.transform.localPosition;
            _manoPosicion_v3.x = Mathf.Abs(_manoPosicion_v3.x);
            _Mano_go.transform.localPosition = _manoPosicion_v3;
        }

        if (Input.GetKeyDown("w"))
        {
            _rb.AddForce(new Vector2(0f , (100000f * 2) * Time.deltaTime));       
        }

        if (Input.GetKeyDown("f"))
        {
            Vector2 _paLante_v2 = _spriteRenderer.flipX ? Vector2.left : Vector2.right;

            // soltar delante
            if (_levantando == true)
            {
                _levantando = false;
                Rigidbody2D _rbObjeto_rb = _objetoRayCast.GetComponent<Rigidbody2D>();
                _rbObjeto_rb.simulated = true;

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
                RaycastHit2D _hit1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), _paLante_v2, 2, layerMask);
                if (_hit1.collider != null)
                {
                    Debug.Log("El rayo1 ha colisionado con: " + _hit1.collider.name);
                    cogerObjeto(_hit1.collider.gameObject);
                    _objetoRayCast = _hit1.collider.gameObject;

                    return;
                }

                RaycastHit2D _hit2 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), _paLante_v2, 2, layerMask);
                if (_hit2.collider != null)
                {
                    Debug.Log("El rayo2 ha colisionado con: " + _hit2.collider.name);
                    cogerObjeto(_hit2.collider.gameObject);
                    _objetoRayCast = _hit2.collider.gameObject;

                    return;
                }

                RaycastHit2D _hit3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), _paLante_v2, 2, layerMask);
                if (_hit3.collider != null)
                {
                    Debug.Log("El rayo3 ha colisionado con: " + _hit3.collider.name);
                    cogerObjeto(_hit3.collider.gameObject);
                    _objetoRayCast = _hit3.collider.gameObject;

                    return;
                }

                RaycastHit2D _hit4 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), _paLante_v2, 2, layerMask);
                if (_hit4.collider != null)
                {
                    Debug.Log("El rayo4 ha colisionado con: " + _hit4.collider.name);
                    cogerObjeto(_hit4.collider.gameObject);
                    _objetoRayCast = _hit4.collider.gameObject;

                    return;
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
            _rbObjeto_rb.simulated = false;
        }
        float alturaObjeto = _Objeto_go.GetComponent<Renderer>().bounds.size.y;

        _Objeto_go.transform.SetParent(transform);
        _Objeto_go.transform.rotation = Quaternion.identity;
        _Objeto_go.transform.localPosition = new Vector3(0, (alturaObjeto / 2) + 1f, 0);
    }

  


     
}