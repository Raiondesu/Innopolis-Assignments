package com.company;

/**
 * Lamp
 */
public class Lamp implements IBreakable {

	protected ITogglable toggle;
	protected ILightable light;

	public Lamp (ILightable newLight, ITogglable newToggle) {
		this.light = newLight;
		this.toggle = newToggle;
	}

	public void fracture() {
        System.out.println("\nLamp fracture:");
		this.light.fracture();
	}

	public void fix() {
        System.out.println("\nLamp fix:");
		this.light.fix();
	}

	public boolean isBroken() {
		return this.light.isBroken();
	}

	public void toggle() {
        System.out.println("\nLamp toggle:");
		this.toggle.toggle(this.light);
	}

	public boolean isOn() {
		return this.toggle.isOn() && this.light.isOn();
	}

	public String getLightColor() {
		return this.light.getColor();
	}
}