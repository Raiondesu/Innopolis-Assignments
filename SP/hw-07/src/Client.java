import java.util.ArrayList;
import java.util.List;

/**
 * Created by Alexey Iskhakov on 7.3.17.
 */
public class Client {
	public int clientID;
	public String name;
	public boolean premiumClient;
	public boolean penalised;

	public List<Order> orders;

	public List<Product> getProductList(){
		Order o = new Order();
		o.placeOrder(new Client());
		o.placeProductItem(new Client(), new Product(), 1);
		return new ArrayList<>();
	}
}
