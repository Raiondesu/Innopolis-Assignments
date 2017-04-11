package com.company;

/**
 * ITogglable
 */
public interface ITogglable {
	public void toggle(ILightable toToggle);

	public boolean isOn();
}