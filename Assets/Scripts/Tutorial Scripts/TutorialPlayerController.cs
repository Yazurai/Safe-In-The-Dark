using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TutorialPlayerController : MonoBehaviour
{
    public Image BackGround;
    public Image ProgressBar;
    public Image ProgressBar2;
    public bool Simulation;
    public bool Invisible;

    public Slider Ability1Slider;
    public Slider Ability2Slider;

    public EventTrigger Interact;
    public EventTrigger Activate;

    public GameObject ToggleWall;
    public GameObject Station;
    float speed;
    public bool CanMove;
    public float speedMultiplier;
    public float filter;
    public float CooldownMultiplier;

    public bool canInteract;
    public bool canActivate;
    public bool canUseAbility1;
    public bool canUseAbility2;

    public float Ability1CoolDown;
    public float Ability2CoolDown;

    float Ability1Timer;
    float Ability2Timer;

    bool Ability1Active;
    bool Ability2Active;

    public float Ability1Duration;
    public float Ability2Duration;

    float Ability1DurationTimer;
    float Ability2DurationTimer;

    float Timer;
    float Timer2;
    public bool ActivateButtonIsPushed;
    public bool ActivateButtonIsPushed2;

    public GameObject ParticleEffect;

    public void SetInvisible()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        ParticleEffect.SetActive(false);
        Invisible = true;
    }

    public void SetVisible()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        ParticleEffect.SetActive(true);
        Invisible = false;
    }

    void Start()
    {
        Timer = 10;
        Timer2 = 1;
        Ability1Timer = 0;
        Ability2Timer = 0;
        Ability1DurationTimer = Ability1Duration;
        Ability2DurationTimer = Ability2Duration;
        speed = 17.5f;
        ProgressBar.color = new Color(0, 1, 0, 0);
        ProgressBar2.color = new Color(1, 0, 0, 0);
        Simulation = false;
        CanMove = true;
        canActivate = false;
        canInteract = false;
        canUseAbility1 = false;
        canUseAbility2 = false;
    }

    void interactPressDown()
    {
        ActivateButtonIsPushed = true;
        CanMove = false;
    }

    void interactPressUp()
    {
        ActivateButtonIsPushed = false;
        CanMove = true;
        Timer = 10;
        ProgressBar.color = new Color(0, 1, 0, 0);
        ProgressBar.gameObject.GetComponent<RectTransform>().localScale = new Vector3(10, 1, 1);
        if (Station != null)
        {
            Station.GetComponent<TutorialStationController>().Speed = 1;
            Station.GetComponent<TutorialStationController>().Glow.color = new Color(1, 1, 1, 0);
        }
    }

    void ActivatePressDown()
    {
        ActivateButtonIsPushed2 = true;
        CanMove = false;
    }

    void ActivatePressUp()
    {
        ActivateButtonIsPushed2 = false;
        CanMove = true;
        Timer2 = 1;
        ProgressBar2.color = new Color(1, 0, 0, 0);
        ProgressBar2.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    public void ability1()
    {
        if (Ability1Timer < 0)
        {
            Ability1Timer = Ability1CoolDown * CooldownMultiplier;
            Ability1Active = true;
            SetInvisible();
        }
    }

    public void ability2()
    {
        if (Ability2Timer < 0)
        {
            Ability2Timer = Ability2CoolDown * CooldownMultiplier;
            Ability2Active = true;
            speed = 11;
        }
    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (CanMove)
        {
            Vector2 Direction = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Direction = Direction / speed * speedMultiplier;
            Vector3 NextPosition = transform.position;
            NextPosition.x = NextPosition.x + Direction.x;
            NextPosition.y = NextPosition.y + Direction.y;
            GetComponent<Rigidbody2D>().MovePosition(NextPosition);
        }

        if (Simulation == true)
        {
            Vector2 Distance = new Vector2 (transform.position.x - GameObject.FindGameObjectWithTag("Hunter").transform.position.x, transform.position.y - GameObject.FindGameObjectWithTag("Hunter").transform.position.y);
            float temp = (Distance.magnitude / filter);
            if (temp > 0.85f)
                temp = 0.85f;
            BackGround.color = new Color(0, 0, 0, temp);
        }

        if (Input.GetKeyDown (KeyCode.Q) && canInteract)
            interactPressDown();

        if (Input.GetKeyUp(KeyCode.Q) && canInteract)
            interactPressUp();

        if (Input.GetKeyDown(KeyCode.E) && canActivate)
            ActivatePressDown();

        if (Input.GetKeyUp(KeyCode.E) && canActivate)
            ActivatePressUp();

        if (Input.GetKey(KeyCode.Alpha1) && canUseAbility1)
            ability1();

        if (Input.GetKey(KeyCode.Alpha2) && canUseAbility2)
            ability2();

        if (ActivateButtonIsPushed)
        {
            Timer = Timer - Time.deltaTime;
            ProgressBar.color = new Color(0, 1, 0, 0.8f);
            ProgressBar.gameObject.GetComponent<RectTransform>().localScale = new Vector3(Timer, 1, 1);
            if (Station != null)
            {
                Station.GetComponent<TutorialStationController>().Speed = Station.GetComponent<TutorialStationController>().Speed + Time.deltaTime;
                Station.GetComponent<TutorialStationController>().Glow.color = new Color(1, 1, 1, (10 - Timer) / 10);
            }
            if (Timer < 0)
            {
                if (Station != null)
                {
                    Station.GetComponent<TutorialStationController>().GetHacked();
                    Timer = 10;
                    ActivateButtonIsPushed = false;
                    ProgressBar.color = new Color(0, 1, 0, 0);
                    ProgressBar.gameObject.GetComponent<RectTransform>().localScale = new Vector3(2, 1, 1);
                }
                else
                {
                    Timer = 10;
                    ActivateButtonIsPushed = false;
                    ProgressBar.color = new Color(0, 1, 0, 0);
                    ProgressBar.gameObject.GetComponent<RectTransform>().localScale = new Vector3(2, 1, 1);
                }
            }
        }

        if (ActivateButtonIsPushed2)
        {
            Timer2 = Timer2 - Time.deltaTime;
            ProgressBar2.color = new Color(1, 0, 0, 0.8f);
            ProgressBar2.gameObject.GetComponent<RectTransform>().localScale = new Vector3(Timer2, 1, 1);
            if (Timer2 < 0)
            {
                if (ToggleWall != null)
                {
                    ToggleWall.GetComponent<TutorialToggleWallController>().Activate();
                    Timer2 = 1;
                }
                else
                {
                    Timer2 = 1;
                    ActivateButtonIsPushed2 = false;
                    ProgressBar2.color = new Color(0, 1, 0, 0);
                    ProgressBar2.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                }
            }
        }

        //TODO : use a delegate/coroutine for the timer and animation for the slider
        if (Ability1Timer >= 0)
        {
            Ability1Timer = Ability1Timer - Time.deltaTime;
            float Ability1temp;
            if (Ability1Timer < 0)
                Ability1temp = 0;
            else
                Ability1temp = Ability1Timer;
            Ability1temp = 1 - Ability1temp / Ability1CoolDown;
            Ability1Slider.value = Ability1temp;
        }

        //TODO : use a delegate/coroutine for the timer
        if (Ability1Active)
        {
            Ability1DurationTimer = Ability1DurationTimer - Time.deltaTime;
            if (Ability1DurationTimer < 0)
            {
                Ability1Active = false;
                Ability1DurationTimer = Ability1Duration;
                SetVisible();
            }
        }

        //TODO : use a delegate/coroutine for the timer and animation for the slider
        if (Ability2Timer >= 0)
        {
            Ability2Timer = Ability2Timer - Time.deltaTime;
            float Ability2temp;
            if (Ability2Timer < 0)
                Ability2temp = 0;
            else
                Ability2temp = Ability2Timer;
            Ability2temp = 1 - Ability2temp / Ability2CoolDown;
            Ability2Slider.value = Ability2temp;
        }

        //TODO : use a delegate/coroutine for the timer
        if (Ability2Active)
        {
            Ability2DurationTimer = Ability2DurationTimer - Time.deltaTime;
            if (Ability2DurationTimer < 0)
            {
                Ability2Active = false;
                Ability2DurationTimer = Ability2Duration;
                speed = 17.5f;
            }
        }
    }
}
