using UnityEngine;
using System.Collections;

public class ParticleAutoRemove : MonoBehaviour {
    public ParticleSystem particle;

	void Update () {
        if (particle)
        {
            if (particle.IsAlive() == false)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
	}
}
