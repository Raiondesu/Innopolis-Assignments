package com.company;

/**
 * Toggle
 */
public class Toggle implements ITogglable {

	private boolean isOn = false;

	public Toggle () {
		this.isOn = false;
	}

	public void toggle(ILightable toToggle) {
		if (!this.isOn && !toToggle.isOn()) {
			this.isOn = true;
			System.out.println("\tToggle is on.");
			toToggle.turnOn();
		}
		else {
			this.isOn = false;
			System.out.println("\tToggle is off.");
			toToggle.turnOff();
		}
	}
	
	public boolean isOn() {
		return this.isOn;
	}
}