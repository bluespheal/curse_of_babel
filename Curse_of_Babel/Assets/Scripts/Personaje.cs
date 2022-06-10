using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Personaje : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource dash_sound;
    public AudioSource death_sound;

    [Header("Characteristics")]
    public float salto;
    public float fallMultiplier = 2.5f;
    public bool isGrounded = false;
    public bool canDash = false;
    public bool stopDashing = false;

    private Vector3 touchStart;
    private Vector3 touchEnd;
    private GameObject lineRenderer;

    private Collider coll;
    private Rigidbody rb;
    public ParticleSystem dashRelease;

    public float dashTime;
    private float distToGround;
    public float dashSpeed;
    public float startDashTime;
    public float amortiguador;
    public Pause p;

    private int direction;

    public float vel_rot;

    [Header("Levels")]
    public Saved_variables saved_variables;
    public GameObject[] easy_levels;
    public GameObject[] normal_levels;
    public GameObject[] hard_levels;
    public GameObject tutorial_level;
    public int max_easy_score = 100;
    public int max_normal_score = 300;

    [Header("Animations")]
    public bool Gdash = false;
    public bool landing = false;
    public Animator transitionAnim;
    public Animator knight_animation;
    public bool stomp = false;

    public int knight_idle = 0;
    public int idle_change = 0;

    public GameObject particulasDash;
    public GameObject particulasJump;
    public ParticleSystem particlesStomp;
    Vector3 lastFrameVelocity;
    public float velY;
    public float velX;

    private IEnumerator spawn_tutorial;
    public camera_follow mainCamera;

    public bool alive;

    float speed;

    /*static Personaje instance;
    public static Personaje Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }*/

    void Start()
    {
        alive = true;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        idle_change = Random.Range(2, 4);
        saved_variables.Cargar();
        spawn_tutorial = tutorial_spawn();
        amortiguador = 0.25f;
        if (!saved_variables.progreso.Tutorial)
        {
            // hacer pull de un nivel según el puntaje del jugador
            if (saved_variables.progreso.FirstLevel)
            {
                saved_variables.progreso.FirstLevel = false;
                saved_variables.progreso.score = 0;
                Buscar_Niveles();
            }
            choose_level();
        }
        else {
            //cargar tutorial
            loadtut();
            saved_variables.progreso.Tutorial = false;
        }
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        dashTime = startDashTime;
        distToGround = coll.bounds.extents.y;
    }

    void choose_level()
    {
        //escojer un nivel según el puntaje del jugador(easy,normal,hard), se escojen de forma aleatoria hasta que todos los niveles se cargaron y se reinician
        int indx = -1;
        if (saved_variables.progreso.score <= max_easy_score)
        {
            if (saved_variables.progreso.index < saved_variables.progreso.listE.Count)
            {
                do
                {
                    indx = saved_variables.progreso.listE[Random.Range(0, saved_variables.progreso.listE.Count)];
                } while (indx == -1 || indx == saved_variables.progreso.nivelActual);
            }
            else
            {
                Buscar_Niveles();
                do
                {
                    indx = Random.Range(0, saved_variables.progreso.listE.Count);
                } while (indx == -1 || indx == saved_variables.progreso.nivelActual);
            }
            saved_variables.progreso.listE[indx] = -1;
        }
        if (saved_variables.progreso.score >= max_easy_score && saved_variables.progreso.score <= max_normal_score)
        {
            if (saved_variables.progreso.index < saved_variables.progreso.listN.Count)
            {
                do
                {
                    indx = saved_variables.progreso.listN[Random.Range(0, saved_variables.progreso.listN.Count)];
                } while (indx == -1 || indx == saved_variables.progreso.nivelActual);
            }
            else
            {
                Buscar_Niveles();
                do
                {
                    indx = Random.Range(0, saved_variables.progreso.listN.Count);
                } while (indx == -1 || indx==saved_variables.progreso.nivelActual);
            }
            saved_variables.progreso.listN[indx] = -1;
        }
        if (saved_variables.progreso.score >= max_normal_score)
        {
            if (saved_variables.progreso.index < saved_variables.progreso.listH.Count)
            {
                do
                {
                    indx = saved_variables.progreso.listH[Random.Range(0, saved_variables.progreso.listH.Count)];
                } while (indx == -1 || indx == saved_variables.progreso.nivelActual);
            }
            else
            {
                Buscar_Niveles();
                do
                {
                    indx = Random.Range(0, saved_variables.progreso.listH.Count);
                } while (indx == -1 || indx == saved_variables.progreso.nivelActual);
            }
            saved_variables.progreso.listH[indx] = -1;
        }
        print(indx);
        saved_variables.progreso.index++;
        saved_variables.progreso.nivelActual = indx;
        loadscenes(saved_variables.progreso.nivelActual);
    }

    //reiniciar las listas de index de los niveles
    void Buscar_Niveles()
    {
        saved_variables.progreso.index = 0;
        saved_variables.progreso.listE.Clear();
        saved_variables.progreso.listN.Clear();
        saved_variables.progreso.listH.Clear();
        for (int i = 0; i < easy_levels.Length; i++)
        {
            saved_variables.progreso.listE.Insert(i, i);
        }
        for (int i = 0; i < normal_levels.Length; i++)
        {
            saved_variables.progreso.listN.Insert(i, i);
        }
        for (int i = 0; i < hard_levels.Length; i++)
        {
            saved_variables.progreso.listH.Insert(i, i);
        }
    }

    //reiniciar variables cuando se quita el juego
    void OnApplicationQuit()
    {
        saved_variables.progreso.score = 0;
        saved_variables.progreso.nivelActual = 0;
        saved_variables.progreso.FirstLevel = true;
        saved_variables.Guardar();
    }

    void Play_dash() {
        dash_sound.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if(!isGrounded && canDash && particulasJump.gameObject.activeSelf == false)
        {
            StartCoroutine(turnJumpParticlesOn());
        }
        if (!isGrounded && !canDash)
        {
            particulasJump.gameObject.SetActive(false);
        }
        //Registra la velocidad en X y Y del personaje
        lastFrameVelocity = rb.velocity;
        velY = rb.velocity.y;
        velX = rb.velocity.x;
        //Si el jugador esta moviendose mientras no esta en el aire, reproduce la animacion de Grund Dash
        if (Gdash)
        {
            knight_animation.SetBool("Gdash", true);
        }
        else
        {
            knight_animation.SetBool("Gdash", false);
        }
        //Regristra si el jugador esta moviendose para las animaciones
        speed = rb.velocity.magnitude;
        knight_animation.SetFloat("speed", (float)velX);

        if (alive)
            {

            if (Input.GetKeyDown(KeyCode.R)) {
                saved_variables.progreso.score = 0;
                saved_variables.progreso.hscore = 0;
                saved_variables.progreso.Tutorial = true;
            }

            //Checks if player is touching the ground
            if (isGrounded)
            {
                //Apaga las particulas que indican si tienes un dash extra disponible
                transform.GetChild(5).gameObject.SetActive(false);
                //Indica que estoy haciendo un dash en el suelo
                if (velX > 0 && velY == 0)
                {
                    Gdash = true;
                }
                //Si el jugador "aterriza", hace que su velocidad sea 0 para que no derrape
                if (landing && velY <= 0)
                {
                    landing = false;
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }

            //Dash
            if (direction == 0 && dashTime == startDashTime && !p.paused)
            {
                //Testing with mouse
                if (Input.GetButtonDown("Fire1") && !p.paused)
                {
                    touchStart = Input.mousePosition;
                }
                if (Input.GetButtonUp("Fire1") && !p.paused)
                {
                    touchEnd = Input.mousePosition;
                    direction = 5;
                    stopDashing = false;
                    transform.GetChild(5).gameObject.SetActive(false);
                }
            }
            else
            {
                //Checks if dash is complete and if so, returns direction to 0. Allowing the player to dash again.
                if (dashTime <= 0.0f && !stopDashing)
                {
                    stopDashing = true;
                    dashTime = startDashTime;
                    direction = 0;
                }
                else
                {
                    if (!p.paused)
                    {
                        if (!stopDashing)
                        {
                            dashTime -= Time.deltaTime;
                        }
                        if (direction == 1)
                        {
                            rb.velocity = Vector3.left * dashSpeed;
                        }
                        else if (direction == 2)
                        {
                            rb.velocity = Vector3.right * dashSpeed;
                        }
                        else if (direction == 3)
                        {
                            canDash = false;
                            rb.velocity = Vector3.up * dashSpeed;
                        }
                        else if (direction == 4)
                        {
                            rb.velocity = Vector3.down * dashSpeed;
                        }
                        else if (direction == 5)
                        {
                            DoInput();
                        }
                    }
                }
            }

            //Jump

            if (isGrounded && Input.GetKeyDown(KeyCode.Space)&&!p.paused)
            {
                jump();
            }

            if (!isGrounded && Input.GetKeyDown(KeyCode.Space))
            {

                groundPound();
            }

            if (!isGrounded && direction == 0)
            {
                if (rb){
                    rb.AddForceAtPosition(new Vector3(0f, fallMultiplier * -1, 0f), Vector3.down);
                }
            }
        }
    }

    public void jump()
    {
        if(canDash) StartCoroutine(turnJumpParticlesOn());
        knight_animation.SetTrigger("jump");
        //Si estaba haciendo un stomp, interrumpe la animacion
        knight_animation.SetBool("stomp", false);
        rb.velocity = Vector3.zero;
        //Activa la animacion de estar en el aire
        knight_animation.SetBool("on_air",true);
        if (direction > 0)
        {
            stopDashing = true;
            dashTime = startDashTime;
            direction = 0;
        }
        rb.velocity = Vector3.up * salto;
    }

    public void enemy_bounce()
    {
        transform.position = transform.position + new Vector3(0, 1, 0);
        jump();
        transform.GetChild(5).gameObject.SetActive(true);
        canDash = true;
    }

    public void groundPound()
    {
        rb.velocity = Vector3.zero;
        knight_animation.SetBool("stomp", true);
        stomp = true;
        if (direction > 0)
        {
            stopDashing = true;
            dashTime = startDashTime;
            direction = 0;
        }
        rb.velocity = Vector3.down * salto;
    }

    void DoInput()
    {
        Vector3 p1 = new Vector3();
        Vector3 p2 = new Vector3();
        touchStart.z = Camera.main.nearClipPlane + .1f; //make sure its visible on screen
        touchEnd.z = Camera.main.nearClipPlane + .1f;
        p1 = Camera.main.ScreenToWorldPoint(touchStart);
        p2 = Camera.main.ScreenToWorldPoint(touchEnd);
        //CreateLine(p1, p2);
        Vector3 v = p2 - p1;
        //Rotate character
        if (v.normalized.x >= 0)
        {
            if (v.x <= 1f && v.y <= 1f && v.z <= 1f && v.x >= -1f && v.y >= -1f && v.z >= -1f)
            {
                //si es tap no hace nada
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            if (v.x <= 1f && v.y <= 1f && v.z <= 1f && v.x >= -1f && v.y >= -1f && v.z >= -1f)
            {
                //si es tap no hace nada
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -183, 0);
            }
        }
        //Checks if it was a tap
        if (v.x <= 1f && v.y <= 1f && v.z <= 1f && v.x >= -1f && v.y >= -1f && v.z >= -1f && isGrounded)
        {
            jump();
        }
        //Checks if it was a downwards swipe
        else if (v.normalized.x <= 0.4 && v.normalized.y <= -0.7f && v.normalized.z <= 0 && v.normalized.x >= -0.4f && v.normalized.y >= -1.0f &&  !isGrounded)
        {
            groundPound();
        }
        //bugfix. If the previous if wasn't true because isGrounded was false, it would go into the else below this statement, multiplying v.normalized (0,0,0) by dashSpeed. Causing the player to freeze in the air for a bit.
        else if (v.normalized.x == 0 && v.normalized.y == 0 && v.normalized.z == 0)
        {

        }
        //If it's not a tap, then it does the dash.
        else
        {
            if(canDash)
            {
                Play_dash();
                //Si hace un Dash mientras hace un stomp, interrumpe la animacion de este ultimo
                knight_animation.SetBool("stomp", false);
                //Checks if dash was done upwards, and if so, turns canDash off because for some fucking reason it turns itself on whenever the dash is done upwards.
                if (v.normalized.y > 0.0f)
                {
                    dashRelease.Play();
                    //Realiza una animacion de dash diferente al Dash en suelo
                    if (!isGrounded)
                    {
                        knight_animation.SetTrigger("dash");
                    }
                    //Permite hace un dash extra en el aire
                    if (canDash && !isGrounded)
                    canDash = false;
                }
                rb.velocity = v.normalized * dashSpeed;
            }
        }

    }

    //Death
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mist"))
        {
            dead();
        }
        if (other.CompareTag("door"))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            saved_variables.progreso.score += 100;
            saved_variables.Guardar();
            StartCoroutine(LoadScene());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            dead();
        }
        //Al golpear una pared en el aire, rebotas en la direccion opuesta
        if (collision.gameObject.CompareTag("lateral") && !isGrounded)
        {
            Wall_Bounce();
        }
        //Al golpear una plataforma en el aire, rebotas en la direccion opuesta
        if (collision.gameObject.CompareTag("platform") && !isGrounded || collision.gameObject.CompareTag("platform") && velY > 0)
        {
            Bounce_Platform(collision.contacts[0].normal);
        }
    }

    //Invierte la direccion que tenia el jugador y reduce su velocidad en proporcion a  "amortiguador"
    private void Wall_Bounce()
    {
        rb.velocity = new Vector3(-lastFrameVelocity.x * amortiguador, lastFrameVelocity.y, lastFrameVelocity.z);
    }
    //Invierte la direccion que tenia el jugador y detiene al jugador al traspasar una plataforma semi solida
    private void Bounce_Platform(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.velocity = direction * Mathf.Max(speed - (speed * 0.75f));
    }

    void dead()
    {
        alive = false;
        coll.enabled = false;
        //Apaga las particulas del jugador
        particulasDash.gameObject.SetActive(false);
        particulasJump.gameObject.SetActive(false);
        //Activa la animacion de mierte y evita que otras animaciones se reproduscan
        knight_animation.SetBool("f", true);
        knight_animation.SetTrigger("dead");
        //Cambia la velocidad del jugador a 0 y lo vuelve kinemático
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        saved_variables.progreso.FirstLevel = true;
        saved_variables.Guardar();
        StartCoroutine(LoadGameOver());
        mainCamera.alive = false;
    }

    public void goto_origin() //Función que mueve el personaje al morir en el tutorial
    {
        rb.isKinematic = true;
        particulasDash.gameObject.SetActive(false);
        StartCoroutine("tutorial_spawn");
    }

    void loadtut() { //Función que carga el tutorial
        Instantiate(tutorial_level);
        if (mainCamera)
        {
            mainCamera.stop_signal = GameObject.FindGameObjectWithTag("alto").transform;
            mainCamera.push_signal = GameObject.FindGameObjectWithTag("push_signal").transform;
            mainCamera.alive = true;
        }
    }

    void loadscenes(int scene)
    {
        if (saved_variables.progreso.score <= max_easy_score)
        {
            Instantiate(easy_levels[scene]);
        }
        if (saved_variables.progreso.score >= max_easy_score && saved_variables.progreso.score <= max_normal_score)
        {
            Instantiate(normal_levels[scene]);
        }
        if (saved_variables.progreso.score >= max_normal_score)
        {
            Instantiate(hard_levels[scene]);
        }
        if (mainCamera)
        {
            mainCamera.stop_signal = GameObject.FindGameObjectWithTag("alto").transform;
            mainCamera.push_signal = GameObject.FindGameObjectWithTag("push_signal").transform;
            mainCamera.alive = true;
        }
    }

    public void score_up(int _score) {
        saved_variables.progreso.score += _score;
    }

    //Se llama con eventos en la animacion, cuando se llama cierto numero de veces, hace una animacion de idle alternativa
    public void idle_alts()
    {
        knight_idle++;
        if (knight_idle >= idle_change)
        {
            knight_animation.SetTrigger("check");
            knight_idle = 0;
            idle_change = Random.Range(2, 4);
        }
    }

    IEnumerator turnJumpParticlesOn()
    {
        yield return new WaitForSeconds(0.07f);
        particulasJump.gameObject.SetActive(true);
    }

    IEnumerator tutorial_spawn() //Función que hace que el personaje quede congelado unos segundos al morir en el tutorial
    {
        yield return new WaitForSeconds(0.3f);
        rb.isKinematic = false;
        gameObject.transform.position = GameObject.Find("Try again").transform.position;
        particulasDash.gameObject.SetActive(true);
    }

    IEnumerator LoadScene(){ //Animación y cambio de escena de nivel
      transitionAnim.SetTrigger("fade_out");
      yield return new WaitForSeconds(0.5f);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator LoadGameOver(){ //Animación y cambio de escena a Game Over
      transitionAnim.SetTrigger("fade_out");
    if (!death_sound.isPlaying && !alive)
    {
        death_sound.Play();
    }
      yield return new WaitForSeconds(2.0f);
      SceneManager.LoadScene("GameOver");
      Destroy(this.gameObject);
    }
}
