
using System;
using Godot;

partial class FreeLookCamera : FreeLookCameraBase
{	
	public bool Enabled
	{
		set
		{
			m_enabled = value;
			if (m_enabled)
			{
				// save current active camera
				m_previousCamera = GetViewport().GetCamera3D();
				m_previousMouseMode = Input.MouseMode;

				if (m_previousCamera != null)
				{
					// Copy current camera properties into this camera
					GlobalTransform = m_previousCamera.GlobalTransform;
					Fov = m_previousCamera.Fov;
					Near = m_previousCamera.Near;
					Far = m_previousCamera.Far;
					Projection = m_previousCamera.Projection;

					// disable current camera
					m_previousCamera.Current = false;
				}

				// Enable free look camera and set as the current one
				Current = true;
			}
			else
			{
				
				Current = false;
				if (m_previousCamera != null)
				{
					m_previousCamera.Current = true;				
					Input.MouseMode = m_previousMouseMode;
				}
			}
		}

		get
		{
			return m_enabled;
		}
	}

	private bool m_enabled = false;
	private Camera3D m_previousCamera;
	private Input.MouseModeEnum m_previousMouseMode;

	public override void _Ready()
	{
		Current = false;
	}
	public override void _Input(InputEvent _event)
	{
		if (! Enabled)
		{
			return;
		}
		base._Input(_event);
	}
	public override void _Process(double delta)
	{
		if (! Enabled)
		{
			return;
		}

		base._Process(delta);
	}
}
