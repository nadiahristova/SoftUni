import java.util.Scanner;

public class Prob7_CountOfBitsOne {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		System.out.print("Num = ");
		int num = input.nextInt();
		byte counter = 0;		
		for (int i = 0; i < 16; i++) {
			byte bit =(byte)((num >> i) & 1);			
			if (bit == 1) {
				counter++;
			}
		}
		//or we don't use a loop and we find the total count of 1s in num by doing this:
		//byte counter = (byte)Integer.bitCount(num);
		System.out.println("There are total " + counter + " ones in Num's binary form.");
	}
}
