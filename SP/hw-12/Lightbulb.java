package com.company;

/**
 * Lightbulb
 * Can wear out, but brighter than LED
 */
public class Lightbulb extends Lightsource {

	private int wearout;

	public Lightbulb(String newColor) {
		super(newColor, 100);
	}

	public void turnOn() {
		super.turnOn();
		
		this.wearout++;

		if (wearout > 2)
			this.fracture();
	}
}