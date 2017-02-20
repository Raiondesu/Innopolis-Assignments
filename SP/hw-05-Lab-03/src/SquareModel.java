class SquareModel {
    private int value;

    SquareModel() { value = 0; }

    public void reset() { value = 0; }

    void setValue(int v){
    	value = v;
    }
    
    int square() {
    	return value * value;
    }

}
