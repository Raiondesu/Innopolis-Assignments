import java.awt.event.*;

class SquareController {
    private SquareView V;
    private SquareModel M;

    SquareController(SquareModel m, SquareView v) {
		this.M = m;
		this.V = v;
		// adding listener to the view
		this.V.addSquareListener(new ButtonListener());
		this.V.addInputListener(new InputListener());
    }

    private class ButtonListener implements ActionListener {
		public void actionPerformed(ActionEvent e) {
			String userInput = V.getNumber();
			M.setValue(Integer.parseInt(userInput));
			V.setNumber(Integer.toString(M.square()));
		}
    }

	private class InputListener implements KeyListener {
		public void keyTyped(KeyEvent e) {

		}

		public void keyPressed(KeyEvent e) {
			V.setNumber("");
		}

		public void keyReleased(KeyEvent e) {

		}
	}
}
