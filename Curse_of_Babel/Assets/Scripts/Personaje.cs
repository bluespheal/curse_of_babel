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

    public GameObject mist;
    private Collider coll;
    private Rigidbody rb;
    public ParticleSystem dashRelease;

    public float dashTime;
    private float distToGround;
    public float dashSpeed;
    public float startDashTime;
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

    public int knight_idle = 0;
    public int idle_change = 0;

    public GameObject particulasDash;
    public GameObject particulasJump;
    Vector3 lastFrameVelocity;
    public float velY;
    public float velX;
    //public Transform raycGround;
    /*RaycastHit hit; //Raycast for floor detection
    public float rayDis; // distance to catch floor
    public Vector3 rayDir; // downwards direction of raycast*/

    public camera_follow mainCamera;

    float speed;

    // Start is called before the first frame update
    void Start()
    {
       //Set starting rotation to look at the camera
        transform.rotation = Quaternion.Euler(0, 180, 0);
        idle_change = Random.Range(2, 4);
        saved_variables.Cargar();
        //saved_variables.progreso.score = 0;
        //print(easy_levels.Length);
        if (!saved_variables.progreso.Tutorial)
        {
            if (saved_variables.progreso.score <= max_easy_score)
            {
                saved_variables.progreso.nivelActual = Random.Range(0, easy_levels.Length-1);
            }
            if (saved_variables.progreso.score >= max_easy_score && saved_variables.progreso.score <= max_normal_score)
            {
                saved_variables.progreso.nivelActual = Random.Range(0, normal_levels.Length-1);
            }
            if (saved_variables.progreso.score >= max_normal_score)
            {
                saved_variables.progreso.nivelActual = Random.Range(0, hard_levels.Length-1);
            }
            print(saved_variables.progreso.nivelActual);
            loadscenes(saved_variables.progreso.nivelActual);
        }
        else {
            loadtut();
            saved_variables.progreso.Tutorial = false;
        }
        mist = GameObject.Find("Niebla");
        //levels = new GameObject[30];
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        dashTime = startDashTime;
        distToGround = coll.bounds.extents.y;
    }

    //Checa si esta en el suelo usando raycast
    /*
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    */

    void OnApplicationQuit()
    {
        saved_variables.progreso.score = 0;
        saved_variables.progreso.nivelActual = 0;
        saved_variables.Guardar();
    }

    void Play_dash() {
        dash_sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        lastFrameVelocity = rb.velocity;
        velY = rb.velocity.y;
        velX = rb.velocity.x;
        if (Input.GetKeyDown(KeyCode.J))
        {
            knight_animation.SetTrigger("dead");
        }
        if (Gdash)
        {
            knight_animation.SetBool("Gdash", true);
        }
        else
        {
            knight_animation.SetBool("Gdash", false);
        }
        speed = rb.velocity.magnitude;
        knight_animation.SetFloat("speed", (float)velX);
        /*if (speed < 1 || !isGrounded)
            Gdash = false;*/
        //rayDir = raycGround.position + (transform.up * -rayDis);
        //Debug.DrawLine(raycGround.position, rayDir, Color.green, 1.0f);
        /*if (Physics.Raycast(raycGround.position, raycGround.up * -1, out hit, rayDis) && velY <= 0)
        {
            isGrounded = true;
            knight_animation.SetBool("on_air", false);
            knight_animation.SetBool("stomp", false);
            if(!Gdash)
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            isGrounded = false;
            knight_animation.SetBool("on_air", true);
            knight_animation.SetBool("Gdash", false);
        }*/

        //print(speed);
        if (Input.GetKeyDown(KeyCode.R)) {
            saved_variables.progreso.score = 0;
            saved_variables.progreso.hscore = 0;
            saved_variables.progreso.Tutorial = true;
        }

        //print(isGrounded());
        //Checks if player is touching the ground
        if (isGrounded)
        {
            transform.GetChild(5).gameObject.SetActive(false);
            //canDash = true;
            if (velX > 0 && velY == 0)
            {
                Gdash = true;
            }
            if (landing && velY <= 0)
            {
                landing = false;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }

        //Dash
        if (direction == 0 && canDash && dashTime == startDashTime)
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
                //rb.velocity = Vector3.zero;
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
            //rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }


        //rb.AddForceAtPosition(new Vector3(0f, fallMultiplier * -1, 0f), Vector3.down);

        //rb.velocity = vel;
    }

    public void bounce()
    {
        jump();
        transform.GetChild(5).gameObject.SetActive(true);
        canDash = true;
    }

    public void jump()
    {
        if(canDash) StartCoroutine(turnJumpParticlesOn());
        knight_animation.SetTrigger("jump");
        knight_animation.SetBool("stomp", false);
        rb.velocity = Vector3.zero;
        knight_animation.SetBool("on_air",true);
        if (direction > 0)
        {
            //StopCoroutine(knight_idle);
            stopDashing = true;
            dashTime = startDashTime;
            direction = 0;
        }
        rb.velocity = Vector3.up * salto;
    }
    public void groundPound()
    {
        rb.velocity = Vector3.zero;
        knight_animation.SetBool("stomp", true);
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
        Play_dash();
        //CreateLine(p1, p2);
        Vector3 v = p2 - p1;
        print(v.normalized);
        //Rotate character
        if(v.normalized.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 273, 0);
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
        //Asspull bugfix. If the previous if wasn't true because isGrounded was false, it would go into the else below this statement, multiplying v.normalized (0,0,0) by dashSpeed. Causing the player to freeze in the air for a bit.
        else if (v.normalized.x == 0 && v.normalized.y == 0 && v.normalized.z == 0)
        {
            
        }
        //If it's not a tap, then it does the dash.
        else
        {
            //Checks if dash was done upwards, and if so, turns canDash off because for some fucking reason it turns itself on whenever the dash is done upwards.
            if (v.normalized.y > 0.0f)
            {
                dashRelease.Play();
                if (!isGrounded)
                {
                    knight_animation.SetTrigger("dash");
                }
                if (canDash && !isGrounded)
                canDash = false;
            }
            rb.velocity = v.normalized * dashSpeed;
            //Rotate character
            if (v.normalized.x >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -183, 0);
            }
        }

    }

    //creates an ugly purple line from p1 to p2
    /*void CreateLine(Vector3 p1, Vector3 p2)
    {
        Destroy(lineRenderer);
        lineRenderer = new GameObject();
        lineRenderer.name = "LineRenderer";
        LineRenderer lr = (LineRenderer)lineRenderer.AddComponent(typeof(LineRenderer));
        lr.SetVertexCount(2);
        lr.SetWidth(0.001f, 0.001f);
        lr.SetPosition(0, p1);
        lr.SetPosition(1, p2);
    }*/

    //Death
    void OnTriggerEnter(Collider other)
    {
        if (other.name == mist.name)
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
        if (collision.gameObject.CompareTag("lateral") && velY > 0 || collision.gameObject.CompareTag("platform") && velY > 0)
        {
            //print("Bounce");
            Bounce(collision.contacts[0].normal);
        }
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        //Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed - 2.0f);
    }


    void dead()
    {
        particulasDash.gameObject.SetActive(false);
        particulasJump.gameObject.SetActive(false);
        knight_animation.SetBool("f", true);
        knight_animation.SetTrigger("dead");
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        saved_variables.Guardar();
        StartCoroutine(LoadGameOver());
        mainCamera.alive = false;
    }

    void loadtut() {
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

    /*public void comportamientos()
    {
        knight_idle = StartCoroutine("idleAlt");
        //idle variado
        //onloadscene
    }*/

    public void idle_alts()
    {
        knight_idle++;
        /*print(knight_idle);
        print(idle_change);*/
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
    IEnumerator LoadScene(){
      transitionAnim.SetTrigger("fade_out");
      yield return new WaitForSeconds(0.5f);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator LoadGameOver(){
      transitionAnim.SetTrigger("fade_out");
    if (!death_sound.isPlaying)
    {
        death_sound.Play();
    }
      yield return new WaitForSeconds(2.0f);
      SceneManager.LoadScene("GameOver");
      Destroy(this.gameObject);
    }
}
