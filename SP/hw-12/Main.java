package com.company;

public class Main {
    public static void main(String[] args) {
        /// With lightbulb
        System.out.println("\n\nLightbulb:");
        Lamp lamp = new Lamp(new Lightbulb("yellow"), new Toggle());
        lamp.toggle();
        lamp.toggle();
        lamp.fracture();
        lamp.toggle();
        lamp.toggle();
        lamp.fix();
        lamp.toggle();
        lamp.toggle();
        lamp.toggle();
        lamp.toggle();
        
        /// With LED
        System.out.println("\n\nLED:");
        lamp = new Lamp(new LED("yellow"), new Toggle());
        lamp.toggle();
        lamp.toggle();
        lamp.fracture();
        lamp.toggle();
        lamp.toggle();
        lamp.fix();
        lamp.toggle();
        lamp.toggle();
        lamp.toggle();
        lamp.toggle();
    }
}
