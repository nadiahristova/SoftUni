import java.util.Scanner;

public class Prob6_FormattingNumbers {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		System.out.print("The integer: ");
		int a = input.nextInt();
		System.out.print("The first floating-point num: ");
		float b = input.nextFloat();
		System.out.print("The second floating-point num: ");
		float c = input.nextFloat();
		
		String firstColmn = String.format("%-10s", Integer.toString(a,16)).toUpperCase();
		//or String firstColmn = String.format("%-10s", Integer.toHexString(a));
		String secondColmn = String.format("%10s", Integer.toBinaryString(a)).replace(' ','0');
		//or String secondColmn = String.format("%010d", Integer.parseInt(Integer.toBinaryString(a)));		
		String thirdColmn = String.format("%10.2f", b);
		String fourthColmn = String.format("%-10.3f", c);
		System.out.println("|"+ firstColmn + "|" + secondColmn + "|" + thirdColmn + "|" + fourthColmn + "|");
	}
}
