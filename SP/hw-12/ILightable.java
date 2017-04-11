package com.company;

/**
 * ILightable
 */
public interface ILightable extends IBreakable  {
	public void turnOn();
	public void turnOff();

	public boolean isOn();
	public String getColor();
}