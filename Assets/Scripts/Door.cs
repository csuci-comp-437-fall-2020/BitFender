using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    private Animator _animator;
    private Collider2D _collider;
    private Transform spawnPoint;
    private bool open;

    private bool enteredDoor;

    [HideInInspector]
    public GameObject roomToGo;

    // Start is called before the first frame update
    void Start () {
        _animator = GetComponent<Animator> ();
        _collider = GetComponent<Collider2D> ();

        spawnPoint = this.gameObject.transform.GetChild (0);
        CloseDoor ();

        GetConnectedRoom ();

        enteredDoor = false;
    }

    void OnCollisionEnter2D (Collision2D collider) {
        if (open && collider.gameObject.tag == "Player") {
            //Go through door
            GameObject player = collider.gameObject;
            player.transform.position = spawnPoint.position;
            player.GetComponent<Player> ().currentRoom = roomToGo;
            player.GetComponent<Player> ().currentRoom.GetComponent<RoomManager> ().playerInRoom = true;
            Debug.Log (player.GetComponent<Player> ().currentRoom.name);
            gameObject.transform.parent.gameObject.GetComponent<RoomManager> ().playerInRoom = false;
        }
    }

    public void OpenDoor () {
        _animator.SetBool ("Open", true);
        open = true;
    }

    private void CloseDoor () {
        _animator.SetBool ("Open", false);
        open = false;
    }

    private void GetConnectedRoom () {
        RaycastHit2D hit = Physics2D.Raycast (spawnPoint.position, Vector2.zero);

        if (hit.collider) {
            roomToGo = hit.collider.gameObject.transform.parent.gameObject;
        }
    }
}