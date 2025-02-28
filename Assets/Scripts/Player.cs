using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    // ***********************( Declaraciones )*********************** //
    Rigidbody2D _rb;
    Queue<KeyCode> inputBuffer;
    Queue<KeyCode> inputBufferSalto;
    SpriteRenderer _spriteRenderer;
    GameObject _objetoRayCast;

    // Declaraciones de Referencias //
    public LayerMask _MascaraObjetos_lm;
    public LayerMask _MascaraSuelo_lm;
    [SerializeField]
    GameObject _Mano_go;

    // variables comunes //
    bool _levantando = false;
    float tiempoEnElAire;
    public float coyoteTime = 0.2f;
    public float _feurzaSalto_f = 10f;
    public float _fuerzaMovimiento_f = 10f;
    private bool _saltar_b = true;
    private const float _coyoteEpsilon_f = 0.01f;



    // ***********************( Metodos de UNITY )*********************** //
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        inputBuffer = new Queue<KeyCode>();
        inputBufferSalto = new Queue<KeyCode>();
    }

    void Update()
    {
        esquinas();

        if (Input.GetKey(KeyCode.A))
        {
            inputBuffer.Enqueue(KeyCode.A);
            Invoke("quitarAccion", 0.5f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputBuffer.Enqueue(KeyCode.D);
            Invoke("quitarAccion", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputBufferSalto.Enqueue(KeyCode.Space);
            Invoke("quitarSalto", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            inputBuffer.Enqueue(KeyCode.F);
            Invoke("quitarAccion", 0.5f);
        }

        InputBuffer();
        InputBufferSalto();
    }


    // ***********************( Metodos NESTROS )*********************** // 
    // --- InputBuffer --- //
    void InputBuffer()
    {
        if (inputBuffer.Count > 0)
        {
            // Cojer Objeto
            if (inputBuffer.Peek() == KeyCode.F)
            {
                cojerObjeto();
                inputBuffer.Dequeue();
            }

            // Ir izquierda
            if (inputBuffer.Peek() == KeyCode.A)
            {
                RaycastHit2D _hitArriba_rh = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 1f), Vector2.left, 0.2f, _MascaraSuelo_lm);
                RaycastHit2D _hitAbajo_rh = Physics2D.Raycast(transform.position + new Vector3(-0.5f, -1f), Vector2.left, 0.2f, _MascaraSuelo_lm);

                if (!_hitArriba_rh && !_hitAbajo_rh)
                    _rb.AddForce(new Vector2((0 - _fuerzaMovimiento_f) * Time.deltaTime, 0));
                

                _spriteRenderer.flipX = true;

                Vector3 _manoPosicion_v3 = _Mano_go.transform.localPosition;
                _manoPosicion_v3.x = -Mathf.Abs(_manoPosicion_v3.x);
                _Mano_go.transform.localPosition = _manoPosicion_v3;

                inputBuffer.Dequeue();
            }

            // Ir derecha
            else if (inputBuffer.Peek() == KeyCode.D)
            {
                RaycastHit2D _hitArriba_rh = Physics2D.Raycast(transform.position + new Vector3(0.5f, 1f), Vector2.right, 0.2f, _MascaraSuelo_lm);
                RaycastHit2D _hitAbajo_rh = Physics2D.Raycast(transform.position + new Vector3(0.5f, -1f), Vector2.right, 0.2f, _MascaraSuelo_lm);

                if (!_hitArriba_rh && !_hitAbajo_rh)
                    _rb.AddForce(new Vector2(_fuerzaMovimiento_f * Time.deltaTime, 0));
                
                _spriteRenderer.flipX = false;

                Vector3 _manoPosicion_v3 = _Mano_go.transform.localPosition;
                _manoPosicion_v3.x = Mathf.Abs(_manoPosicion_v3.x);
                _Mano_go.transform.localPosition = _manoPosicion_v3;

                inputBuffer.Dequeue();
            }
        }
    }
    void InputBufferSalto()
    {
        RaycastHit2D raycastsuelo = Physics2D.Raycast(transform.position, Vector2.down, 1.25f, _MascaraSuelo_lm);

        if (raycastsuelo == true)
        {
            tiempoEnElAire = 0;
        }
        else
        {
            tiempoEnElAire += Time.deltaTime;
        }


        if (inputBufferSalto.Count > 0)
        {
            // Debug.Log
            // (
            //     " => Salto: " + _saltar_b +
            //     " => RayCastSuelo: " + (raycastsuelo == true) +
            //     " => TiempoEnElAire: " + tiempoEnElAire +
            //     " => CoyoteTiempo: " + coyoteTime +
            //     " => AireCoyote: " + (tiempoEnElAire < coyoteTime) +
            //     " => AireCoyoteEpsilon: " + (tiempoEnElAire < (coyoteTime - _coyoteEpsilon_f)) +
            //     " => if: " + ((_saltar_b == true) && (raycastsuelo == true || tiempoEnElAire < (coyoteTime - _coyoteEpsilon_f)))
            // );

            // Salto
            if ((_saltar_b == true) && (raycastsuelo == true || tiempoEnElAire < (coyoteTime - _coyoteEpsilon_f)))
            {
                Debug.Log("--SALTAR--");

                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _feurzaSalto_f);
                inputBufferSalto.Dequeue();
                _saltar_b = false;
                StartCoroutine(saltarTrue());
            }
        }  
    }

    IEnumerator saltarTrue()
    {
        yield return new WaitForSeconds(coyoteTime + _coyoteEpsilon_f);
        Debug.Log("-- VUELVES a SALTAR --");
        _saltar_b = true;
    }

    void quitarAccion()
    {
        if (inputBuffer.Count > 0)
            inputBuffer.Dequeue();
    }
    void quitarSalto()
    {
        if (inputBufferSalto.Count > 0)
            inputBufferSalto.Dequeue();
    }

    IEnumerator quitarAccionConRetraso()
    {
        yield return new WaitForSeconds(0.5f);
        if (inputBuffer.Count > 0)
            inputBuffer.Dequeue();
    }


    // --- Cojer/Levantar Objeto --- //
    void cojerObjeto()
    {
        Vector2 _paLante_v2 = _spriteRenderer.flipX ? Vector2.left : Vector2.right;

        // soltar delante
        if (_levantando == true)
        {
            RaycastHit2D pared = Physics2D.Raycast(transform.position, _paLante_v2, 1.5f, _MascaraSuelo_lm);

            if (pared.collider != null)
            {
                transform.position -= new Vector3(_paLante_v2.x, _paLante_v2.y, 0f);
            }


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
            for (float i = 1f; i >= -1f; i -= 0.5f)
            {
                RaycastHit2D _hit = Physics2D.Raycast(transform.position + new Vector3(0, i), _paLante_v2, 2, _MascaraObjetos_lm);

                //Debug.Log("Rayo: " + (transform.position + new Vector3(0, i)) + " i:" + i );
                if (_hit.collider != null)
                {
                    Debug.Log("El rayo ha colisionado con: " + _hit.collider.name);
                    levantarObjeto(_hit.collider.gameObject);
                    _objetoRayCast = _hit.collider.gameObject;

                    return;
                }
            }
        }
    }
    void levantarObjeto(GameObject _Objeto_go)
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


    // --- Correcion de esquinas --- //
    void esquinas()
    {
        RaycastHit2D raycastIzquierda = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 1f), Vector2.up, 0.25f, _MascaraSuelo_lm);
        RaycastHit2D raycastDerecha = Physics2D.Raycast(transform.position + new Vector3(0.5f, 1f), Vector2.up, 0.25f, _MascaraSuelo_lm);
       
        if (raycastIzquierda && !raycastDerecha)
        {
            while (true)
            {
                transform.position += new Vector3(0.25f, 0);
                raycastIzquierda = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 1f), Vector2.up, 0.25f, _MascaraSuelo_lm);
                if (!raycastIzquierda)
                {
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, (_feurzaSalto_f / 10) * 8);
                    break; 
                }
            }
        }
        else if (raycastDerecha && !raycastIzquierda)
        {
            while (true)
            {
                transform.position -= new Vector3(0.25f, 0);
                raycastDerecha = Physics2D.Raycast(transform.position + new Vector3(0.5f, 1f), Vector2.up, 0.25f, _MascaraSuelo_lm);
                if (!raycastDerecha)
                {
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, (_feurzaSalto_f / 10) * 8);
                    break;
                }
            }
        }
    }
    /*
     void esquinas()
    {

        Vector3 offsetIzq = new Vector3(-0.5f, 0.5f);
        Vector3 offsetDer = new Vector3(0.5f, 0.5f);
        float alturaSubida = 0.5f;

        RaycastHit2D raycastIzquierda = Physics2D.Raycast(transform.position + offsetIzq, Vector2.up, 0.8f, _MascaraSuelo_lm);
        RaycastHit2D raycastDerecha = Physics2D.Raycast(transform.position + offsetDer, Vector2.up, 0.8f, _MascaraSuelo_lm);

        if (raycastIzquierda && !raycastDerecha)
        {
            transform.position += new Vector3(0.1f, alturaSubida, 0);
        }
        else if (raycastDerecha && !raycastIzquierda)
        {
            transform.position += new Vector3(-0.1f, alturaSubida, 0);
        }
    }
    */

}