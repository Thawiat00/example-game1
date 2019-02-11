using UnityEngine;
using System.Collections;

public class FollowCamera : BaseCamera, ICamera
{
    public Transform transformToFollow;
    public Vector3 offset = Vector3.zero;  // new Vector3(3.0f, 0.5f, 0f);
    public float smooth = 5;


    public string CameraName { get { return "Follow Camera"; } }


    void Awake() {
        if (this.offset == Vector3.zero) this.offset = this.transform.position - transformToFollow.position;
    }

    void Start() {
        MoveCamera(true);
    }
    
    void LateUpdate() {
        MoveCamera(false);
    }

    public void MoveCamera(bool changeCameras) {
        Vector3 moveTo = this.transformToFollow.position + this.offset;
        float transitionSpeed = this.CheckTransitionTime(moveTo, changeCameras);

        if (transitionSpeed <= 0.0f) {
            transitionSpeed = Time.deltaTime * smooth;
        }
        moveTo = this.barrierStrikeCheck(this.transformToFollow.position, moveTo);
        this.transform.position = Vector3.Lerp(this.transform.position, moveTo, transitionSpeed);
        this.lookAt(this.transformToFollow.position, transitionSpeed);
    }
}
