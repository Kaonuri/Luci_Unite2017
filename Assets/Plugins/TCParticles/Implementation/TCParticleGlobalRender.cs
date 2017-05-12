using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace TC.Internal {
	public static class TCParticleGlobalRender {
		static Dictionary<Camera, CommandBuffer[]> s_camToBuffer = new Dictionary<Camera, CommandBuffer[]>();
		public static Dictionary<Mesh, ComputeBuffer> MeshStore = new Dictionary<Mesh, ComputeBuffer>();

		public static float[] StretchFacs = {
			0.0f,
			1.0f,
			0.0f,
			1.0f
		};

		public static float[] TailStretchFacs = {
			0.0f,
			0.0f,
			0.0f,
			0.0f,
			1.0f,
			1.0f
		};

		public static Vector3[] Verts = {
			new Vector3(0.5f, -0.5f, 0.0f),
			new Vector3(-0.5f, -0.5f, 0.0f),
			new Vector3(0.5f, 0.5f, 0.0f),
			new Vector3(-0.5f, 0.5f, 0.0f)
		};

		public static void Init() {
			Camera.onPreRender += Render;
		}

		static void Render(Camera cam) {
			CommandBuffer[] cmd;
			if (!s_camToBuffer.TryGetValue(cam, out cmd)) {
				cmd = new CommandBuffer[2];
				cmd[0] = new CommandBuffer();
				cmd[1] = new CommandBuffer();

				cam.AddCommandBuffer(CameraEvent.BeforeForwardAlpha, cmd[0]);
				cam.AddCommandBuffer(CameraEvent.AfterForwardAlpha, cmd[1]);

				s_camToBuffer[cam] = cmd;
			}

			cmd[0].Clear();
			cmd[1].Clear();

			int count = TCParticleSystem.All.Count;

			for (int index = 0; index < count; index++) {
				var syst = TCParticleSystem.All[index];
				var rend = syst.ParticleRenderer as ParticleRenderer;

				if (rend.Material.renderQueue == 2450) {
					//Opaque particles into before forward alpha
					rend.FillCommandBuffer(cmd[0]);
				}
				else {
					//Additive particles after forward alpha
					rend.FillCommandBuffer(cmd[1]);
				}
			}
		}

		public static void ReleaseCaches() {
			Camera.onPreRender -= Render;

			foreach (var bufs in s_camToBuffer) {
				if (bufs.Key != null) {
					bufs.Key.RemoveCommandBuffer(CameraEvent.BeforeForwardAlpha, bufs.Value[0]);
					bufs.Key.RemoveCommandBuffer(CameraEvent.AfterForwardAlpha, bufs.Value[1]);
				}
				
				foreach (var cmd in bufs.Value) {
					cmd.Release();
				}
			}

			s_camToBuffer.Clear();

			foreach (var m in MeshStore) {
				m.Value.Release();
			}

			MeshStore.Clear();
		}
	}
}