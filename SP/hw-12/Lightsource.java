package com.company;

/**
 * Lightsource
 */
public abstract class Lightsource implements ILightable {

	private boolean isBroken;
	private boolean isOn;
	private String color;
	private int brightness;

	public Lightsource (String newColor, int newBrightness) {
		this.isBroken = false; // make a NEW lightsource
		this.color = newColor;
		this.brightness = newBrightness;
	}

	public void fracture() {
		this.isBroken = true;
		this.isOn = false;
		System.out.println("\tLight is broken and off.");
	}

	public void fix() {
		this.isBroken = false;
		System.out.println("\tLight has been fixed.");
	}

	public boolean isBroken() {
		return this.isBroken;
	}

	public void turnOn() {
		if (!this.isBroken) {
			this.isOn = true;
			System.out.println("\tLight is on.");
		}
		else
			System.out.println("\tLight is broken and can't be turned on.");
	}

	public void turnOff() {
		this.isOn = false;
		System.out.println("\tLight is off.");
	}

	public boolean isOn() {
		return this.isOn;
	}

	public String getColor() { return this.color; }
}