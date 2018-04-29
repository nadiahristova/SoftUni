import java.util.Scanner;

public class SumTwoNumbers {
	public static void main(String [] args) {
		Scanner in = new Scanner(System.in);
		System.out.print("Please, enter the first num: ");
		int firstNum = in.nextInt();
		//System.out.println();
		System.out.print("Please, enter the second num: ");
		int secondNum = in.nextInt();
		in.close();
		int sum = firstNum+secondNum;
		
		System.out.printf("%d+%d=%d", firstNum, secondNum,sum);
	}
}
