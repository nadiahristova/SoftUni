import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;

//the problem using TreeMap and no new class
public class Prob9_ListOfProducts {
	public static void main(String[] args) throws FileNotFoundException {
		Scanner input = new Scanner(new File("Input.txt"));
		String line = null;
		Map<Double,String> listOfProducts = new TreeMap<>(); 
		
		while (input.hasNextLine()) {
			line = input.nextLine();
			String[] productAndPrice = line.trim().split("[ //s+]");
			Double price = Double.parseDouble(productAndPrice[1]);
			listOfProducts.put(price, productAndPrice[0]);
		}
		
		input.close();
		
		try (PrintWriter writer = new PrintWriter("Output.txt", "UTF-8")) {			
			for (Double price : listOfProducts.keySet()) {
				writer.println(price + " " + listOfProducts.get(price));
			}	
			//writer.close();
		} catch (IOException e) {
			e.printStackTrace();
		}		
	}

}
