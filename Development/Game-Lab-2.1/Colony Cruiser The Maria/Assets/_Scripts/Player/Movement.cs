using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public int moveSpeed;

	void FixedUpdate () {
        MovingChecker();
	}

    public void MovingChecker() {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed);
        }
    }

    public void LadderChecker() {
    }

    public void StateSwitch() {
    }

    public void CoverChecker() {
    }

}
