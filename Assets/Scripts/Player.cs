using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;
using System;

public class Player : MonoBehaviour
{
    // ***********************( Declaraciones )*********************** //
    // --- pausa --- //
    public bool _pausado_b = false;
    public MenuPausa _menuPausa_script;

    // --- InputBuffer --- //
    Queue<KeyCode> _inputBuffer_q;
    Queue<KeyCode> _inputBufferSalto_q;


    // --- Mascaras --- //
    public LayerMask _MascaraObjetos_lm;
    public LayerMask _MascaraSuelo_lm;
    

    // --- Variables de coyoteTime/Salto --- //
    public float coyoteTime = 0.2f;
    float tiempoEnElAire;
    private bool _saltar_b = true;
    private const float _coyoteEpsilon_f = 0.01f; // MQM

    // variables comunes //
    bool _levantando = false;
    public float _feurzaSalto_f = 10f;
    public float _fuerzaMovimiento_f = 10f;

    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    GameObject _objetoRayCast;
    Animator _animator_a;
    [SerializeField]
    Tienda _tienda_script;


    // --- variables de Audio --- //
    AudioSource _audioSource_as;
    [SerializeField] private AudioClip _sonidoSalto;
    private AudioClip _sonidoCaminar;
    [SerializeField] private AudioClip[] _pasos_array_ac;

    // --- variables de esquinas --- //
    private bool _poderCorregir_b = true;

    // --- Teclas de Movimiento Personalizadas --- //
    private KeyCode _teclaIzquierda_kc;
    private KeyCode _teclaDerecha_kc;
    private KeyCode _teclaSalto_kc;
    private KeyCode _teclaAccion_kc;


    // ***********************( Metodos de UNITY )*********************** //
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _inputBuffer_q = new Queue<KeyCode>();
        _inputBufferSalto_q = new Queue<KeyCode>();
        _animator_a = gameObject.GetComponent<Animator>();
        _audioSource_as = GetComponentInParent<AudioSource>();

