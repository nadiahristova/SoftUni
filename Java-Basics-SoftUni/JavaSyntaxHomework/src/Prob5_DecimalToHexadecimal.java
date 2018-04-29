import java.util.Scanner;

public class Prob5_DecimalToHexadecimal {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		System.out.print("Please enter a number in a decimal format: ");
		int num = input.nextInt();
		String inHexiForm = Integer.toHexString(num);
		System.out.printf("%d = %s", num, inHexiForm.toUpperCase());
	}
}
