import java.util.List;

/**
 * Created by Alexey Iskhakov on 7.3.17.
 */
public class Order {
	public int orderID;
	public String date;
	public int value;

	public List<OrderItem> orderItems;

	/**
	 * @pre: a precondition to test if a client is registered and not penalized.
	 * @post: a postcondition to test if client's order list contains this order.
	 * @param client: a client to bind order to.
	 */
	public void placeOrder(Client client){
		assert client != null && !client.penalised;

		client.orders.add(this);

		assert client.orders.size() > 0;
	}

	/**
	 * @pre: a precondition to test if amount is in limits.
	 * @post: a postcondition to test if client's order contains this product;
	 * @param client: a client to bind order to.
	 * @param product: a product to add to the order.
	 * @param amount: an amount of products to add,
	 */
	public void placeProductItem(Client client, Product product, int amount){
		assert amount <= product.numItemsInStock;

		int price = client.premiumClient && amount >= 2 ?
				product.premium.premiumPrice : product.normalPrice;
		OrderItem item = new OrderItem(product, amount, price * amount);
		this.orderItems.add(item);

		assert 0 <= product.numItemsInStock && orderItems.size() > 0 && orderItems.contains(item);
	}

	public void finalizeOrder(){}
}
