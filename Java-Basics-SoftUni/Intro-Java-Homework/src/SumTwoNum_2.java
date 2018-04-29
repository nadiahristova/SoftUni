import java.util.Scanner;

public class SumTwoNum_2 {
	public static void main(String[] args) {
		Scanner in = new Scanner(System.in);
		System.out.print("Please enter the two numbers separated by space: ");
		String input = in.nextLine().trim();
		in.close();
		Scanner sift = new Scanner(input).useDelimiter("\\s* \\s*");
		
		int firstNum = sift.nextInt();
		int secondNum = sift.nextInt();
		sift.close();
		
		System.out.printf("Sum = %d", firstNum + secondNum);
	}
}
