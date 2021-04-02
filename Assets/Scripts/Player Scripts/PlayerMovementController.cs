using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public PhotonView PV;
    PlayerController PlayerCtrl;

    float Speed;
    public bool CanMove;
    public float SpeedMultiplier;

    private void Start() {
        Speed = 17.5f;
        CanMove = true;
        SpeedMultiplier = 1;
    }

    public void SetSpeed(float s) {
        Speed = s;
    }

    private void Awake() {
        PlayerCtrl = gameObject.GetComponent<PlayerController>();
        PV = gameObject.GetComponent<PhotonView>();
    }

    private void Update() {
        if (PV.isMine && PlayerCtrl.GameStarted) {
            if (CanMove) {
                /*if (IsWindows == false) 
				{*/
                Vector2 Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                Direction = Direction / Speed * SpeedMultiplier;
                Vector3 NextPosition = transform.position;
                NextPosition.x = NextPosition.x + Direction.x;
                NextPosition.y = NextPosition.y + Direction.y;
                GetComponent<Rigidbody2D>().MovePosition(NextPosition);
                /*}
				else
				{
					Vector2 ScrnMidPoint = new Vector2 (Screen.width / 2, Screen.height / 2);
					Vector2 MousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					Vector2 Direction = MousePosition - ScrnMidPoint;
					Direction = Direction / Direction.magnitude;
					Direction = Direction / speed * speedMultiplier;
					Vector3 NextPosition = transform.position;
					NextPosition.x = NextPosition.x + Direction.x;
					NextPosition.y = NextPosition.y + Direction.y;
					GetComponent<Rigidbody2D> ().MovePosition (NextPosition);
				}*/
            }
        }
    }
}
