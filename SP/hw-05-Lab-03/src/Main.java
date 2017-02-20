public class Main {
	public static void main(String[] args) {
		SquareModel M = new SquareModel();
		SquareView V = new SquareView(M);
		SquareController C = new SquareController(M, V);
		V.setVisible(true);
    }
}
