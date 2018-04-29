import java.util.Scanner;

public class Prob8_CountOfEqualBitPairs {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		System.out.print("Num = ");
		int num = input.nextInt();
		String str = Integer.toBinaryString(num);
		byte counter = 0;
		counter = Integer.bitCount(i)
		for (int i = 0; i < (str.length()-1); i++) {
			byte rightBit =(byte)((num >> i) & 1);
			byte leftBit =(byte)((num >> (i+1)) & 1);			
			if (rightBit == leftBit) {
				counter++;
			}
		}
		//or we use byte counter = (byte) Integer.bitCount(num);
		System.out.println("There are total << " + counter + " >> sequences of two equal bits (\"00\" or \"11\") that can be found in the");
		System.out.printf("binary representation of the num %d.\n",num);
	}
}
