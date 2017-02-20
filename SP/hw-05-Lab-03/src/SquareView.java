import javax.swing.*;
import java.awt.event.*;

public class SquareView extends JFrame{    
    private JTextField input = new JTextField(5);
    private JTextField output = new JTextField(10);
    private JButton sqrBtn = new JButton("Square");
    //
    private SquareModel M;

    SquareView(SquareModel m) {
		M = m;

		// Layout
		JPanel content = new JPanel();
		content.add(input);
		content.add(sqrBtn);
		content.add(output);
		//
		this.setContentPane(content);
		this.pack();
		//
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    String getNumber() {
		return input.getText();
    }

    void setNumber(String v) {
		output.setText(v);
    }

    void addSquareListener(ActionListener a) {
		sqrBtn.addActionListener(a);
    }

    void addInputListener(KeyListener e) {
		input.addKeyListener(e);
    }
}
