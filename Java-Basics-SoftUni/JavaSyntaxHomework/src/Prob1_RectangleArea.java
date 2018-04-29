import java.util.Scanner;

public class Prob1_RectangleArea {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		System.out.print("Please enter the sides of the rectangle[cm]: ");
		int a = input.nextInt();
		int b = input.nextInt();
		int area = a * b;
		System.out.println("The area of the rectangle is "+ area + " cm\u00B2.");	
	}

}
