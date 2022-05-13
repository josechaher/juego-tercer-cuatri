using UnityEngine;
using System.Collections;

public class PendulumScript : MonoBehaviour
{

	public GameObject pivot;
	public GameObject ball;

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
		this.bobStartingPosition = this.ball.transform.position;
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
		this.ball.transform.position = newPosition;
	}

	// QUE HACE ESTO?
	[ContextMenu("Reset Pendulum Position")]
	void ResetPendulumPosition()
	{
		if (this.bobStartingPositionSet)
			this.MoveBob(this.bobStartingPosition);
		else
			this.PendulumInit();
	}

	// QUE HACE ESTO?
	[ContextMenu("Reset Pendulum Forces")]
	void ResetPendulumForces()
	{

		this.currentVelocity = Vector3.zero;
		this.currentStatePosition = this.ball.transform.position;
	}

	void PendulumInit()
	{
		// Calculates distance from ball to pivot.
		this.ropeLength = Vector3.Distance(pivot.transform.position, ball.transform.position);
		this.ResetPendulumForces();
	}

	// QUE HACE ESTO?
	void MoveBob(Vector3 resetBobPosition)
	{
		this.ball.transform.position = resetBobPosition;
		this.currentStatePosition = resetBobPosition;
	}

	// QUE HACE ESTO?
	Vector3 PendulumUpdate(Vector3 currentStatePosition, float deltaTime)
	{
		this.gravityForce = this.mass * Physics.gravity.magnitude;
		this.gravityDirection = Physics.gravity.normalized;
		this.currentVelocity += this.gravityDirection * this.gravityForce * deltaTime;

		Vector3 pivot_p = this.pivot.transform.position;
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

	// QUE HACE ESTO?
	Vector3 GetPointOnLine(Vector3 start, Vector3 end, float distanceFromStart)
	{
		return start + (distanceFromStart * Vector3.Normalize(end - start));
	}

	//Draws pendulum path on editor
	void OnDrawGizmos()
	{
		// purple
		Gizmos.color = new Color(.5f, 0f, .5f);
		Gizmos.DrawWireSphere(this.pivot.transform.position, this.ropeLength);

		Gizmos.DrawWireCube(this.bobStartingPosition, new Vector3(.5f, .5f, .5f));

		// Blue: Auxilary
		Gizmos.color = new Color(.3f, .3f, 1f); // blue
		Vector3 auxVel = .3f * this.currentVelocity;
		Gizmos.DrawRay(this.ball.transform.position, auxVel);
		Gizmos.DrawSphere(this.ball.transform.position + auxVel, .2f);

		// Yellow: Gravity
		Gizmos.color = new Color(1f, 1f, .2f);
		Vector3 gravity = .3f * this.gravityForce * this.gravityDirection;
		Gizmos.DrawRay(this.ball.transform.position, gravity);
		Gizmos.DrawSphere(this.ball.transform.position + gravity, .2f);

		// Orange: Tension
		Gizmos.color = new Color(1f, .5f, .2f); // Orange
		Vector3 tension = .3f * this.tensionForce * this.tensionDirection;
		Gizmos.DrawRay(this.ball.transform.position, tension);
		Gizmos.DrawSphere(this.ball.transform.position + tension, .2f);

		// Red: Resultant
		Gizmos.color = new Color(1f, .3f, .3f); // red
		Vector3 resultant = gravity + tension;
		Gizmos.DrawRay(this.ball.transform.position, resultant);
		Gizmos.DrawSphere(this.ball.transform.position + resultant, .2f);

	}
}

