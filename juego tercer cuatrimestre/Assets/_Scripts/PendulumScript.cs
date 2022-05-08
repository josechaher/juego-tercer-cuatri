
using UnityEngine;
using System.Collections;


public class PendulumScript : MonoBehaviour
{

	public GameObject Pivot;
	public GameObject Bob;

	public float mass = 1f;

	float ropeLength = 2f;

	Vector3 bobStartingPosition;
	bool bobStartingPositionSet = false;

	private Vector3 gravityDirection;
	private Vector3 tensionDirection;

	private Vector3 tangentDirection;
	private Vector3 pendulumSideDirection;

	private float tensionForce = 0f;
	private float gravityForce = 0f;

	Vector3 currentVelocity = new Vector3();
	Vector3 currentStatePosition;
	Vector3 previousStatePosition;

	void Start()
	{
		this.bobStartingPosition = this.Bob.transform.position;
		this.bobStartingPositionSet = true;
		this.PendulumInit();
	}


	float t = 0f;
	float dt = 0.01f;
	float currentTime = 0f;
	float accumulator = 0f;

	void Update()
	{
		float frameTime = Time.time - currentTime;
		this.currentTime = Time.time;
		this.accumulator += frameTime;

		while (this.accumulator >= this.dt)
		{
			this.previousStatePosition = this.currentStatePosition;
			this.currentStatePosition = this.PendulumUpdate(this.currentStatePosition, this.dt);
			accumulator -= this.dt;
			this.t += this.dt;
		}

		float alpha = this.accumulator / this.dt;
		Vector3 newPosition = this.currentStatePosition * alpha + this.previousStatePosition * (1f - alpha);
		this.Bob.transform.position = newPosition;
	}

	[ContextMenu("Reset Pendulum Position")]
	void ResetPendulumPosition()
	{
		if (this.bobStartingPositionSet)
			this.MoveBob(this.bobStartingPosition);
		else
			this.PendulumInit();
	}

	[ContextMenu("Reset Pendulum Forces")]
	void ResetPendulumForces()
	{
		this.currentVelocity = Vector3.zero;
		this.currentStatePosition = this.Bob.transform.position;
	}

	void PendulumInit()
	{
		this.ropeLength = Vector3.Distance(Pivot.transform.position, Bob.transform.position);
		this.ResetPendulumForces();
	}

	void MoveBob(Vector3 resetBobPosition)
	{
		this.Bob.transform.position = resetBobPosition;
		this.currentStatePosition = resetBobPosition;
	}


	Vector3 PendulumUpdate(Vector3 currentStatePosition, float deltaTime)
	{
		this.gravityForce = this.mass * Physics.gravity.magnitude;
		this.gravityDirection = Physics.gravity.normalized;
		this.currentVelocity += this.gravityDirection * this.gravityForce * deltaTime;

		Vector3 pivot_p = this.Pivot.transform.position;
		Vector3 bob_p = this.currentStatePosition;

		Vector3 auxiliaryMovementDelta = this.currentVelocity * deltaTime;
		float distanceAfterGravity = Vector3.Distance(pivot_p, bob_p + auxiliaryMovementDelta);

		if (distanceAfterGravity > this.ropeLength || Mathf.Approximately(distanceAfterGravity, this.ropeLength))
		{
			this.tensionDirection = (pivot_p - bob_p).normalized;

			this.pendulumSideDirection = (Quaternion.Euler(0f, 90f, 0f) * this.tensionDirection);
			this.pendulumSideDirection.Scale(new Vector3(1f, 0f, 1f));
			this.pendulumSideDirection.Normalize();

			this.tangentDirection = (-1f * Vector3.Cross(this.tensionDirection, this.pendulumSideDirection)).normalized;

			float inclinationAngle = Vector3.Angle(bob_p - pivot_p, this.gravityDirection);

			this.tensionForce = this.mass * Physics.gravity.magnitude * Mathf.Cos(Mathf.Deg2Rad * inclinationAngle);
			float centripetalForce = ((this.mass * Mathf.Pow(this.currentVelocity.magnitude, 2)) / this.ropeLength);
			this.tensionForce += centripetalForce;

			this.currentVelocity += this.tensionDirection * this.tensionForce * deltaTime;
		}

		Vector3 movementDelta = Vector3.zero;
		movementDelta += this.currentVelocity * deltaTime;

		float distance = Vector3.Distance(pivot_p, currentStatePosition + movementDelta);
		return this.GetPointOnLine(pivot_p, currentStatePosition + movementDelta, distance <= this.ropeLength ? distance : this.ropeLength);
	}

	Vector3 GetPointOnLine(Vector3 start, Vector3 end, float distanceFromStart)
	{
		return start + (distanceFromStart * Vector3.Normalize(end - start));
	}

	void OnDrawGizmos()
	{
		// purple
		Gizmos.color = new Color(.5f, 0f, .5f);
		Gizmos.DrawWireSphere(this.Pivot.transform.position, this.ropeLength);

		Gizmos.DrawWireCube(this.bobStartingPosition, new Vector3(.5f, .5f, .5f));

		// Blue: Auxilary
		Gizmos.color = new Color(.3f, .3f, 1f); // blue
		Vector3 auxVel = .3f * this.currentVelocity;
		Gizmos.DrawRay(this.Bob.transform.position, auxVel);
		Gizmos.DrawSphere(this.Bob.transform.position + auxVel, .2f);

		// Yellow: Gravity
		Gizmos.color = new Color(1f, 1f, .2f);
		Vector3 gravity = .3f * this.gravityForce * this.gravityDirection;
		Gizmos.DrawRay(this.Bob.transform.position, gravity);
		Gizmos.DrawSphere(this.Bob.transform.position + gravity, .2f);

		// Orange: Tension
		Gizmos.color = new Color(1f, .5f, .2f); // Orange
		Vector3 tension = .3f * this.tensionForce * this.tensionDirection;
		Gizmos.DrawRay(this.Bob.transform.position, tension);
		Gizmos.DrawSphere(this.Bob.transform.position + tension, .2f);

		// Red: Resultant
		Gizmos.color = new Color(1f, .3f, .3f); // red
		Vector3 resultant = gravity + tension;
		Gizmos.DrawRay(this.Bob.transform.position, resultant);
		Gizmos.DrawSphere(this.Bob.transform.position + resultant, .2f);

	}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
>>>>>>> Stashed changes
}
