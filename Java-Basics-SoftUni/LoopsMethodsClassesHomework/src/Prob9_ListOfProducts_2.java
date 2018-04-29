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


public class Prob9_ListOfProducts_2 {
	public static void main(String[] args) {
		String currentLine = null;
		ArrayList<Product> inputListOFProducts = new ArrayList<>();	
		//Product[] sortedProducts = new ArrayList<>();	
		File outFile = new File("Output1.txt");
		Product product;
		
		try (InputStream input = new FileInputStream("Input.txt");
			BufferedReader buffRead = new BufferedReader(new InputStreamReader(input));
			BufferedWriter output = new BufferedWriter(new FileWriter(outFile))){
			Reader reader = new InputStreamReader(input);
			while ((currentLine = buffRead.readLine()) != null) {
				if (!currentLine.equals("")) {
					String[] line = currentLine.trim().split("[ //s+]");
					product = new Product(line[0], Double.parseDouble(line[1]));
					inputListOFProducts.add(product);					
				}					
			}
			Product[] sortedProducts = Product.sortProducts(inputListOFProducts);	
			for(Product currProduct : sortedProducts){
				String line = currProduct.getPrice() + " " + currProduct.getName()+ "\n";
				output.write(line);
			}
			output.close();
		} catch (IOException e) {
			System.out.println("Error");
		}
	}	
}

class Product{
	private String name;
	private Double price;
	
	public Product(String name, Double price){
		this.price = price;
		this.name = name;
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
	
	public static Product[] sortProducts(ArrayList<Product> inputListOFProducts) {
		Product[] sortedProducts = new Product[inputListOFProducts.size()];		
		int arrIndex = 0;
		do {
			Product minPriceProduct = inputListOFProducts.get(0);
			int index = 0;
			for (int i = 1; i < inputListOFProducts.size(); i++) {
				if (minPriceProduct.getPrice() > inputListOFProducts.get(i).getPrice()) {
					minPriceProduct = inputListOFProducts.get(i);
					index = i;
				}
			}
			inputListOFProducts.remove(index);
			sortedProducts[arrIndex] = minPriceProduct;
			arrIndex++;
		} while (arrIndex < sortedProducts.length);	
		//or we can simply use TreeMap<key=price, value= name> as shown in the other version of this problem << Prob9_ListOfProducts >>
		return sortedProducts;
	}
	
}
