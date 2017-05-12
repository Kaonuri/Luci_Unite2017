using UnityEngine;

public interface TCParticleManager {
	void DispatchExtensionKernel(ComputeShader extension, int kernel);
	bool NoSimulation { get; set; }
}