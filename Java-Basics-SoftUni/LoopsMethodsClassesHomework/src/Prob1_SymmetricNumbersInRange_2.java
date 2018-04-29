import java.util.Scanner;
//This program finds symmetric digits in the range of type Long
public class Prob1_SymmetricNumbersInRange_2 {
	public static void main(String[] args) {
		Scanner scn = new Scanner(System.in);
		long startNum = scn.nextLong();
		long endNum = scn.nextLong();
		scn.nextLine();	
		for (long givenNum = startNum; givenNum <= endNum; givenNum++) {
			if (isSymmetric(givenNum)) {
				System.out.print(givenNum + " ");
			}
		}		
	}

	private static boolean isSymmetric(long num) {
		boolean isSymm = true;
		long lengthOfNum = calcLength(num);
		
		for (int i = 1; i <= lengthOfNum/2; i++) {
			long rightDig = num/(long)(Math.pow(10, lengthOfNum-i))%10;
			long leftDig = (num/(long)(Math.pow(10, i)/10))%10;
			if (!(rightDig == leftDig)) {
				isSymm = false;
				continue;
			}
		}
		return isSymm;
	}

	private static long calcLength(long num) {
		int length = 0;	
		long divider=0;
		do {
			length++;
			divider = (long) Math.pow(10, length);
		} while (num/divider != 0);
		return length;
	}

}
