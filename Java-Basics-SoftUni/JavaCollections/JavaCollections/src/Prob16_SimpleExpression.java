import java.math.BigDecimal;
import java.util.Scanner;

public class Prob16_SimpleExpression {

	public static void main(String[] args) {
		Scanner scanner = new Scanner(System.in);
		String expression = scanner.nextLine().trim().replace(" ", "");
		String[] numbers = expression.split("[^0-9.]+");
		String[] signs = expression.split("[0-9.]+");
		BigDecimal sum = new BigDecimal(numbers[0]);
		
		for (int i = 1; i < numbers.length; i++) {
			if (signs[i].equals("+")) {
				BigDecimal num = new BigDecimal(numbers[i]);
				sum = sum.add(num);
			} else{
				BigDecimal num = new BigDecimal(numbers[i]);
				sum = sum.subtract(num);
			}				
		}
		
		System.out.println(sum);
	}
}