        // --- teclas personalizadas --- //
        _teclaIzquierda_kc = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("izquierda", KeyCode.A.ToString()));
        _teclaDerecha_kc = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("derecha", KeyCode.D.ToString()));
        _teclaSalto_kc = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("salto", KeyCode.W.ToString()));
        _teclaAccion_kc = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("accion", KeyCode.F.ToString()));
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menuPausa_script.AbrirMenu();
            //Time.timeScale = _pausado_b ? 0 : 1;
            return;
        }
        else if (_pausado_b)
            return;

        esquinas();

        if (Input.GetKey(_teclaIzquierda_kc))
        {
            _inputBuffer_q.Enqueue(_teclaIzquierda_kc);
            Invoke("quitarAccion", 0.5f);
        }

        if (Input.GetKey(_teclaDerecha_kc))
        {
            _inputBuffer_q.Enqueue(_teclaDerecha_kc);
            Invoke("quitarAccion", 0.5f);
        }

        if (Input.GetKeyDown(_teclaSalto_kc))
        {
            _inputBufferSalto_q.Enqueue(_teclaSalto_kc);
            Invoke("quitarSalto", 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _inputBufferSalto_q.Enqueue(_teclaSalto_kc);
            Invoke("quitarSalto", 0.5f);
        }

        if (Input.GetKeyDown(_teclaAccion_kc))
        {
            _inputBuffer_q.Enqueue(_teclaAccion_kc);
            Invoke("quitarAccion", 0.5f);
        }

        inputBuffer();
        inputBufferSalto();

        // **** Animaciones
        if (estaCayendo())
            _animator_a.SetBool("cayendo", true);
        else
            _animator_a.SetBool("cayendo", false);
        


        if (estaMoviendose())
            _animator_a.SetBool("moviendose", true);
        else
            _animator_a.SetBool("moviendose", false);
        


        if (_levantando == true)
            _animator_a.SetBool("levantandoObjeto", true);
        else
            _animator_a.SetBool("levantandoObjeto", false);


        //Debug.Log("cayendo: " + _animator_a.GetBool("cayendo"));
        //Debug.Log("moviendose: " + _animator_a.GetBool("moviendose"));
        //Debug.Log("tocandoSuelo: " + _animator_a.GetBool("tocandoSuelo"));

        // **** Audio
        if
        (
            _animator_a.GetBool("moviendose") &&
            _animator_a.GetBool("tocandoSuelo") &&
            !_animator_a.GetBool("cayendo") &&
            !_audioSource_as.isPlaying
        )
        {
            if (_audioSource_as != null && _pasos_array_ac.Length > 0)
            {
                _audioSource_as.PlayOneShot(_pasos_array_ac[UnityEngine.Random.Range(0, _pasos_array_ac.Length)]);
            }
        }
        
    }


    // ***********************( Metodos NESTROS )*********************** // 
    bool estaCayendo()
    {
        return _rb.linearVelocityY < -0.1f && !_animator_a.GetBool("tocandoSuelo");
    }

    bool estaMoviendose()
    {
        return (_rb.linearVelocityX > 0.1f) || (_rb.linearVelocityX < -0.1f);  // (_inputBuffer_q.Count > 0) && 
    }

    // --- InputBuffer --- //
    void inputBuffer()
    {
        if (_inputBuffer_q.Count > 0)
        {
            try 
            {
                // Cojer Objeto
                if (_inputBuffer_q.Peek() == _teclaAccion_kc)
                {
                    cojerObjeto();
                    _inputBuffer_q.Dequeue();
                }

                // Evitar que se quede pulsado A y D.
                if (_inputBuffer_q.Contains(_teclaIzquierda_kc) && _inputBuffer_q.Contains(_teclaDerecha_kc))
                {
                    _animator_a.SetBool("moviendose", false);
                    _inputBuffer_q.Clear();
                }

                // Ir izquierda
                else if (_inputBuffer_q.Peek() == _teclaIzquierda_kc)
                {
                    RaycastHit2D _hitArriba_rh = Physics2D.Raycast(transform.position + new Vector3(-0.5f, 1f), Vector2.left, 0.2f, _MascaraSuelo_lm);
                    RaycastHit2D _hitAbajo_rh = Physics2D.Raycast(transform.position + new Vector3(-0.5f, -1f), Vector2.left, 0.2f, _MascaraSuelo_lm);

                    if (!_hitArriba_rh && !_hitAbajo_rh)
                    {
                        _rb.AddForce(new Vector2((0 - _fuerzaMovimiento_f) * Time.deltaTime, 0));
                        _animator_a.SetBool("moviendose", true);
                    }


                    _spriteRenderer.flipX = true;

                    // Vector3 _manoPosicion_v3 = _Mano_go.transform.localPosition;
                    // _manoPosicion_v3.x = -Mathf.Abs(_manoPosicion_v3.x);
                    // _Mano_go.transform.localPosition = _manoPosicion_v3;

                    _inputBuffer_q.Dequeue();
                }

                // Ir derecha
                else if (_inputBuffer_q.Peek() == _teclaDerecha_kc)
                {
                    RaycastHit2D _hitArriba_rh = Physics2D.Raycast(transform.position + new Vector3(0.5f, 1f), Vector2.right, 0.2f, _MascaraSuelo_lm);
                    RaycastHit2D _hitAbajo_rh = Physics2D.Raycast(transform.position + new Vector3(0.5f, -1f), Vector2.right, 0.2f, _MascaraSuelo_lm);

                    if (!_hitArriba_rh && !_hitAbajo_rh)
                    {
                        _rb.AddForce(new Vector2(_fuerzaMovimiento_f * Time.deltaTime, 0));
                        _animator_a.SetBool("moviendose", true);
                    }

                    _spriteRenderer.flipX = false;

                    // Vector3 _manoPosicion_v3 = _Mano_go.transform.localPosition;
                    // _manoPosicion_v3.x = Mathf.Abs(_manoPosicion_v3.x);
                    // _Mano_go.transform.localPosition = _manoPosicion_v3;

                    _inputBuffer_q.Dequeue();
                }
            }
            catch (InvalidOperationException _ipe_e)
            {
                // No sabesmos porque no para de sucecer.
                // Debug.LogError("Error: " + _ipe_e.Message);
                var matenme = _ipe_e.Message;
            }
        }
    }
    void inputBufferSalto()
    {
        RaycastHit2D raycastsueloIz = Physics2D.Raycast(transform.position + new Vector3(-0.5f, -1f), Vector2.down, 0.25f, _MascaraSuelo_lm);
        RaycastHit2D raycastsueloDer = Physics2D.Raycast(transform.position + new Vector3(0.5f, -1f), Vector2.down, 0.25f, _MascaraSuelo_lm);

        if (raycastsueloIz == true || raycastsueloDer == true)
        {
            tiempoEnElAire = 0;
            _animator_a.SetBool("tocandoSuelo", true);
        }
        else
        {
            tiempoEnElAire += Time.deltaTime;
            _animator_a.SetBool("tocandoSuelo", false);
        }


        if (_inputBufferSalto_q.Count > 0)
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
            if ((_saltar_b == true) && ((raycastsueloIz == true || raycastsueloDer == true) || tiempoEnElAire < (coyoteTime - _coyoteEpsilon_f)))
            {
                _audioSource_as.PlayOneShot(_sonidoSalto);

                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _feurzaSalto_f);
                _inputBufferSalto_q.Dequeue();

                _saltar_b = false;
                StartCoroutine(saltarTrue());
            }
        }  
    }

    IEnumerator saltarTrue()
    {
        yield return new WaitForSeconds(coyoteTime + _coyoteEpsilon_f);
        //Debug.Log("-- VUELVES a SALTAR --");
        _saltar_b = true;
    }

    void quitarAccion()
    {
        if (_inputBuffer_q.Count > 0)
            _inputBuffer_q.Dequeue();
    }
    void quitarSalto()
    {
        if (_inputBufferSalto_q.Count > 0)
            _inputBufferSalto_q.Dequeue();
    }
    

    IEnumerator quitarAccionConRetraso()
    {
        yield return new WaitForSeconds(0.5f);
        if (_inputBuffer_q.Count > 0)
            _inputBuffer_q.Dequeue();
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
                    GameObject _cartaALevantar_go = _hit.collider.transform.parent.gameObject;
                    Debug.Log("El rayo ha colisionado con: " + _cartaALevantar_go.name);

                    if ((_cartaALevantar_go.GetComponent<Carta>() != null) && (_cartaALevantar_go.GetComponent<Carta>()._vendiendose_b == true))
                    {
                        if (_tienda_script.venderCarta(_cartaALevantar_go) == true)
                        {
                            _cartaALevantar_go.GetComponent<Carta>()._vendiendose_b = false;

                            levantarObjeto(_cartaALevantar_go);
                            _objetoRayCast = _cartaALevantar_go;

                            return;
                        }
                    }
                    else
                    {
                        levantarObjeto(_cartaALevantar_go);
                        _objetoRayCast = _cartaALevantar_go;

                        return;
                    }
                }

            }
        }
    }
    void levantarObjeto(GameObject _Objeto_go)
    {
        try
        {
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

            _levantando = true;
        }
        catch (Exception _e)
        {
            Debug.LogError("Error: " + _e.Message);
        }
    }


    // --- Correcion de esquinas --- //
    void esquinas()
    {
        if (_poderCorregir_b == false)
            return;

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
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, (_feurzaSalto_f / 10) * 8.5f);
                    _poderCorregir_b = false;
                    StartCoroutine(volverCoregir());
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
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, (_feurzaSalto_f / 10) * 8.5f);
                    _poderCorregir_b = false;
                    StartCoroutine(volverCoregir());
                    break;
                }
            }
        }
    }

    IEnumerator volverCoregir()
    {
        yield return new WaitForSeconds(0.1f);
        _poderCorregir_b = true;
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