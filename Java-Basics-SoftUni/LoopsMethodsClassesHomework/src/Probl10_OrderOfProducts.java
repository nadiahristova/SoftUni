import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.util.ArrayList;


public class Probl10_OrderOfProducts {
	public static void main(String[] args) {
		String currentLine = null;
		ArrayList<ProductOr> inputListOFProducts = new ArrayList<>();	
		//Product[] sortedProducts = new ArrayList<>();	
		File outFile = new File("Output2.txt");
		ProductOr product;
		
		try (InputStream inputProducts = new FileInputStream("Input.txt");
			InputStream inputOrder = new FileInputStream("/home/nadia/Downloads/Order.txt");
			BufferedReader buffReadProducts = new BufferedReader(new InputStreamReader(inputProducts));
			BufferedReader buffReadOrder = new BufferedReader(new InputStreamReader(inputOrder));
			BufferedWriter output = new BufferedWriter(new FileWriter(outFile))){
			Reader reader = new InputStreamReader(inputProducts);
			
			while ((currentLine = buffReadProducts.readLine()) != null) {
				if (!currentLine.equals("")) {
					String[] line = currentLine.trim().split("[ //s+]");
					product = new ProductOr(line[0], Double.parseDouble(line[1]),0);
					inputListOFProducts.add(product);					
				}					
			}
			
			while ((currentLine = buffReadOrder.readLine()) != null) {
				if (!currentLine.equals("")) {
					String[] line = currentLine.trim().split("[ //s+]");
					for (int i = 0; i < inputListOFProducts.size(); i++) {
						if (inputListOFProducts.get(i).getName().equals(line[1])) {
							double numOrders = inputListOFProducts.get(i).getNumberOfOrders()+Double.parseDouble(line[0]);
							inputListOFProducts.get(i).setNumberOfOrders(numOrders);
						}
					}					
				}					
			}
			
			double check = ProductOr.calcCheck(inputListOFProducts);	
			output.write(String.format("The total price of the ordered products is %.1f", check));
			output.close();
		} catch (IOException e) {
			System.out.println("Error");
		}
	}	
}

class ProductOr{
	private String name;
	private double price;
	private double numberOfOrders;
	
	public double getNumberOfOrders() {
		return numberOfOrders;
	}	

	public void setNumberOfOrders(double numberOfOrders) {
		this.numberOfOrders = numberOfOrders;
	}

	public ProductOr(String name, double price, double numberOfOrders){
		this.price = price;
		this.name = name;
		this.numberOfOrders = numberOfOrders;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public Double getPrice() {
		return price;
	}

	public void setPrice(Double price) {
		this.price = price;
	}
	
	public static double calcCheck(ArrayList<ProductOr> inputListOFProducts) {
		double sum = 0;
		for (int i = 0; i < inputListOFProducts.size(); i++) {
			double numOrders = inputListOFProducts.get(i).getNumberOfOrders();
			double price = inputListOFProducts.get(i).getPrice();
			sum += numOrders*price;
		}
		return sum;
	}
}
